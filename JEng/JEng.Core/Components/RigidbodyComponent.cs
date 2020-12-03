using Microsoft.Xna.Framework;
using JEng.Core.Physics;

namespace JEng.Core.Components
{
    public class RigidbodyComponent
    {
        public Rigidbody Body { get; set; }
        public Vector2 Offset { get; set; }

        public RigidbodyComponent(Rigidbody body)
        {
            Body = body;
        }
    }
}
