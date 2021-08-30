using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JEng.Core.Physics.Colliders
{
    public class PolygonCollider : Collider
    {
        private Vector2[] _points;

        public PolygonCollider(Vector2[] points)
        {
            _points = points;
        }

        public override void ConstructCollider(Rigidbody rigidbody)
        {
            var verts = new tainicom.Aether.Physics2D.Common.Vertices(_points.Select(Physics.ConvertVector));
            rigidbody.Body.CreatePolygon(verts, rigidbody.Density);
        }
    }
}
