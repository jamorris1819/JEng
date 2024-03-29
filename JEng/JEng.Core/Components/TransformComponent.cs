﻿using Microsoft.Xna.Framework;

namespace JEng.Core.Components
{
    public class TransformComponent
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public TransformComponent(Vector2 pos) { Position = pos; }

        public TransformComponent Parent { get; set; }

        public Vector2 GetWorldPosition()
            => Position + (Parent?.GetWorldPosition() ?? Vector2.Zero);

        public float GetWorldRotation()
            => Rotation += (Parent?.GetWorldRotation() ?? 0.0f);
    }
}
