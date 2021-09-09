using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JEng.Engine.UI
{
    public class UIRenderer
    {
        public List<UIState> States { get; }

        public UIRenderer()
        {
            States = new List<UIState>();
        }

        public void Update(GameTime gameTime)
        {
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
