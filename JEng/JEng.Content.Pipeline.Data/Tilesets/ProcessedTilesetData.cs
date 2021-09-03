using JEng.Content.Pipeline.Data.Textures.Processed;
using System.Collections.Generic;

namespace JEng.Content.Pipeline.Data.Tilesets
{
    public class ProcessedTilesetData : TilesetData
    {
        public ProcessedTexture Texture { get; set; }

        public ProcessedTilesetData() { }

        public ProcessedTilesetData(TilesetData data)
        {
            Name = data.Name;
            Location = data.Location;
            TilesetWidth = data.TilesetWidth;
            TilesetHeight = data.TilesetHeight;
            TilesWide = data.TilesWide;
            TilesHigh = data.TilesHigh;
        }
    }
}
