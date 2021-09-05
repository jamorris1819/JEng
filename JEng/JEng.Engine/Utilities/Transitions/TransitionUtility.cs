using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.Utilities.Transitions
{
    public abstract class TransitionUtility : ITransitionUtility
    {
        protected readonly GraphicsDeviceManager _graphics;
        protected readonly ContentManager _content;
        protected Texture2D _pixel;

        public bool Active { get; protected set; }
        public TransitionType Type { get; protected set; }

        public event EventHandler OnTransitionEnd;

        public TransitionUtility(GraphicsDeviceManager graphics, ContentManager content)
        {
            _graphics = graphics;
            _content = content;
        }

        public abstract void BeginTransition(TransitionType type);

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void LoadContent()
        {
            _pixel = _content.Load<Texture2D>("pixel");
        }

        public abstract void Update(GameTime gameTime);

        protected virtual void TransitionCompleted()
        {
            Active = false;
            OnTransitionEnd?.Invoke(this, null);
        }
    }
}
