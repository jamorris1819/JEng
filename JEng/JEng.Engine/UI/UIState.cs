using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI
{
    public abstract class UIState
    {
        protected UIComponentManager Components { get; }

        public UIState()
        {
            Components = new UIComponentManager();
        }

        public virtual void Update(GameTime gameTime) => Components.Update(gameTime);

        public virtual void Draw(SpriteBatch spriteBatch) => Components.Draw(spriteBatch);
    }
}
