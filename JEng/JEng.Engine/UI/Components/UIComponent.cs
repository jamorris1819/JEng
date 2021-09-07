using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI.Components
{
    public abstract class UIComponent
    {
        public string Name { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public bool Enabled { get; set; }

        public bool Visible { get; set; }

        public Color Colour { get; set; }

        public Rectangle Region
            => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public UIComponent()
        {
            Colour = Color.White;
            Enabled = true;
            Visible = true;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
