using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled.Processed
{
    public class ProcessedTiledMapData
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public ProcessedTiledMapLayerData[] Layers { get; set; }
        public ProcessedTiledMapTileData[] Tiles { get; set; }
        public ProcessedTiledMapTilesetData[] Tilesets { get; set; }
        public ProcessedTiledMapLayerObjectData[] Objects { get; set; }
    }
}
