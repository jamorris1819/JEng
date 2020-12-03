using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapTilesetData
    {
        public int FirstGID { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public string Image { get; set; }
        public TiledMapTileData[] Tiles { get; set; }
    }
}
