using JEng.Core.Tilesets;
using Microsoft.Xna.Framework;

namespace JEng.Engine.UI.Texture
{
    public class SlicedTexture3x1 : ITexture
    {
        public Tileset Tileset { get; }
        public TextureType Type => TextureType.Sliced3x1;

        public Point Left { get; set; }
        public Point Centre { get; set; }
        public Point Right { get; set; }

        public SlicedTexture3x1(Tileset tileset)
        {
            Tileset = tileset;
        }
    }
}
