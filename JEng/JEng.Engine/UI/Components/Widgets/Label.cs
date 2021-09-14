using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JEng.Engine.UI.Components.Widgets
{
    public class Label : UIComponent
    {
        public SpriteFont Font { get; set; }

        public string Text { get; set; }

        public override Point Size
            => new Point((int)Font.MeasureString(Text).X,
                (int)Font.MeasureString(Text).Y);

        public int TextWidth => (int)Math.Ceiling(Font.MeasureString(Text).X);

        public int TextHeight => (int)Math.Ceiling(Font.MeasureString(Text).Y);

        public Label(string text, SpriteFont font)
        {
            Text = text;
            Font = font;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, new Vector2(Position.X, Position.Y), Colour);
        }
    }
}
