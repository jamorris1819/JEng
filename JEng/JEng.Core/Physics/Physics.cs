using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics
{
    public class Physics
    {
        private World _world;

        public Physics(World world)
        {
            _world = world;
        }

        public Rigidbody CreateRigidbody(Vector2 position)
        {
            var body = _world.CreateCircle(1.0f, 1.0f);
            body.BodyType = BodyType.Dynamic;
            body.Position = new tainicom.Aether.Physics2D.Common.Vector2(position.X, position.Y);

            body.SetRestitution(0.3f);
            body.SetFriction(0.5f);

            return new Rigidbody(body);
        }
    }
}
