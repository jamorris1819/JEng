// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary.Graphics;

namespace GameLibrary.Controls
{
    /// <summary>
    /// A control component that the user can type into.
    /// </summary>
    public class Textbox : Control
    {
        Color textColor = Color.Black;
        Color backgroundColor = Color.White;
        Color borderColor = Color.Black;
        Color backgroundColorHighlight = Color.LightGray;
        Color backgroundColorSelected = Color.Gray;
        SpriteFont font;
        Vector2 dimensions;
        Vector2 offset;
        int maxCharacters;
        int borderSize;
        bool selected = false;

        /// <summary>
        /// Color of the text.
        /// </summary>
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        /// <summary>
        /// Background color of the control.
        /// </summary>
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        /// <summary>
        /// Border color for the control.
        /// </summary>
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        /// <summary>
        /// Background color on highlight.
        /// </summary>
        public Color BackgroundColorHighlight
        {
            get { return backgroundColorHighlight; }
            set { backgroundColorHighlight = value; }
        }

        /// <summary>
        /// Background color when selected.
        /// </summary>
        public Color BackgroundColorSelected
        {
            get { return backgroundColorSelected; }
            set { backgroundColorSelected = value; }
        }

        /// <summary>
        /// Font to be used inside box.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// Dimensions of the control element.
        /// </summary>
        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        /// <summary>
        /// Offset of the text inside the box.
        /// </summary>
        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Maximum number of characters allowed in the box.
        /// </summary>
        public int MaxCharacters
        {
            get { return maxCharacters; }
            set
            {
                maxCharacters = value;
                FitBox(true);
            }
        }

        /// <summary>
        /// Thickness of the border.
        /// </summary>
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; }
        }

        /// <summary>
        /// Constructore for the control element.
        /// </summary>
        public Textbox()
        {
            HasFocus = false;
            Position = Vector2.Zero;
            Offset = Vector2.Zero;
            Dimensions = new Vector2(300, 50);
            text = "";
            FitBox(false);
        }

        /// <summary>
        /// Sets the width of the textbox.
        /// </summary>
        /// <param name="width">Width in pixels</param>
        public void SetWidth(int width)
        {
            dimensions.X = width;
            FitBox(false);
        }

        /// <summary>
        /// Set the height of the textbox.
        /// </summary>
        /// <param name="height">Height in pixels</param>
        public void SetHeight(int height = 0)
        {
            if (height == 0)
                dimensions.Y = font.MeasureString("A").Y;
            else
                dimensions.Y = height;
        }

        /// <summary>
        /// Update the region of the textbox.
        /// </summary>
        private void UpdateRegion()
        {
            region = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        /// <summary>
        /// Updates the control element.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last update</param>
        public override void Update(GameTime gameTime)
        {
            UpdateRegion();
            hasFocus = region.Intersects(InputHandler.Mouse);
            if (InputHandler.MousePressed(ButtonPressed.Left))
                selected = hasFocus;
            InputHandler.KeyboardListen();
            if (selected && InputHandler.KeyboardIsListening)
            {
                foreach (Keys key in InputHandler.GetKeys())
                {
                    //Check if it's a character we want
                    if ("qwertyuiopasdfghjklzxcvbnm.".Contains(key.ToString().ToLower()) && Text.Length < maxCharacters)
                        Text += key.ToString();
                    if (key.ToString() == "Back" && Text.Length > 0)
                        Text = Text.Remove(Text.Length - 1);
                    if (key.ToString() == "Space")
                        Text += " ";
                }
                OnContentChanged(null);
            }
            InputHandler.KeyboardStopListening();
        }

        /// <summary>
        /// Finds the appropriate color to use for the background.
        /// </summary>
        /// <returns></returns>
        private Color DetermineColor()
        {
            Color drawColor;
            if (selected)
                drawColor = backgroundColorSelected;
            else if (hasFocus)
                drawColor = backgroundColorHighlight;
            else
                drawColor = backgroundColor;
            return drawColor;
        }

        /// <summary>
        /// Draws the textbox to screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D pixel = new SolidColorTexture(spriteBatch.GraphicsDevice, Color.White);
            if (borderSize > 0)
            {
                // Draw border surrounding box.
                spriteBatch.Draw(pixel, new Rectangle((int)position.X - borderSize, (int)position.Y - borderSize, (int)dimensions.X + (borderSize * 2), (int)dimensions.Y + (borderSize * 2)), borderColor);
                // Draw box.
                spriteBatch.Draw(pixel, new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y), DetermineColor());
                spriteBatch.DrawString(font, Text, position + offset, Color.Black);
            }
            else
            {
                spriteBatch.Draw(pixel, region, DetermineColor());
                spriteBatch.DrawString(font, Text, position + offset, Color.Black);
            }
        }

        public override void HandleInput()
        {
            if (!HasFocus)
                return;
            if (InputHandler.MouseReleased(ButtonPressed.Left))
            {
                base.OnSelected(null);
            }
        }

        /// <summary>
        /// Recalculates dimensions and max characters allowed.
        /// </summary>
        /// <param name="fitChars">Whether to resize to fit max characters, or change max characters to fit width</param>
        /// <param name="padding">Padding to place around the text</param>
        private void FitBox(bool fitChars, int padding = 10)
        {
            Vector2 size = spriteFont.MeasureString("A");
            offset = new Vector2(padding);
            dimensions.Y = size.Y + (2 * padding);
            if (fitChars)
            {
                dimensions.X = (2 * padding) + ((maxCharacters + 1) * size.X);
            }
            else
            {
                maxCharacters = (int)Math.Floor(((dimensions.X - (2 * padding)) / size.X));
            }
        }
    }
}
