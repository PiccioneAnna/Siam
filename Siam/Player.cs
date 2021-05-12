using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Siam
{
    //Player class handles all functions related to the player, derives from the gameobject class
    class Player : GameObject
    {
        #region Fields
        KeyboardState oldKeyState; // Old key state to help track user input
        #endregion

        /// <summary>
        /// Player constructer derives from gameobject to get asset, dimensions, positions, and speed
        /// </summary>
        public Player(int width, int height, Texture2D asset,
            Rectangle position, int moveSpeed) : base(width, height, asset, position, moveSpeed)
        {
            //TODO
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
        }

        /// <summary>
        /// Based on the current keyboard states and keys pressed, determines player movements/interactions
        /// </summary>
        private void PlayerMovement(KeyboardState kbState)
        {
            // Takes player input in 8 directional movement
            if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A))
            {
                position.X -= moveSpeed;
            }
            if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D))
            {
                position.X += moveSpeed;
            }
            if (kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.S))
            {
                position.Y += moveSpeed;
            }
            if (kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.W))
            {
                position.Y -= moveSpeed;
            }

            // Simulates running by multiplying the speed by half of itself
            if (kbState.IsKeyDown(Keys.LeftShift) || kbState.IsKeyDown(Keys.RightShift))
            {
                moveSpeed *= moveSpeed/2;
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
