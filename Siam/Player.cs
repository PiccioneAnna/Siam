using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Siam
{
    /// <summary>
    /// Determines which player sprite to draw based on where/what the player is doing
    /// </summary>
    enum PlayerState
    {
        WalkLeft,
        WalkRight,
        WalkStraight,
        WalkBack,
        Standing,
    }


    //Player class handles all functions related to the player, derives from the gameobject class
    class Player : GameObject
    {
        #region Fields
        KeyboardState oldKeyState; // Old key state to help track user input
        PlayerState state;

        //Animation fields
        int frame;              //Curent Animation field
        double timeCounter;     //Amount of time that has passed
        double fps;             //Animation Speed
        double timePerFrame;    //Amount of time per frame

        //Constants for the source rectangle inside of the spritesheet
        const int idleStandingFrames = 2;   //Number of frames in idle standing state

        #endregion

        /// <summary>
        /// Player constructer derives from gameobject to get asset, dimensions, positions, and speed
        /// </summary>
        public Player(int width, int height, Texture2D asset,
            Rectangle position, int moveSpeed) : base(width, height, asset, position, moveSpeed)
        {
            //Starts off starting state as player standing
            this.state = PlayerState.Standing;

            //Initializes
            fps = 3.0;
            timePerFrame = 1.0 / fps;
        }

        // When called updates the player object
        public override void Update(GameTime gameTime)
        {
            // Set a new keyboard state
            KeyboardState newKeystate = Keyboard.GetState();
            // Run the methods for user input and the pause menu input
            PlayerMovement(newKeystate);
            // Sets the current keystate to the previous before the method is ran again
            oldKeyState = newKeystate;
            UpdateAnimation(gameTime);
        }

        /// <summary>
        /// Updates player's animation as necessary
        /// </summary>
        public void UpdateAnimation(GameTime gameTime)
        {
            // Add to the time counter  
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Check if we have enough "time" to advance the frame
            // If enough time has passed:
            if (timeCounter >= timePerFrame)
            {
                frame += 1;                     // Adjust the frame to the next image

                if (frame > 1)                  // Check the bounds - have we reached the end of walk cycle?
                    frame = 0;                  // Back to 1 (since 0 is the "standing" frame)

                timeCounter -= timePerFrame;    // Remove the time we "used" - don't reset to 0
                                                // This keeps the time passed 
            }
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            //Determines what to draw based on what state the player is in
            switch (state)
            {
                case PlayerState.WalkLeft:
                    AnimateSprite(new Rectangle(frame * Position.Width, Position.Height*4, Position.Width, Position.Height), SpriteEffects.None, sb);
                    break;
                case PlayerState.WalkRight:
                    AnimateSprite(new Rectangle(frame * Position.Width, Position.Height*3, Position.Width, Position.Height), SpriteEffects.None, sb);
                    break;
                case PlayerState.WalkStraight:
                    AnimateSprite(new Rectangle(frame * Position.Width, Position.Height, Position.Width, Position.Height), SpriteEffects.None, sb);
                    break;
                case PlayerState.WalkBack:
                    AnimateSprite(new Rectangle(frame * Position.Width, Position.Height*2, Position.Width, Position.Height), SpriteEffects.None, sb);
                    break;
                case PlayerState.Standing:
                    AnimateSprite(new Rectangle(frame * Position.Width, 0, Position.Width, Position.Height), SpriteEffects.None, sb);
                    break;
                default:
                    break;
            }



        }

        /// <summary>
        /// Based on the current keyboard states and keys pressed, determines player movements/interactions
        /// </summary>
        private void PlayerMovement(KeyboardState kbState)
        {
            //Default state of standing
            state = PlayerState.Standing;

            // Takes player input in 8 directional movement

            if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A))
            {
                position.X -= moveSpeed;
                state = PlayerState.WalkLeft;
            }
            if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D))
            {
                position.X += moveSpeed;
                state = PlayerState.WalkRight;
            }
            if (kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.S))
            {
                position.Y += moveSpeed;
                state = PlayerState.WalkStraight;
            }
            if (kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.W))
            {
                position.Y -= moveSpeed;
                state = PlayerState.WalkBack;
            }

            // Simulates running by multiplying the speed by half of itself
            if (kbState.IsKeyDown(Keys.LeftShift) || kbState.IsKeyDown(Keys.RightShift))
            {
                //moveSpeed = moveSpeed * 2;
            }

            // Prevent it if the player tries to move outside of the window boundaries
            if (position.X < minX || position.X > windowWidth)
            {
                position.X = MathHelper.Clamp(position.X, minX, windowWidth);
            }
            if (position.Y < minY || position.Y > windowHeight)
            {
                position.Y = MathHelper.Clamp(position.Y, minY, windowHeight);
            }
        }

        /// <summary>
        /// Checks to see if a single key has been pressed or if it is being held
        /// </summary>
        private bool SingleKeyPress(Keys key, KeyboardState kbState)
        {
            return kbState.IsKeyDown(key) && !oldKeyState.IsKeyDown(key);
        }
    }
}
