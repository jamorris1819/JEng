using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace JEng.Core.Components
{
    public class CameraComponent
    {
        public bool Active { get; set; }
        public OrthographicCamera Camera { get; set; }
        public Entity Tracking { get; set; }
    }
}
