using JEng.Engine.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI
{
    public class UIComponentManager
    {
        private List<UIComponent> _components;

        public UIComponentManager()
        {
            _components = new List<UIComponent>();
        }

        public void Add(UIComponent component) => _components.Add(component);

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < _components.Count; i++)
            {
                var component = _components[i];
                if (component.Enabled) component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                var component = _components[i];
                if (component.Visible) component.Draw(spriteBatch);
            }
        }
    }
}
