using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI.Texture.Renderer
{
    internal class SlicedTexture3x1Renderer : ITextureRenderer
    {
        private SlicedTexture3x1 _texture;

        public Point TileSize { get; set; }

        public SlicedTexture3x1Renderer(ITexture texture)
        {
            SetTexture(texture);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color colour)
        {
            if (destination.Width < 2 * TileSize.X) throw new UIException("Panel must be wider than 2x the tile width");

            DrawCorners(spriteBatch, destination, colour);
            DrawBorder(spriteBatch, destination, colour);
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
                sourceRectangle: GetSourceRectangle(_texture.Left));

            // Top right
            spriteBatch.Draw(_texture.Tileset.Texture,
                new Rectangle(destination.X + destination.Width - TileSize.X, destination.Y, TileSize.X, TileSize.Y),
                color: colour,
                sourceRectangle: GetSourceRectangle(_texture.Right));
        }

        private void DrawBorder(SpriteBatch spriteBatch, Rectangle destination, Color colour)
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
                        new Rectangle(destination.X + x, destination.Y, TileSize.X, destination.Height),
                        color: colour,
                        sourceRectangle: GetSourceRectangle(_texture.Centre));
                    remainingWidth -= TileSize.X;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(_texture.Centre);
                    var amountShort = TileSize.X - remainingWidth;
                    sourceRect.Width -= amountShort;

                    spriteBatch.Draw(_texture.Tileset.Texture,
                        new Rectangle(destination.X + x, destination.Y, remainingWidth, destination.Height),
                        color: colour,
                        sourceRectangle: sourceRect);

                    remainingWidth = 0;
                }

                x += TileSize.X;
            }
        }

        public void SetTexture(ITexture texture)
        {
            if (texture is SlicedTexture3x1 tex)
            {
                _texture = tex;
                TileSize = new Point(tex.Tileset.TileWidth, tex.Tileset.TileHeight);
            }
            else throw new UIException("Incorrect texture type has been provided.");
        }
    }
}
