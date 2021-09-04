using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.TiledMap
{
    public class TiledMap
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public TiledMapLayer[] Layers { get; set; }
        public TiledMapTile[] Tiles { get; set; }
        public TiledMapTileset[] Tilesets { get; set; }
        public TiledMapTransition[] Transitions { get; set; }
    }
}
