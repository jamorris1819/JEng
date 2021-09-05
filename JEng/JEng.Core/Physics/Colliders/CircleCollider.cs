using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics.Colliders
{
    public class CircleCollider : Collider
    {
        public float _radius;

        public CircleCollider(float radius)
        {
            _radius = radius;
        }

        public override void ConstructCollider(Rigidbody rigidbody)
        {
            Fixture = rigidbody.Body.CreateCircle(_radius, rigidbody.Density, Physics.ConvertVector(Offset));
            Fixture.Body.SetIsSensor(IsSensor);
            Fixture.Body.OnCollision += (sender, other, contact) =>
            {
                var data = new CollisionData
                {
                    Sender = sender,
                    Other = other,
                    Contact = contact
                };
                InvokeCollision(data);
                return true;
            };
            Fixture.Body.OnSeparation += (sender, other, contact) =>
            {
                var data = new CollisionData
                {
                    Sender = sender,
                    Other = other,
                    Contact = contact
                };
                InvokeSeparation(data);
            };
        }
    }
}
