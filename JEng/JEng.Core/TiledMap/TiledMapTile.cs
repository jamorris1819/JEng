using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.TiledMap
{
    public class TiledMapTile
    {
        public int Id { get; set; }
        public TiledMapTileProperty[] Properties { get; set; }
        public int[] Animation { get; set; }
        public int AnimationSpeed { get; set; }
        public float AnimationTimer { get; set; }
        public int Index { get; set; }
        public Vector2[] Collider { get; set; }
    }
}
