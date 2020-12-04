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
            var body = _world.CreateCircle(16.0f, 0.15f);
            body.BodyType = BodyType.Dynamic;
            body.Position = new tainicom.Aether.Physics2D.Common.Vector2(position.X, position.Y);

            body.SetRestitution(0.3f);
            body.SetFriction(0.5f);

            return new Rigidbody(body);
        }

        public Rigidbody CreateCircleRigidbody(float radius, float density, Vector2 position = default, RigidbodyType type = RigidbodyType.Static)
            => new Rigidbody(_world.CreateCircle(radius, density, ConvertVector(position), (BodyType)Enum.Parse(typeof(BodyType), type.ToString())));
        



        public static Vector2 ConvertVector(tainicom.Aether.Physics2D.Common.Vector2 vec) => new Vector2(vec.X, vec.Y);

        public static tainicom.Aether.Physics2D.Common.Vector2 ConvertVector(Vector2 vec) => new tainicom.Aether.Physics2D.Common.Vector2(vec.X, vec.Y);
    }
}
