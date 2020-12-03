using Microsoft.Xna.Framework;

namespace JEng.Core.Components
{
    public class TransformComponent
    {
        public Vector2 Position { get; set; }

        public TransformComponent(Vector2 pos) { Position = pos; }
    }
}
