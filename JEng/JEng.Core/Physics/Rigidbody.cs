using JEng.Core.Physics.Colliders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics
{
    public class Rigidbody
    {
        private Body _body;
        private float _friction;
        private float _restitution;

        internal Body Body => _body;

        public float Density { get; set; }

        public Vector2 Offset { get; set; }

        public object Tag
        {
            get => Collider.Fixture.Tag;
            set => Collider.Fixture.Tag = value;
        }

        public float Rotation
        {
            get => _body.Rotation;
            set => _body.Rotation = value;
        }

        public Vector2 Position
        {
            get => Physics.ConvertVector(_body.Position);
            set => _body.Position = Physics.ConvertVector(value);
        }

        public Vector2 LinearVelocity
        {
            get => Physics.ConvertVector(_body.LinearVelocity);
            set => _body.LinearVelocity = Physics.ConvertVector(value);
        }

        public Collider Collider { get; set; }

        public Collider Sensor { get; set; }

        public float Friction
        {
            get => _friction;
            set
            {
                _body.SetFriction(value);
                _friction = value;
            }
        }

        public float Restitution
        {
            get => _restitution;
            set
            {
                _body.SetRestitution(value);
                _restitution = value;
            }
        }

        public float LinearDrag
        {
            get => _body.LinearDamping;
            set => _body.LinearDamping = value;
        }

        public float AngularDrag
        {
            get => _body.AngularDamping;
            set => _body.AngularDamping = value;
        }

        public RigidbodyType Type
        {
            get => (RigidbodyType)Enum.Parse(typeof(RigidbodyType), _body.BodyType.ToString());
            set => _body.BodyType = (BodyType)Enum.Parse(typeof(BodyType), value.ToString());
        }

        public Rigidbody(Body body)
        {
            _body = body;
        }

        public Rigidbody(Body body, Collider collider)
        {
            _body = body;
            Collider = collider;
            Collider.ConstructCollider(this);
        }

        public void ApplyForce(Vector2 vec) => ApplyForce(vec.X, vec.Y);

        public void ApplyForce(float x, float y) => _body.ApplyLinearImpulse(new tainicom.Aether.Physics2D.Common.Vector2(x, y));

        public void SetVelocity(Vector2 vec)
        {
            _body.LinearVelocity = new tainicom.Aether.Physics2D.Common.Vector2(vec.X, vec.Y);
        }

        public Vector2 GetVelocity()
            => new Vector2(_body.LinearVelocity.X, _body.LinearVelocity.Y);

        public void SetLinearDamping(float f)
            => _body.LinearDamping = f;

        public void CreateSensor(float radius)
        {
            Sensor = new CircleCollider(radius);
            Sensor.IsSensor = true;
            Sensor.ConstructCollider(this);
            Sensor.Fixture.Tag = Tag;
        }
    }
}
