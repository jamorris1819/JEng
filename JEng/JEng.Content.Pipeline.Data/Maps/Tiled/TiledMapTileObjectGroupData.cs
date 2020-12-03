using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapTileObjectGroupData
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public TiledMapTileObjectData[] Objects { get; set; }
    }
}
