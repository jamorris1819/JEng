using JEng.Core.Tilesets;
using Microsoft.Xna.Framework;

namespace JEng.Engine.UI.Texture
{
    public class SlicedTexture3x3 : ITexture
    {
        public Tileset Tileset { get; }

        public TextureType Type =>  TextureType.Sliced3x3;

        public Point TopLeft { get; set; }
        public Point TopCentre { get; set; }
        public Point TopRight { get; set; }
        public Point Left { get; set; }
        public Point Centre { get; set; }
        public Point Right { get; set; }
        public Point BottomLeft { get; set; }
        public Point BottomCentre { get; set; }
        public Point BottomRight { get; set; }

        public SlicedTexture3x3(Tileset tileset)
        {
            Tileset = tileset;
        }
    }
}
