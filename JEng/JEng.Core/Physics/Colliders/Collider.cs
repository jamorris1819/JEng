using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics.Colliders
{
    public abstract class Collider
    {
        public Vector2 Offset { get; set; }

        public abstract void ConstructCollider(Rigidbody rigidbody);
    }
}
