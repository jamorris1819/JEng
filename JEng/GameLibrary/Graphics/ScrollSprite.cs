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
    public class ScrollSprite : Sprite
    {
        private bool enabled;
        private static float speed;
        private Rectangle bounds;

        public ScrollSprite(Texture2D texture, Vector2 position, Rectangle bounds)
            : base(texture, position)
        {
            enabled = true;
            this.bounds = bounds;
        }

        /// <summary>
        /// Updates the scroll sprite.
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public void Update(GameTime gameTime)
        {
            if (!enabled)
                return;
            position.Y -= speed;
            if (position.Y < bounds.Y - texture.Height)
                position.Y = bounds.Y + bounds.Height;
        }
        /// <summary>
        /// Draws the scroll sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Region, Color.White);
        }

        /// <summary>
        /// Stops the sprite from scrolling.
        /// </summary>
        public void Stop()
        {
            enabled = false;
        }

        /// <summary>
        /// Sets the speed of the scroller.
        /// </summary>
        /// <param name="inSpeed"></param>
        public static void SetSpeed(float inSpeed)
        {
            speed = inSpeed;
        }
    }
}
