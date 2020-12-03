// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary.Components
{
    public class Camera2D
    {
        protected float zoom;
        protected Matrix transform;
        protected Matrix inverseTransform;
        protected Vector2 position;
        protected float rotation;
        protected Viewport viewport;

        /// <summary>
        /// Camera zoom scale.
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        /// <summary>
        /// Camera View Matrix Property.
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates.
        /// from its object coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return inverseTransform; }
        }

        /// <summary>
        /// Camera position.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Camera rotation.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Camera2D(Viewport viewport)
        {
            zoom = 1f;
            rotation = 0f;
            position = Vector2.Zero;
            this.viewport = viewport;
        }

        public void Update(GameTime gameTime)
        {
            MathHelper.Clamp(zoom, 0.01f, 10.0f);
            rotation = ClampAngle(rotation);
            transform = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(position.X, position.Y, 0);
            inverseTransform = Matrix.Invert(transform);
        }

        /// <summary>
        /// Clamps a radian value between -pi and pi
        /// </summary>
        /// <param name="radians">angle to be clamped</param>
        /// <returns>clamped angle</returns>
        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public void Target(Vector2 position)
        {
            //position *= zoom;
            //position.X += viewport.Width / 2;
            //position.Y += viewport.Height / 2;
            this.position = Vector2.Lerp(this.position, position, 0.2f);
        }
    }
}
