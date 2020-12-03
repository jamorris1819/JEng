// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary.Graphics
{
    /// <summary>
    /// A class for drawing a sprite.
    /// </summary>
    public class Sprite
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle region;
        protected Color color;
        protected Vector2 root;
        protected bool border;

        /// <summary>
        /// The texture drawn by the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Location on screen to draw the sprite.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Region covered by the sprite.
        /// </summary>
        public Rectangle Region
        {
            get { return new Rectangle((int)(root.X + position.X), (int)(position.Y + root.Y), (int)texture.Width, (int)texture.Height); }
        }

        /// <summary>
        /// The draw color of the sprite.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// The root position to draw from.
        /// </summary>
        public Vector2 Root
        {
            get { return root; }
            set { root = value; }
        }

        /// <summary>
        /// Whether or not the sprite is visible.
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor for the sprite.
        /// </summary>
        /// <param name="texture">Texture to be drawn</param>
        /// <param name="position">Location on screen to draw the sprite</param>
        public Sprite(Texture2D texture, Vector2 position)
        {
            root = Vector2.Zero;
            this.texture = texture;
            this.position = position;
            Visible = true;
            color = Color.White;
        }

        /// <summary>
        /// Constructor for the sprite.
        /// </summary>
        /// <param name="texture">Texture to be drawn</param>
        /// <param name="position">Location on screen to draw the sprite</param>
        /// <param name="border">Whether to display a border</param>
        public Sprite(Texture2D texture, Vector2 position, bool border)
        {
            root = Vector2.Zero;
            this.texture = texture;
            this.position = position;
            this.border = border;
            Visible = true;
            color = Color.White;
        }

        /// <summary>
        /// Constructor for the sprite.
        /// </summary>
        /// <param name="texture">Texture to be drawn</param>
        /// <param name="position">Location on screen to draw the sprite</param>
        public Sprite(Texture2D texture)
        {
            root = Vector2.Zero;
            this.texture = texture;
            this.position = Vector2.Zero;
            Visible = true;
            color = Color.White;
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            if (border)
            {
                spriteBatch.Draw(texture, position + new Vector2(-1, -1), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(0, -1), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(1, -1), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(-1, 0), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(1, 0), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(-1, 1), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(0, 1), Color.Black);
                spriteBatch.Draw(texture, position + new Vector2(1, 1), Color.Black);
            }

            spriteBatch.Draw(texture, Region, color);
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch, SpriteEffects effect)
        {
            spriteBatch.Draw(texture, Region, null, Color.White, 0f, Vector2.Zero, effect, 0f);
        }
    }
}
