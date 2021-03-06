﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapTileObjectData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public TiledMapTilePolygonPointData[] Polygon { get; set; }
    }
}
