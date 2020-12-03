// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameLibrary.Controls
{
    /// <summary>
    /// A text label.
    /// </summary>
    public class Label : Control
    {
        private Color outlineColor = new Color(0, 0, 0, 50);
        private bool outline = false;
        private int outlineWidth = 3;

        public Color OutlineColor
        {
            get { return outlineColor; }
            set { outlineColor = value; }
        }

        public bool Outline
        {
            get { return outline; }
            set { outline = value; }
        }

        public int OutlineWidth
        {
            get { return outlineWidth; }
            set { outlineWidth = value; }
        }
        /// <summary>
        /// Constructor for label.
        /// </summary>
        public Label()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void SetRegion(int offset = 0)
        {
            Vector2 textSize = spriteFont.MeasureString(text);
            region = new Rectangle((int)position.X, (int)position.Y - offset, (int)textSize.X, (int)textSize.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (outline)
            {
                for (int i = -outlineWidth; i <= outlineWidth; i++)
                {
                    for (int j = -outlineWidth; j <= outlineWidth; j++)
                    {
                        spriteBatch.DrawString(SpriteFont, Text, Position + new Vector2(i, j), outlineColor);
                    }
                }
            }

            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput()
        {
        }
    }
}
