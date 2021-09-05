using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.Utilities.Transitions
{
    public class FadeTransitionUtility : TransitionUtility, ITransitionUtility
    {
        private float _transitionCounter;

        public float TransitionDuration { get; }

        public Color CurrentFadeColour
        {
            get
            {
                float alpha = Type == TransitionType.FadeIn ? (_transitionCounter / TransitionDuration) : 1f - (_transitionCounter / TransitionDuration);

                return new Color(0, 0, 0, alpha);
            }
        }

        public FadeTransitionUtility(float duration, GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            TransitionDuration = duration;
        }

        public override void BeginTransition(TransitionType type)
        {
            Active = true;
            Type = type;
            _transitionCounter = TransitionDuration;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Active) return;

            spriteBatch.Begin();
            spriteBatch.Draw(_pixel, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), CurrentFadeColour);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _transitionCounter -= dt;

            if (_transitionCounter <= 0)
            {
                TransitionCompleted();
            }
        }
    }
}
