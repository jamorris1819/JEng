﻿using JEng.Core.Input;
using JEng.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JEng.Engine
{
    public class GameBase : Game
    {
        public GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        private GameStateManager _gameStateManager;

        SpriteFont font;

        public SpriteBatch SpriteBatch { get => _spriteBatch; }

        protected GameStateManager GameStateManager
        {
            get => _gameStateManager;
            set => _gameStateManager = value;
        }

        public GameBase()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _gameStateManager = new GameStateManager(this);

            Components.Add(_gameStateManager);
            Components.Add(new InputHandler(this));
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;

            Graphics.ApplyChanges();
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
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(54, 46, 63));
            base.Draw(gameTime);
        }

        protected void ChangeState(BaseGameState state) => _gameStateManager.ChangeState(state);

        protected void PushState(BaseGameState state) => _gameStateManager.PushState(state);

        protected void PopState(BaseGameState state) => _gameStateManager.PopState();
    }
}
