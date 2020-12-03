﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Physics
{
    public class Rigidbody
    {
        private Body _body;

        public Vector2 Position
        {
            get
            {
                var pos = _body.Position;
                return new Vector2(pos.X, pos.Y);
            }
        }

        public Rigidbody(Body body)
        {
            _body = body;
        }

        public void ApplyForce(Vector2 vec) => ApplyForce(vec.X, vec.Y);

        public void ApplyForce(float x, float y)
        {
            _body.ApplyLinearImpulse(new tainicom.Aether.Physics2D.Common.Vector2(x, y));
            //_body.
            //SetVelocity(new Vector2(_body.LinearVelocity.X, _body.LinearVelocity.Y) * new Vector2(x, y))
        }

        public void SetVelocity(Vector2 vec)
            => _body.LinearVelocity = new tainicom.Aether.Physics2D.Common.Vector2(vec.X, vec.Y);

        public Vector2 GetVelocity()
            => new Vector2(_body.LinearVelocity.X, _body.LinearVelocity.Y);

        public void SetLinearDamping(float f)
            => _body.LinearDamping = f;
    }
}
