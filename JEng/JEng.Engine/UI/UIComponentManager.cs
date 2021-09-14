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
        private List<IInteractable> _interactableComponents;

        public UIComponentManager()
        {
            _components = new List<UIComponent>();
            _interactableComponents = new List<IInteractable>();
        }

        public void Add(UIComponent component)
        {
            _components.Add(component);
            if (component is IInteractable interactable)
                _interactableComponents.Add(interactable);
        }

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

        public void HandleInput(UIInputState state)
        {
            for (int i = 0; i < _interactableComponents.Count; i++)
            {
                _interactableComponents[i].HandleInput(state);
            }
        }
    }
}
