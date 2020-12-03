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
    /// A clickable box that displays images
    /// </summary>
    public class LinkPictureBox : Control
    {
        Texture2D image;
        Rectangle sourceRect;
        Rectangle destinationRect;

        Texture2D hoverImage;
        Rectangle hoverSourceRect;

        Color hoverColor;
        float angle;

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
        /// Image to be drawn on hover.
        /// </summary>
        public Texture2D HoverImage
        {
            get { return hoverImage; }
            set { hoverImage = value; }
        }

        /// <summary>
        /// Area of image to be drawn on hover.
        /// </summary>
        public Rectangle HoverSourceRectangle
        {
            get { return hoverSourceRect; }
            set { hoverSourceRect = value; }
        }

        /// <summary>
        /// Color used to highlight control on hover.
        /// </summary>
        public Color HoverColor
        {
            get{ return hoverColor; }
            set { hoverColor = value; }
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// Constructor for LinkPictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        /// <param name="hoverImage">Hover image to be drawn</param>
        public LinkPictureBox(Texture2D image, Rectangle destination, Texture2D hoverImage)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            HoverImage = hoverImage;
        }

        /// <summary>
        /// Constructor for LinkPictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="pos">Position to draw image</param>
        /// <param name="hoverImage">Hover image to be drawn</param>
        public LinkPictureBox(Texture2D image, Vector2 pos, Texture2D hoverImage)
        {
            Image = image;
            DestinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, image.Width, image.Height);
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            HoverImage = hoverImage;
            HoverSourceRectangle = new Rectangle(0, 0, hoverImage.Width, hoverImage.Height);
        }

        /// <summary>
        /// Constructor for PictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        /// <param name="source">Area of image to be drawn</param>
        /// <param name="hoverImage">Hover image to be drawn</param>
        /// <param name="hoverSource">Area of hover image to be drawn</param>
        public LinkPictureBox(Texture2D image, Rectangle destination, Rectangle source, Texture2D hoverImage, Rectangle hoverSource)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;
            Color = Color.White;
        }

        /// <summary>
        /// Constructor for LinkPictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        /// <param name="hoverColor">Color used to highlight control on hover</param>
        public LinkPictureBox(Texture2D image, Rectangle destination, Color hoverColor)
        {
            Image = image;
            DestinationRectangle = destination;
            if (image != null)
                SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            HoverColor = hoverColor;
        }

        /// <summary>
        /// Constructor for LinkPictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="pos">Position to draw image</param>
        /// <param name="hoverColor">Color used to highlight control on hover</param>
        public LinkPictureBox(Texture2D image, Vector2 pos, Color hoverColor)
        {
            Image = image;
            DestinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, image.Width, image.Height);
            position = pos;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            HoverColor = hoverColor;
        }

        /// <summary>
        /// Constructor for PictureBox.
        /// </summary>
        /// <param name="image">Image to be drawn</param>
        /// <param name="destination">Area to draw image to</param>
        /// <param name="source">Area of image to be drawn</param>
        /// <param name="hoverColor">Color used to highlight control on hover</param>
        public LinkPictureBox(Texture2D image, Rectangle destination, Rectangle source, Color hoverColor)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;
            Color = Color.White;
            HoverColor = hoverColor;
        }

        public override void Update(GameTime gameTime)
        {
            hasFocus = Region.Intersects(InputHandler.Mouse);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HoverColor != null)
            {
                if (hoverImage == null && image != null)
                    spriteBatch.Draw(image, destinationRect, sourceRect, hasFocus ? HoverColor : Color, MathHelper.ToRadians(angle), Vector2.Zero, SpriteEffects.None, 0);
                else if (hasFocus && image != null)
                    spriteBatch.Draw(hoverImage, destinationRect, hoverSourceRect, hasFocus ? HoverColor : Color, MathHelper.ToRadians(angle), Vector2.Zero, SpriteEffects.None, 0);
                else if(image != null)
                    spriteBatch.Draw(image, destinationRect, sourceRect, hasFocus ? HoverColor : Color, MathHelper.ToRadians(angle), Vector2.Zero, SpriteEffects.None, 0); ;
            }
            else if (hoverImage != null)
            {
                if (hasFocus)
                    spriteBatch.Draw(hoverImage, destinationRect, hoverSourceRect, Color);
                else
                    spriteBatch.Draw(image, destinationRect, sourceRect, Color);
            }
            else
                throw new Exception("LinkPictureBox has no hover image or color.");
        }

        public void SetRegion()
        {
            region = destinationRect;
        }

        public override void HandleInput()
        {
            if (!hasFocus)
                return;
            if (InputHandler.MousePressed(ButtonPressed.Left))
                base.OnSelected(null);
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
