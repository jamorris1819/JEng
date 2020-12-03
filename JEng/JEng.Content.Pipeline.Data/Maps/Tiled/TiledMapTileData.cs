using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapTileData
    {
        public int Id { get; set; }
        public TiledMapTilePropertyData[] Properties { get; set; }
        public TiledMapTileAnimationFrameData[] Animation { get; set; }
        public TiledMapTileObjectGroupData ObjectGroup { get; set; }
    }
}
