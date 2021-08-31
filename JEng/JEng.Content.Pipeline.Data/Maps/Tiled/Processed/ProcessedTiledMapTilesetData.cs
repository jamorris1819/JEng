using JEng.Content.Pipeline.Data.Textures.Processed;

namespace JEng.Content.Pipeline.Data.Maps.Tiled.Processed
{
    public class ProcessedTiledMapTilesetData
    {
        public int StartId { get; set; }
        public ProcessedTexture Tileset { get; private set; }
        public int TilesetHeight { get; set; }
        public int TilesetWidth { get; set; }
        public int TilesHigh { get; set; }
        public int TilesWide { get; set; }

        public ProcessedTiledMapTilesetData() : this(null) { }

        public ProcessedTiledMapTilesetData(ProcessedTexture tileset)
        {
            Tileset = tileset;
        }
    }
}
