using JEng.Core.Tilesets;
using Microsoft.Xna.Framework;

namespace JEng.Engine.UI
{
    public class SlicedTexture
    {
        public Tileset Tileset { get; }

        public Point TopLeft { get; set; }
        public Point TopCentre { get; set; }
        public Point TopRight { get; set; }
        public Point Left { get; set; }
        public Point Centre { get; set; }
        public Point Right { get; set; }
        public Point BottomLeft { get; set; }
        public Point BottomCentre { get; set; }
        public Point BottomRight { get; set; }

        public SlicedTexture(Tileset tileset)
        {
            Tileset = tileset;
        }
    }
}
