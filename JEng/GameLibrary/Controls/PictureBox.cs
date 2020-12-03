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
    /// A box that displays images
    /// </summary>
    public class PictureBox : Control
    {
        Texture2D image;
        Rectangle sourceRect;
        Rectangle destinationRect;

        /// <summary>
        /// Image to be drawn.
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// Area of image to be drawn.
        /// </summary>
        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        /// <summary>
        /// Area to draw image to.
        /// </summary>
        public Rectangle DestinationRectangle
        {
            get { return destinationRect; }
            set { destinationRect = value; }
        }

        /// <summary>
        /// Constructor for PictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        public PictureBox(Texture2D image, Rectangle destination)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }

        /// <summary>
        /// Constructor for PictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="pos">Position to draw image</param>
        public PictureBox(Texture2D image, Vector2 pos)
        {
            Image = image;
            DestinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, image.Width, image.Height);
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }

        /// <summary>
        /// Constructor for PictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        /// <param name="source">Area of image to be drawn</param>
        public PictureBox(Texture2D image, Rectangle destination, Rectangle source)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;
            Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, destinationRect, sourceRect, Color);
        }

        public override void HandleInput()
        {
        }

        /// <summary>
        /// Set the position to draw the image to.
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(Vector2 newPosition)
        {
            destinationRect = new Rectangle((int)newPosition.X, (int)newPosition.Y, sourceRect.Width, sourceRect.Height);
        }
    }
}
