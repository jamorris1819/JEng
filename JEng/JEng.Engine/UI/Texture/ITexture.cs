using JEng.Core.Tilesets;

namespace JEng.Engine.UI.Texture
{
    public interface ITexture
    {
        public Tileset Tileset { get; }
        public TextureType Type { get; }
    }
}