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
        private List<Texture2D> frames;
        private int currentFrame;
        private int delay;

        public string Id { get; set; }

        /// <summary>
        /// All the frames in the animation.
        /// </summary>
        public Texture2D[] Frames
        {
            get { return frames.ToArray(); }
            set { frames = new List<Texture2D>(value); }
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
        /// Constructor for the animation.
        /// </summary>
        /// <param name="frames">List of frames to be played</param>
        /// <param name="delay">Delay between frames in milliseconds</param>
        public Animation(string id, List<Texture2D> frames, int delay)
        {
            Id = id;
            this.frames = frames;
            this.delay = delay;
            currentFrame = 0;

            if (frames.Count == 3) frames.Add(frames[1]);
        }

        /// <summary>
        /// Advances to the next frame
        /// </summary>
        public void NextFrame()
        {
            currentFrame++;
            if (currentFrame == frames.Count) currentFrame = 0;
        }
    }
}
