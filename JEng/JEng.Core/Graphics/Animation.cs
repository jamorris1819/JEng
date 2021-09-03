using JEng.Core.Tilesets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEng.Core.Graphics
{
    public class Animation
    {
        private int currentFrame;
        private int delay;

        public string Id { get; set; }

        public string TilesetId { get; set; }

        public AnimationSet Parent { get; }

        public Rectangle[] Frames { get; set; }

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
        /// Constructor for the animation.
        /// </summary>
        /// <param name="frames">List of frames to be played</param>
        /// <param name="delay">Delay between frames in milliseconds</param>
        public Animation(AnimationSet parent, string id, Rectangle[] frames, int delay)
        {
            Parent = parent;
            Id = id;
            this.Frames = frames;
            this.delay = delay;
            currentFrame = 0;
        }

        /// <summary>
        /// Advances to the next frame
        /// </summary>
        public void NextFrame()
        {
            currentFrame++;
            if (currentFrame == Frames.Length) currentFrame = 0;
        }
    }
}
