using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics
{
    public class Physics
    {
        public static Rigidbody CreateRigidbody(Vector2 position)
        {
            var body = new Body();
            body.Position = new tainicom.Aether.Physics2D.Common.Vector2(position.X, position.Y);

            return new Rigidbody(body);
        }
    }
}
