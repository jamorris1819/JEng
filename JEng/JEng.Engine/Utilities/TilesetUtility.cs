using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JEng.Core;
using JEng.Core.Interfaces;
using JEng.Core.TiledMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
