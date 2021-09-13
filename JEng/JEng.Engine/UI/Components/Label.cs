using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Components
{
    public class Label : UIComponent
    {
        public SpriteFont Font { get; set; }

        public string Text { get; set; }

        public Label(string text, SpriteFont font)
        {
            Text = text;
            Font = font;

            var size = font.MeasureString(text);
            Size = new Point((int)size.X, (int)size.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, new Vector2(Position.X, Position.Y), Colour);
        }
    }
}
