using JEng.Core.TiledMap;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JEng.Engine.Utilities
{
    public class TilesetUtility
    {
        private TiledMapTileset[] _tilesets;

        public TilesetUtility(TiledMapTileset[] tilesets)
        {
            _tilesets = tilesets;
        }

        public Texture2D GetTile(int id)
        {
            var tileset = _tilesets.Last(x => id - x.StartId > 0);
            return tileset.Tiles[id - tileset.StartId];
        }
    }
}
