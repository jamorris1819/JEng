using Microsoft.Xna.Framework;

namespace JEng.Core.TiledMap
{
    public class TiledMapTransition
    {
        public Vector2 Position { get; set; }
        public Vector2[] Polygon { get; set; }
        public string To { get; set; }
    }
}
