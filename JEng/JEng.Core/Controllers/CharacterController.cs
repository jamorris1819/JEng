﻿using Microsoft.Xna.Framework;
using JEng.Core.Components;
using JEng.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.Controllers
{
    public abstract class CharacterController : ICharacterController
    {
        private CharacterAction _action;
        private CharacterDirection _direction;
        public RigidbodyComponent RigidbodyComponent { get; set; }
        public float Speed { get; set; } = 60.0f;

        public CharacterDirection GetDirection() => _direction;
        public CharacterAction GetAction() => _action;

        public abstract void Update(GameTime gameTime);

        protected void Move(Vector2 vec)
        {
            if(Math.Abs(vec.X) > 0) _direction = Math.Sign(vec.X) > 0 ? CharacterDirection.Right : CharacterDirection.Left;
            else if (Math.Abs(vec.Y) > 0) _direction = Math.Sign(vec.Y) >= 0 ? CharacterDirection.Down : CharacterDirection.Up;

            _action = vec == Vector2.Zero ? CharacterAction.Stand : CharacterAction.Walk;

            if (vec != Vector2.Zero)
            {
                vec.Normalize();
                //RigidbodyComponent.Body.ApplyForce(vec * 600.0f);
                var signedVec = new Vector2(Math.Sign(vec.X), Math.Sign(vec.Y));
                signedVec.Normalize();
                RigidbodyComponent.Body.SetVelocity(signedVec * 100.0f);
            }
            else RigidbodyComponent.Body.SetVelocity(new Vector2(0, 0));
        }
    }
}
