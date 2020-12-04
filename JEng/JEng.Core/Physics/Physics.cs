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


        public static Vector2 ConvertVector(tainicom.Aether.Physics2D.Common.Vector2 vec) => new Vector2(vec.X, vec.Y);

        public static tainicom.Aether.Physics2D.Common.Vector2 ConvertVector(Vector2 vec) => new tainicom.Aether.Physics2D.Common.Vector2(vec.X, vec.Y);
    }
}
