using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.TiledMap
{
    public class TiledMapTileset
    {
        public int StartId { get; set; }
        public Texture2D[] Tiles { get; private set; }

        public TiledMapTileset(Texture2D[] tiles)
        {
            Tiles = tiles;
        }
    }
}
