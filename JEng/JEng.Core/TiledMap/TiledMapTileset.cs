using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.TiledMap
{
    public class TiledMapTileset
    {
        public int StartId { get; set; }
        public Texture2D Texture { get; private set; }
        public int TilesetHeight { get; set; }
        public int TilesetWidth { get; set; }
        public int TilesHigh { get; set; }
        public int TilesWide { get; set; }


        public TiledMapTileset(Texture2D tileset)
        {
            Texture = tileset;
        }
    }
}
