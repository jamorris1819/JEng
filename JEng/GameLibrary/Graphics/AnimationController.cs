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

using GameLibrary.Graphics;

namespace GameLibrary.Graphics
{
    public class AnimationController
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        private Vector2 position;

        /// <summary>
        /// The current animation used by the controller.
        /// </summary>
        public Animation CurrentAnimation
        {
            get { return currentAnimation; }
        }

        /// <summary>
        /// Get an animation.
        /// </summary>
        /// <param name="i">Animation identifier</param>
        /// <returns></returns>
        public Animation this[string i]
        {
            get { return animations[i]; }
            set { animations[i] = value; }
        }

        /// <summary>
        /// Root position to draw all animations.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The current width of the animation controller.
        /// </summary>
        public int Width
        {
            get { return currentAnimation.Frames[currentAnimation.CurrentFrame].Width; }
        }

        /// <summary>
        /// The current height of the animation controller.
        /// </summary>
        public int Height
        {
            get { return currentAnimation.Frames[currentAnimation.CurrentFrame].Height; }
        }

        /// <summary>
        /// Constructor for animation controller.
        /// </summary>
        /// <param name="dictionary">All the animations to be loaded into the controller</param>
        /// <param name="initialAnimation">Animation to start on</param>
        public AnimationController(Dictionary<string, Animation> dictionary, string initialAnimation)
            : base()
        {
            animations = dictionary;
            currentAnimation = this[initialAnimation];
            Start();
        }

        /// <summary>
        /// Starts the current animation.
        /// </summary>
        public void Start()
        {
            currentAnimation.Active = true;
        }

        /// <summary>
        /// Stops the current animation.
        /// </summary>
        public void Stop()
        {
            currentAnimation.Active = false;
        }

        /// <summary>
        /// Changes the current animation
        /// </summary>
        /// <param name="name">Key of the animation to change to</param>
        /// <param name="alignTimes">Keep the timer the same so animations don't keep restarting.</param>
        public void Change(string name, bool alignTimes = false)
        {
            int time = currentAnimation.Timer;
            currentAnimation = this[name];
            if (alignTimes)
                currentAnimation.Timer = time;
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">Time since last update</param>
        public void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
            currentAnimation.Root = Position;
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="position">Position to set the animation controller to</param>
        /// <param name="gameTime">Time since last update</param>
        public void Update(Vector2 position, GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
            currentAnimation.Root = position;
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect)
        {
            currentAnimation.Draw(spriteBatch, effect);
        }
    }
}
