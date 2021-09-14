using JEng.Core.Tilesets;

namespace JEng.Engine.UI.Texture
{
    public class Texture : ITexture
    {
        public Tileset Tileset { get; }
        public TextureType Type => TextureType.Texture;

        public Texture(Tileset tileset)
        {
            Tileset = tileset;
        }
    }
}
