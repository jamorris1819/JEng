using JEng.Core.Input;
using JEng.Engine.UI.Texture;
using JEng.Engine.UI.Texture.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI.Components.Widgets
{
    public abstract class Button<T> : UIComponent, IInteractable where T : UIComponent
    {
        private ITextureRenderer _renderer;

        public ITexture Texture { get; }
        public ITexture HoverTexture { get; }
        public ITexture ClickTexture { get; }

        public T Component { get; }
        public Point ComponentPosition { get; set; }
        public Point ComponentOffset { get; set; }
        public Point ComponentPressOffset { get; set; }

        public bool Hovering { get; protected set; }
        public bool Pressed { get; protected set; }

        public event EventHandler OnHover;
        public event EventHandler OnClicked;

        public override Point Size
        {
            get => base.Size;
            set
            {
                base.Size = value;
                CalculateComponentLocation();
            }
        }

        public Button(T component, ITexture texture, ITexture hoverTexture, ITexture clickTexture)
        {
            Component = component;
            CalculateComponentLocation();

            Texture = texture;
            HoverTexture = hoverTexture;
            ClickTexture = clickTexture;

            _renderer = TextureRendererFactory.Create(texture);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _renderer.Draw(spriteBatch, new Rectangle(Position, Size), Colour);
            Component.Draw(spriteBatch);
        }

        public void HandleInput(UIInputState state)
        {
            bool isHovering = Region.Contains(state.MousePosition);

            if (isHovering)
            {
                Pressed = InputHandler.MouseDown(ButtonPressed.Left);
                _renderer.SetTexture(Pressed ? ClickTexture : HoverTexture);

                if (!Hovering) OnHover?.Invoke(this, null);
                if (!Pressed && InputHandler.MouseReleased(ButtonPressed.Left)) OnClicked?.Invoke(this, null);

                Hovering = true;
            }
            else
            {
                Pressed = false;
                _renderer.SetTexture(Texture);

                Hovering = false;
            }

            Component.Position = Position + ComponentPosition + ComponentOffset + (Pressed ? ComponentPressOffset : Point.Zero);
        }

        private void CalculateComponentLocation()
        {
            var remainingWidth = Size.X - Component.Size.X;
            var remainingHeight = Size.Y - Component.Size.Y;

            ComponentPosition = new Point(remainingWidth / 2, remainingHeight / 2);
        }
    }
}
