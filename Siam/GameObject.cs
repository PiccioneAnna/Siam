using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Siam
{
    abstract class GameObject
    {
        #region Fields
        protected int windowWidth;
        protected int windowHeight;
        private Texture2D asset;
        protected Rectangle position;
        protected int moveSpeed;

        // Border positions
        protected const int minX = 0;
        protected const int minY = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Property to access the given position of a game object
        /// </summary>
        public Rectangle Position
        {
            get { return position; }
            set { this.position = value; }
        }

        #endregion

        /// <summary>
        /// Gameobject defines a sprite/object that exists within the game with properties/functions
        /// </summary>
        protected GameObject(int width, int height, Texture2D asset, Rectangle position, int moveSpeed)
        {
            windowWidth = width;
            windowHeight = height;
            this.asset = asset;
            this.position = position;
            this.moveSpeed = moveSpeed;
        }

        /// <summary>
        /// Draws sprite animation
        /// </summary>
        public void AnimateSprite(Rectangle sheetRectangle, SpriteEffects flipSprite, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                asset,
                new Vector2(position.X, position.Y), 
                sheetRectangle,
                Color.White,
                0,              //Rotation
                Vector2.Zero,   //Origin inside the image (top left)
                1.0f,           //Scale (No Change, 100%)
                flipSprite,
                0);             //Layer Depth (Unused)
        }

        public abstract void Update(GameTime gameTime);

    }
}
