using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using JEng.Content.Pipeline.Data.Tilesets;
using System;

namespace JEng.Content.Pipeline.Graphics
{
    internal class FrameReader
    {
        private BitmapContent _texture;
        private TilesetData _tileset;

        public FrameReader(Texture2DContent tex, TilesetData td)
        {
            _texture = tex.Mipmaps[0];
            _tileset = td;
        }

        public FrameReader(BitmapContent bc, TilesetData td)
        {
            _texture = bc;
            _tileset = td;
        }

        /// <summary>
        /// Extracts the frame at the given coordinates.
        /// </summary>
        public Texture2DContent GetFrame(int x, int y)
        {
            if (x >= _tileset.TilesWide || x < 0) throw new IndexOutOfRangeException(nameof(x));
            if (y >= _tileset.TilesHigh || y < 0) throw new IndexOutOfRangeException(nameof(y));

            int frameWidth = _tileset.TilesetWidth / _tileset.TilesWide;
            int frameHeight = _tileset.TilesetHeight / _tileset.TilesHigh;

            BitmapContent frameBitmapContent = new PixelBitmapContent<Color>(frameWidth, frameHeight);

            Rectangle destination = new Rectangle(0, 0, frameWidth, frameHeight);
            Rectangle source = new Rectangle(frameWidth * x, frameHeight * y, frameWidth, frameHeight);

            BitmapContent.Copy(_texture, source, frameBitmapContent, destination);

            return new Texture2DContent()
            {
                Mipmaps = new MipmapChain(frameBitmapContent)
            };
        }
    }
}
