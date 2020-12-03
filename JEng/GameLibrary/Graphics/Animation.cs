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
    /// Draws and animates frames.
    /// </summary>
    public class Animation : Sprite
    {
        private List<Texture2D> frames;
        private int currentFrame;
        private int delay;
        private int timer;
        private bool active;

        /// <summary>
        /// All the frames in the animation.
        /// </summary>
        public Texture2D[] Frames
        {
            get { return frames.ToArray(); }
        }

        /// <summary>
        /// Current frame in the animation.
        /// </summary>
        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        /// <summary>
        /// Delay between frames in milliseconds.
        /// </summary>
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        /// <summary>
        /// Whether the animation is active.
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// The current timer status of the animation.
        /// </summary>
        public int Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        /// <summary>
        /// Constructor for the animation.
        /// </summary>
        /// <param name="frames">List of frames to be played</param>
        /// <param name="delay">Delay between frames in milliseconds</param>
        public Animation(List<Texture2D> frames, int delay)
            : base(frames[0])
        {
            this.frames = frames;
            this.delay = delay;
            timer = delay;
            active = true;
            currentFrame = 0;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public void Update(GameTime gameTime)
        {
            if (!active)
                return;
            timer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer < 0)
            {
                timer = delay;
                currentFrame += 1;
                if (currentFrame >= frames.Count)
                    currentFrame = 0;
                Texture = frames[currentFrame];
            }
        }
    }
}
