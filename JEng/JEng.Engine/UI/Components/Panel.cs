using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Components
{
    public class Panel : UIComponent
    {
        public SlicedTexture SlicedTexture { get; }

        protected Point TileSize { get; }

        public Panel(SlicedTexture texture)
        {
            SlicedTexture = texture;
            TileSize = new Point(SlicedTexture.Tileset.TileWidth, SlicedTexture.Tileset.TileHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Size.X < 2 * TileSize.X) throw new UIException("Panel must be wider than 2x the tile width");
            if (Size.Y < 2 * TileSize.Y) throw new UIException("Panel must be higher than 2x the tile height");

            DrawCorners(spriteBatch);
            DrawBorders(spriteBatch);
            Fill(spriteBatch);
        }

        private Rectangle GetSourceRectangle(Point p)
            => new Rectangle(p.X * SlicedTexture.Tileset.TileWidth,
                p.Y * SlicedTexture.Tileset.TileHeight,
                SlicedTexture.Tileset.TileWidth,
                SlicedTexture.Tileset.TileWidth);

        private void DrawCorners(SpriteBatch spriteBatch)
        {
            // Top left
            spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                new Rectangle(Position.X, Position.Y, TileSize.X, TileSize.Y),
                color: Colour,
                sourceRectangle: GetSourceRectangle(SlicedTexture.TopLeft));

            // Top right
            spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                new Rectangle(Position.X + Size.X - TileSize.X, Position.Y, TileSize.X, TileSize.Y),
                color: Colour,
                sourceRectangle: GetSourceRectangle(SlicedTexture.TopRight));

            // Bottom left
            spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                new Rectangle(Position.X, Position.Y + Size.Y - TileSize.Y, TileSize.X, TileSize.Y),
                color: Colour,
                sourceRectangle: GetSourceRectangle(SlicedTexture.BottomLeft));

            // Bottom right
            spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                new Rectangle(Position.X + Size.X - TileSize.X, Position.Y + Size.Y - TileSize.Y, TileSize.X, TileSize.Y),
                color: Colour,
                sourceRectangle: GetSourceRectangle(SlicedTexture.BottomRight));
        }

        private void DrawBorders(SpriteBatch spriteBatch)
        {
            // Top border
            int remainingWidth = Size.X - TileSize.X * 2;
            int x = TileSize.X;

            while (remainingWidth > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingWidth >= TileSize.X)
                {
                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + x, Position.Y, TileSize.X, TileSize.Y),
                        color: Colour,
                        sourceRectangle: GetSourceRectangle(SlicedTexture.TopCentre));
                    remainingWidth -= TileSize.X;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(SlicedTexture.TopCentre);
                    var amountShort = TileSize.X - remainingWidth;
                    sourceRect.Width -= amountShort;

                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + x, Position.Y, remainingWidth, TileSize.Y),
                        color: Colour,
                        sourceRectangle: sourceRect);

                    remainingWidth = 0;
                }

                x += TileSize.X;
            }

            // Bottom border
            remainingWidth = Size.X - TileSize.X * 2;
            x = TileSize.X;

            while (remainingWidth > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingWidth >= TileSize.X)
                {
                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + x, Position.Y + Size.Y - TileSize.Y, TileSize.X, TileSize.Y),
                        color: Colour,
                        sourceRectangle: GetSourceRectangle(SlicedTexture.BottomCentre));
                    remainingWidth -= TileSize.X;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(SlicedTexture.BottomCentre);
                    var amountShort = TileSize.X - remainingWidth;
                    sourceRect.Width -= amountShort;

                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + x, Position.Y + Size.Y - TileSize.Y, remainingWidth, TileSize.Y),
                        color: Colour,
                        sourceRectangle: sourceRect);

                    remainingWidth = 0;
                }

                x += TileSize.X;
            }

            // Left border
            int remainingHeight = Size.Y - TileSize.Y * 2;
            int y = TileSize.Y;

            while (remainingHeight > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingHeight >= TileSize.Y)
                {
                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X, Position.Y + y, TileSize.X, TileSize.Y),
                        color: Colour,
                        sourceRectangle: GetSourceRectangle(SlicedTexture.Left));
                    remainingHeight -= TileSize.Y;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(SlicedTexture.Left);
                    var amountShort = TileSize.Y - remainingHeight;
                    sourceRect.Height -= amountShort;

                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X, Position.Y + y, TileSize.X, remainingHeight),
                        color: Colour,
                        sourceRectangle: sourceRect);

                    remainingHeight = 0;
                }

                y += TileSize.Y;
            }

            // Right border
            remainingHeight = Size.Y - TileSize.Y * 2;
            y = TileSize.Y;

            while (remainingHeight > 0)
            {
                // Check if we have enough room to draw a full tile.
                if (remainingHeight >= TileSize.Y)
                {
                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + Size.X - TileSize.X, Position.Y + y, TileSize.X, TileSize.Y),
                        color: Colour,
                        sourceRectangle: GetSourceRectangle(SlicedTexture.Right));
                    remainingHeight -= TileSize.Y;
                }
                else
                {
                    var sourceRect = GetSourceRectangle(SlicedTexture.Right);
                    var amountShort = TileSize.Y - remainingHeight;
                    sourceRect.Height -= amountShort;

                    spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                        new Rectangle(Position.X + Size.X - TileSize.X, Position.Y + y, TileSize.X, remainingHeight),
                        color: Colour,
                        sourceRectangle: sourceRect);

                    remainingHeight = 0;
                }

                y += TileSize.Y;
            }
        }

        private void Fill(SpriteBatch spriteBatch)
        {
            for(int y = TileSize.Y; y < Size.Y - TileSize.Y; y += TileSize.Y)
            {
                for (int x = TileSize.X; x < Size.X - TileSize.X; x += TileSize.X)
                {
                    var remainingWidth = Size.X - TileSize.X - x;
                    var remainingHeight = Size.Y - TileSize.Y - y;

                    if (remainingHeight >= TileSize.Y && remainingWidth >= TileSize.X)
                    {
                        spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                            new Rectangle(Position.X + x, Position.Y + y, TileSize.X, TileSize.Y),
                            color: Colour,
                            sourceRectangle: GetSourceRectangle(SlicedTexture.Centre));
                    }
                    else
                    {
                        var tileWidth = remainingWidth > TileSize.X ? TileSize.X : remainingWidth;
                        var tileHeight = remainingHeight > TileSize.Y ? TileSize.Y : remainingHeight;

                        var sourceRect = GetSourceRectangle(SlicedTexture.Centre);
                        var amountShortX = remainingWidth < TileSize.X ? TileSize.X - remainingWidth : 0;
                        var amountShortY = remainingHeight < TileSize.Y ? TileSize.Y - remainingHeight : 0;

                        sourceRect.Width -= amountShortX;
                        sourceRect.Height -= amountShortY;

                        spriteBatch.Draw(SlicedTexture.Tileset.Texture,
                            new Rectangle(Position.X + x, Position.Y + y, tileWidth, tileHeight),
                            color: Colour, sourceRectangle: sourceRect);
                    }
                }
            }
        }
    }
}
