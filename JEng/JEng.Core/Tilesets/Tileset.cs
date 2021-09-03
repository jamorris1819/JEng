using Microsoft.Xna.Framework.Graphics;

namespace JEng.Core.Tilesets
{
    public class Tileset
    {
        public Texture2D Texture { get; }
        public int ImageHeight => Texture.Height;
        public int ImageWidth => Texture.Width;
        public int TileHeight => ImageHeight / TilesHigh;
        public int TileWidth => ImageWidth / TilesWide;
        public int TilesHigh { get; }
        public int TilesWide { get; }

        public Tileset(Texture2D image, int tilesWide, int tilesHigh)
        {
            Texture = image;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
        }
    }
}
