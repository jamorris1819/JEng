using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics
{
    public class PhysicsWorld : World
    {
        public PhysicsWorld(Vector2 gravity) : base(gravity)
        {
        }
    }
}
