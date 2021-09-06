using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JEng.Core.Components
{
    public class SpriteComponent
    {
        private Vector2 _originPoint;
        private SpriteOrigin _origin;

        public Texture2D Texture { get; set; }

        public Vector2 OriginPoint {
            get => _originPoint;
            set
            {
                _originPoint = value;
                _origin = SpriteOrigin.Custom;
            }
        }

        public SpriteOrigin Origin
        {
            get => _origin;
            set
            {
                if (value == SpriteOrigin.Custom) return;
                _originPoint = SpriteOriginToVector(Texture, value);
                _origin = value;
            }
        }

        public float Layer { get; set; }

        public SpriteComponent(Texture2D tex) : this(tex, SpriteOrigin.TopLeft) { }

        public SpriteComponent(Texture2D tex, SpriteOrigin origin)
        {
            Texture = tex;
            Origin = origin;
        }

        private static Vector2 SpriteOriginToVector(Texture2D tex, SpriteOrigin origin)
        {
            switch (origin)
            {
                case SpriteOrigin.TopLeft:
                    return new Vector2(0, 0);
                case SpriteOrigin.Top:
                    return new Vector2(tex.Width / 2.0f, 0);
                case SpriteOrigin.TopRight:
                    return new Vector2(tex.Width, 0);
                case SpriteOrigin.CentreLeft:
                    return new Vector2(0, tex.Height / 2.0f);
                case SpriteOrigin.Centre:
                    return new Vector2(tex.Width / 2.0f, tex.Height / 2.0f);
                case SpriteOrigin.CentreRight:
                    return new Vector2(tex.Width, tex.Height / 2.0f);
                case SpriteOrigin.BottomLeft:
                    return new Vector2(0, tex.Height);
                case SpriteOrigin.Bottom:
                    return new Vector2(tex.Width / 2.0f, tex.Height);
                case SpriteOrigin.BottomRight:
                    return new Vector2(tex.Width, tex.Height);
                default:
                    throw new ArgumentOutOfRangeException(nameof(origin));
            }
        }
    }
}
