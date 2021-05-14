using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Siam
{
    public class Game1 : Game
    {
        #region All Variables/Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Border positions
        private int maxX;
        private int maxY;

        //Tile Sizes
        private int tileSize = 32;

        //Player Information
        private Player player; //Player
        private Rectangle position; //Player position
        private int playerSpeed = 2;

        //Assets & Fonts
        private Texture2D playerAsset;
        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set Temp Game Size
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading Asset Images
            playerAsset = Content.Load<Texture2D>("BetaSheetBetter");

            // Sets the max x and y values for borders
            maxX = _graphics.PreferredBackBufferWidth - tileSize;
            maxY = _graphics.PreferredBackBufferHeight - tileSize * 2;

            //Temp position and Player
            position = new Rectangle(tileSize, tileSize, tileSize, tileSize *2);
            player = new Player(maxX, maxY, playerAsset, position, playerSpeed);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(); //Starts drawing

            player.Draw(_spriteBatch);

            _spriteBatch.End(); //Ends drawing

            base.Draw(gameTime);
        }
    }
}
