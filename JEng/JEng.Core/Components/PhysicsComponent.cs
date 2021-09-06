using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Components
{
    public class PhysicsComponent
    {
        public Body Body { get; set; }

        public Vector2 Offset { get; set; }

        public PhysicsComponent(Body body) : this(body, Vector2.Zero) { }

        public PhysicsComponent(Body body, Vector2 offset)
        {
            Body = body;
            Offset = offset;
        }
    }
}
