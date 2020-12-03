// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
using GameLibrary.Graphics;

namespace GameLibrary.Components
{
    public enum ControlKeys { ArrowKeys, WASD }
    public enum CharacterControllerState { Enabled, Disabled, KeyBlocked }

    public class CharacterController
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected float speed;
        protected float friction;
        protected ControlKeys controlKeys;
        protected CharacterControllerState controllerState;

        /// <summary>
        /// Position of the character controller.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Velocity of the character controller.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// Speed of the character controller.
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Friction of the character controller (between 0 and 1).
        /// </summary>
        public float Friction
        {
            get { return friction; }
            set { friction = MathHelper.Clamp(value, 0, 1); }
        }

        /// <summary>
        /// Current state of the controller.
        /// </summary>
        public CharacterControllerState ControllerState
        {
            get { return controllerState; }
            set { controllerState = value; }
        }

        public CharacterController()
        {
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            speed = 8f;
            friction = 0.1f;
            controlKeys = ControlKeys.ArrowKeys;
            controllerState = CharacterControllerState.Enabled;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (controllerState == CharacterControllerState.Disabled)
                return;
            // Slow down velocity due to friction.
            velocity *= (1f - friction);

            if (velocity.Length() < 0.001f)
                velocity = Vector2.Zero;

            // Binary velocity will give us a base direction
            Vector2 binaryVelocity = new Vector2(0, 0);

            // We'll find out what our binary velocity it.
            // We have different input types, so we'll find out which set.
            if (controllerState != CharacterControllerState.KeyBlocked)
            {
                switch (controlKeys)
                {
                    case ControlKeys.WASD:
                        if (InputHandler.KeyDown(Keys.A))
                            binaryVelocity.X = -1;
                        else if (InputHandler.KeyDown(Keys.D))
                            binaryVelocity.X = 1;

                        if (InputHandler.KeyDown(Keys.W))
                            binaryVelocity.Y = -1;
                        else if (InputHandler.KeyDown(Keys.S))
                            binaryVelocity.Y = 1;

                        if (binaryVelocity.LengthSquared() != 0)
                            binaryVelocity.Normalize();
                        break;
                    case ControlKeys.ArrowKeys:
                        if (InputHandler.KeyDown(Keys.Left))
                            binaryVelocity.X = -1;
                        else if (InputHandler.KeyDown(Keys.Right))
                            binaryVelocity.X = 1;

                        if (InputHandler.KeyDown(Keys.Up))
                            binaryVelocity.Y = -1;
                        else if (InputHandler.KeyDown(Keys.Down))
                            binaryVelocity.Y = 1;

                        if (binaryVelocity.LengthSquared() != 0)
                            binaryVelocity.Normalize();
                        break;
                }
            }

            velocity += binaryVelocity * speed;

            position += velocity;
        }

        /// <summary>
        /// Applies the velocity to the controller.
        /// </summary>
        /// <param name="velocity">Velocity to apply</param>
        /// <param name="obeySpeed">Apply direction with controller's set speed.</param>
        public void ApplyVelocity(Vector2 velocity, bool obeySpeed = false)
        {
            if (obeySpeed)
            {
                velocity.Normalize();
                this.velocity = velocity * speed;
            }
            else
                this.velocity += velocity;
        }
    }
}
