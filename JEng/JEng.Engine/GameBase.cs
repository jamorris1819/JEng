using JEng.Core.Graphics;
using JEng.Core.Input;
using JEng.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;

namespace JEng.Engine
{
    public class GameBase : Game
    {
        private int _width;
        private int _height;

        public GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        private GameStateManager _gameStateManager;

        SpriteFont font;

        public SpriteBatch SpriteBatch { get => _spriteBatch; }

        public BoxingViewportAdapter ViewportAdapter { get; private set; }

        protected GameStateManager GameStateManager
        {
            get => _gameStateManager;
            set => _gameStateManager = value;
        }

        public GameBase(int width, int height)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _gameStateManager = new GameStateManager(this);

            Components.Add(_gameStateManager);
            Components.Add(new InputHandler(this));

            _width = width;
            _height = height;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _width, _height);

            base.Initialize();
            /*Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;

            Graphics.ApplyChanges();*/
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            // System.Console.WriteLine(ViewportAdapter.PointToScreen(InputHandler.Mouse.Location));
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected void ChangeState(BaseGameState state) => _gameStateManager.ChangeState(state);

        protected void PushState(BaseGameState state) => _gameStateManager.PushState(state);

        protected void PopState(BaseGameState state) => _gameStateManager.PopState();
    }
}
