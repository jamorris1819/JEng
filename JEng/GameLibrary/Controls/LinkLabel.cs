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
    /// An interactable label.
    /// </summary>
    public class LinkLabel : Control
    {
        private Color selectedColor = Color.Gray;
        private Color outlineColor = new Color(0, 0, 0, 50);
        private bool outline = false;
        private int outlineWidth = 3;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

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
        /// Constructor for LinkLabel.
        /// </summary>
        public LinkLabel()
        {
            HasFocus = false;
            Position = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            hasFocus = Region.Intersects(InputHandler.Mouse);
        }

        public void SetRegion()
        {
            Vector2 textSize = spriteFont.MeasureString(text);
            region = new Rectangle((int) position.X, (int) position.Y, (int) textSize.X, (int) textSize.Y);
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

            if (hasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, SelectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput()
        {
            if (!HasFocus)
                return;
            if (InputHandler.MouseReleased(ButtonPressed.Left))
                base.OnSelected(null);
        }
    }
}
