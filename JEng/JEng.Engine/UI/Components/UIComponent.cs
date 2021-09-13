using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Components
{
    public abstract class UIComponent
    {
        public string Name { get; set; }

        public Point Position { get; set; }

        public Point Size { get; set; }

        public bool Enabled { get; set; }

        public bool Visible { get; set; }

        public Color Colour { get; set; }

        public Rectangle Region => new Rectangle(Position, Size);

        public UIComponent()
        {
            Colour = Color.White;
            Enabled = true;
            Visible = true;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
