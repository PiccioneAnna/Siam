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

        // Constructor that sets the asset image and position
        // Position is based on a rectangle and the image is based upon a texture2D
        protected GameObject(int width, int height, Texture2D asset, Rectangle position, int moveSpeed)
        {
            windowWidth = width;
            windowHeight = height;
            this.asset = asset;
            this.position = position;
            this.moveSpeed = moveSpeed;
        }

        // Virtual method to draw out the asset 
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(asset, Position, Color.White);
        }

        public abstract void Update(GameTime gameTime);

    }
}
