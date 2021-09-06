using JEng.Core.Physics.Colliders;
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

        public Rigidbody CreateRigidbody(RigidbodyType type = RigidbodyType.Static)
            => new Rigidbody(_world.CreateBody(default, default, (BodyType)Enum.Parse(typeof(BodyType), type.ToString())));

        public Rigidbody CreateRigidbody(Collider collider, RigidbodyType type = RigidbodyType.Static)
            => new Rigidbody(
                _world.CreateBody(default, default, (BodyType)Enum.Parse(typeof(BodyType), type.ToString())),
                collider
                );

        public Rigidbody CreateRigidbody(Vector2 position, Collider collider, RigidbodyType type = RigidbodyType.Static)
            => new Rigidbody(
                _world.CreateBody(position, default, (BodyType)Enum.Parse(typeof(BodyType), type.ToString())),
                collider
                );
    }
}
