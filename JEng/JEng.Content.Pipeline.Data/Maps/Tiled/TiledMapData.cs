using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapData
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public TiledMapTilesetData[] Tilesets { get; set; }
        public TiledMapLayerData[] Layers { get; set; }
    }
}
