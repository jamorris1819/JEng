using JEng.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;

namespace JEng.Engine.UI
{
    public class UIRenderer
    {
        private BoxingViewportAdapter _viewport;
        private UIInputState _inputState;

        public List<UIState> States { get; }

        public UIRenderer(BoxingViewportAdapter viewport)
        {
            _viewport = viewport;
            _inputState = new UIInputState();
            States = new List<UIState>();
        }

        public void Update(GameTime gameTime)
        {
            // Handle input.
            _inputState.MousePosition= _viewport.PointToScreen(InputHandler.Mouse.X, InputHandler.Mouse.Y);
            if(States.Count > 0)
            {
                States[States.Count - 1].HandleInput(_inputState);
            }

            foreach (var state in States)
            {
                state.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var state in States)
            {
                state.Draw(spriteBatch);
            }
        }
    }
}
