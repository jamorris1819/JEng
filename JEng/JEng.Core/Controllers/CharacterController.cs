using JEng.Core.Physics;
using Microsoft.Xna.Framework;

namespace JEng.Core.Controllers
{
    public abstract class CharacterController : ICharacterController
    {
        public Rigidbody Rigidbody { get; set; }
        public float Speed { get; set; } = 60.0f;
        public bool Enabled { get; set; } = true;

        public abstract void Update(GameTime gameTime);

        protected virtual void Move(Vector2 vec, GameTime gameTime)
        {
            if (vec != Vector2.Zero)
            {
                vec.Normalize();
                //Rigidbody.ApplyForce(vec * 100.0f);
                Rigidbody.SetVelocity(vec * Speed);
            }
            else Rigidbody.SetVelocity(new Vector2(0, 0));
        }
    }
}
