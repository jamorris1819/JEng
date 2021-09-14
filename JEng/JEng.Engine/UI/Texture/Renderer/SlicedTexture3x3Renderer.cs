using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Texture.Renderer
{
    internal class SlicedTexture3x3Renderer : ITextureRenderer
    {
        private SlicedTexture3x3 _texture;

        public Point TileSize { get; set; }

        public SlicedTexture3x3Renderer(ITexture texture)
        {
            SetTexture(texture);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color colour)
        {
            if (destination.Width < 2 * TileSize.X) throw new UIException("Panel must be wider than 2x the tile width");
            if (destination.Height < 2 * TileSize.Y) throw new UIException("Panel must be higher than 2x the tile height");

            DrawCorners(spriteBatch, destination, colour);
            DrawBorders(spriteBatch, destination, colour);
            Fill(spriteBatch, destination, colour);
        }

        private Rectangle GetSourceRectangle(Point p)
            => new Rectangle(p.X * _texture.Tileset.TileWidth,
                p.Y * _texture.Tileset.TileHeight,
                _texture.Tileset.TileWidth,
                _texture.Tileset.TileWidth);

        private void DrawCorners(SpriteBatch spriteBatch, Rectangle destination, Color colour)
        {
            // Top left
            spriteBatch.Draw(_texture.Tileset.Texture,
                new Rectangle(destination.X, destination.Y, TileSize.X, TileSize.Y),
                color: colour,
                sourceRectangle: GetSourceRectangle(_texture.TopLeft));

            // Top right
            spriteBatch.Draw(_texture.Tileset.Texture,
                new Rectangle(destination.X + destination.Width - TileSize.X, destination.Y, TileSize.X, TileSize.Y),
                color: colour,
                sourceRectangle: GetSourceRectangle(_texture.TopRight));

            // Bottom left
            spriteBatch.Draw(_texture.Tileset.Texture,
                new Rectangle(destination.X, destination.Y + destination.Height - TileSize.Y, TileSize.X, TileSize.Y),
                color: colour,
                sourceRectangle: GetSourceRectangle(_texture.BottomLeft));

            // Bottom right
            spriteBatch.Draw(_texture.Tileset.Texture,
                new Rectangle(destination.X + destination.Width - TileSize.X, destination.Y + destination.Height - TileSize.Y, TileSize.X, TileSize.Y),
                color: colour,
                sourceRectangle: GetSourceRectangle(_texture.BottomRight));
        }

        private void DrawBorders(SpriteBatch spriteBatch, Rectangle destination, Color colour)
        {
            // Top border
            int remainingWidth = destination.Width - TileSize.X * 2;
            int x = TileSize.X;

            while (remainingWidth > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingWidth >= TileSize.X)
                {
                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + x, destination.Y, TileSize.X, TileSize.Y),
                        color: colour,
                        sourceRectangle: GetSourceRectangle(_texture.TopCentre));
                    remainingWidth -= TileSize.X;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(_texture.TopCentre);
                    var amountShort = TileSize.X - remainingWidth;
                    sourceRect.Width -= amountShort;

                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + x, destination.Y, remainingWidth, TileSize.Y),
                        color: colour,
                        sourceRectangle: sourceRect);

                    remainingWidth = 0;
                }

                x += TileSize.X;
            }

            // Bottom border
            remainingWidth = destination.Width - TileSize.X * 2;
            x = TileSize.X;

            while (remainingWidth > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingWidth >= TileSize.X)
                {
                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + x, destination.Y + destination.Height - TileSize.Y, TileSize.X, TileSize.Y),
                        color: colour,
                        sourceRectangle: GetSourceRectangle(_texture.BottomCentre));
                    remainingWidth -= TileSize.X;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(_texture.BottomCentre);
                    var amountShort = TileSize.X - remainingWidth;
                    sourceRect.Width -= amountShort;

                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + x, destination.Y + destination.Height - TileSize.Y, remainingWidth, TileSize.Y),
                        color: colour,
                        sourceRectangle: sourceRect);

                    remainingWidth = 0;
                }

                x += TileSize.X;
            }

            // Left border
            int remainingHeight = destination.Height - TileSize.Y * 2;
            int y = TileSize.Y;

            while (remainingHeight > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingHeight >= TileSize.Y)
                {
                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X, destination.Y + y, TileSize.X, TileSize.Y),
                        color: colour,
                        sourceRectangle: GetSourceRectangle(_texture.Left));
                    remainingHeight -= TileSize.Y;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(_texture.Left);
                    var amountShort = TileSize.Y - remainingHeight;
                    sourceRect.Height -= amountShort;

                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X, destination.Y + y, TileSize.X, remainingHeight),
                        color: colour,
                        sourceRectangle: sourceRect);

                    remainingHeight = 0;
                }

                y += TileSize.Y;
            }

            // Right border
            remainingHeight = destination.Height - TileSize.Y * 2;
            y = TileSize.Y;

            while (remainingHeight > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingHeight >= TileSize.Y)
                {
                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + destination.Width - TileSize.X, destination.Y + y, TileSize.X, TileSize.Y),
                        color: colour,
                        sourceRectangle: GetSourceRectangle(_texture.Right));
                    remainingHeight -= TileSize.Y;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(_texture.Right);
                    var amountShort = TileSize.Y - remainingHeight;
                    sourceRect.Height -= amountShort;

                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + destination.Width - TileSize.X, destination.Y + y, TileSize.X, remainingHeight),
                        color: colour,
                        sourceRectangle: sourceRect);

                    remainingHeight = 0;
                }

                y += TileSize.Y;
            }
        }

        private void Fill(SpriteBatch spriteBatch, Rectangle destination, Color colour)
        {
            for (int y = TileSize.Y; y < destination.Width - TileSize.Y; y += TileSize.Y)
            {
                for (int x = TileSize.X; x < destination.Width - TileSize.X; x += TileSize.X)
                {
                    var remainingWidth = destination.Width - TileSize.X - x;
                    var remainingHeight = destination.Height - TileSize.Y - y;

                    if (remainingHeight >= TileSize.Y && remainingWidth >= TileSize.X)
                    {
                        spriteBatch.Draw(_texture.Tileset.Texture,
                            new Rectangle(destination.X + x, destination.Y + y, TileSize.X, TileSize.Y),
                            color: colour,
                            sourceRectangle: GetSourceRectangle(_texture.Centre));
                    }
                    else
                    {
                        var tileWidth = remainingWidth > TileSize.X ? TileSize.X : remainingWidth;
                        var tileHeight = remainingHeight > TileSize.Y ? TileSize.Y : remainingHeight;

                        var sourceRect = GetSourceRectangle(_texture.Centre);
                        var amountShortX = remainingWidth < TileSize.X ? TileSize.X - remainingWidth : 0;
                        var amountShortY = remainingHeight < TileSize.Y ? TileSize.Y - remainingHeight : 0;

                        sourceRect.Width -= amountShortX;
                        sourceRect.Height -= amountShortY;

                        spriteBatch.Draw(_texture.Tileset.Texture,
                            new Rectangle(destination.X + x, destination.Y + y, tileWidth, tileHeight),
                            color: colour, sourceRectangle: sourceRect);
                    }
                }
            }
        }

        public void SetTexture(ITexture texture)
        {
            if (texture is SlicedTexture3x3 tex)
            {
                _texture = tex;
                TileSize = new Point(tex.Tileset.TileWidth, tex.Tileset.TileHeight);
            }
            else throw new UIException("Incorrect texture type has been provided.");
        }
    }
}
