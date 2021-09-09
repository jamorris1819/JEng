using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI.Components
{
    public class Panel : UIComponent
    {
        public SlicedTexture SlicedTexture { get; }

        public Panel(SlicedTexture texture)
        {
            SlicedTexture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: ensure the panel is large enough

            // Top left
            spriteBatch.Draw(SlicedTexture.Tileset.Texture, new Rectangle(16, 16, 16, 16), color: Colour, sourceRectangle: GetSourceRectangle(SlicedTexture.TopLeft));
        }

        public override void Update(GameTime gameTime)
        {

        }

        private Rectangle GetSourceRectangle(Point p)
            => new Rectangle(p.X * SlicedTexture.Tileset.TileWidth,
                p.Y * SlicedTexture.Tileset.TileHeight,
                SlicedTexture.Tileset.TileWidth,
                SlicedTexture.Tileset.TileWidth);
    }
}
