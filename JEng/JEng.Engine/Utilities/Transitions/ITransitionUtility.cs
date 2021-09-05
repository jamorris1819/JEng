using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JEng.Engine.Utilities.Transitions
{
    public interface ITransitionUtility
    {
        event EventHandler OnTransitionEnd;
        public bool Active { get; }
        public TransitionType Type { get; }
        void BeginTransition(TransitionType type);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void LoadContent();
    }
}
