using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics.Colliders
{
    public abstract class Collider
    {
        public delegate bool OnCollisionEventHandler(CollisionData data);
        public delegate void OnSeparationEventHandler(CollisionData data);

        public event OnCollisionEventHandler OnCollision;
        public event OnSeparationEventHandler OnSeparation;

        public Fixture Fixture { get; set; }

        public Vector2 Offset { get; set; }

        public bool IsSensor { get; set; }

        public abstract void ConstructCollider(Rigidbody rigidbody);

        protected void InvokeCollision(CollisionData data)
        {
            OnCollision?.Invoke(data);
        }

        protected void InvokeSeparation(CollisionData data)
        {
            OnSeparation?.Invoke(data);
        }
    }
}
