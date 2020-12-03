using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled.Processed
{
    public class ProcessedTiledMapTileData
    {
        public int Id { get; set; }
        public ProcessedTiledMapTilePropertyData[] Properties { get; set; }
        public int[] Animation { get; set; }
        public Vector2[] Collider { get; set; }
    }
}
