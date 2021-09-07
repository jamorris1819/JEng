using Microsoft.Xna.Framework.Graphics;

namespace JEng.Core.Tilesets
{
    public class Tileset
    {
        public Texture2D Texture { get; }
        public int ImageHeight { get; }
        public int ImageWidth { get; }
        public int TileHeight { get; }
        public int TileWidth { get; }
        public int TilesHigh { get; }
        public int TilesWide { get; }

        public Tileset(Texture2D image, int tilesWide, int tilesHigh)
        {
            Texture = image;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            ImageHeight = Texture.Height;
            ImageWidth = Texture.Width;
            TileHeight = ImageHeight / TilesHigh;
            TileWidth = ImageWidth / TilesWide;
        }
    }
}
