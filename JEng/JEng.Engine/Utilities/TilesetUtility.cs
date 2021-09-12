using JEng.Core.TiledMap;
using Microsoft.Xna.Framework;
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

        public (Texture2D, Rectangle) GetTile(int id)
        {
            var tileset = _tilesets.Last(x => id - x.StartId > 0);

            int relativeId = id - tileset.StartId;
            var texture = tileset.Texture;
            var rectangle = GetRectangleLocation(tileset, relativeId);

            // Remove padding
            rectangle.X++;
            rectangle.Y++;
            rectangle.Width -= 2;
            rectangle.Height -= 2;

            return (texture, rectangle);
        }

        private static Rectangle GetRectangleLocation(TiledMapTileset tileset, int i)
        {
            int x = i % tileset.TilesWide;
            int y = i / tileset.TilesWide;

            int tileWidth = tileset.TilesetWidth / tileset.TilesWide;
            int tileHeight = tileset.TilesetHeight / tileset.TilesHigh;

            return new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
        }
    }
}
