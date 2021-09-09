using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Common;

namespace JEng.Content.Pipeline.Graphics
{
    internal class TextureManager
    {
        private ContentProcessorContext _context;
        private Dictionary<string, Texture2DContent> _textures;

        public TextureManager(ContentProcessorContext context)
        {
            _context = context;
            _textures = new Dictionary<string, Texture2DContent>();
        }

        public Texture2DContent Get(string name)
        {
            if (_textures.ContainsKey(name)) return _textures[name];

            var texture = _context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(Paths.Tilesets + $"{name}.png"), "TextureProcessor");

            _textures.Add(name, texture);

            return _textures[name];
        }

        public Texture2DContent GetTilesetWithPadding(string name, int tileWidth, int tileHeight)
        {
            if (_textures.ContainsKey(name + "_padded")) return _textures[name + "_padded"];

            var texture = _context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(Paths.Tilesets + $"{name}.png"), "TextureProcessor");

            var tilesWide = texture.Mipmaps[0].Width / tileWidth;
            var tilesHigh = texture.Mipmaps[0].Height / tileHeight;

            PixelBitmapContent<Color> enlargedTileset = new PixelBitmapContent<Color>((tileWidth + 2) * tilesWide, (tileHeight + 2) * tilesHigh);

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    var sourceRect = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                    BitmapContent newTile = new PixelBitmapContent<Color>(tileWidth + 2, tileHeight + 2);

                    // Assemble new tile
                    BitmapContent.Copy(texture.Mipmaps[0], sourceRect, newTile, new Rectangle(1, 1, tileWidth, tileHeight));

                    // do some more stuff

                    var newTilesetDest = new Rectangle(x * (tileWidth + 2), y * (tileHeight + 2), tileWidth + 2, tileHeight + 2);
                    BitmapContent.Copy(newTile, new Rectangle(0, 0, tileWidth + 2, tileHeight + 2), enlargedTileset, newTilesetDest);
                }
            }

            texture.Mipmaps[0] = enlargedTileset;

            _textures.Add(name + "_padded", texture);

            return _textures[name + "_padded"];
        }
    }
}
