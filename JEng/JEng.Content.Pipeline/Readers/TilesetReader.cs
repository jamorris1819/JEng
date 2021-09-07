using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Core.Tilesets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Content.Pipeline.Readers
{
    public class TilesetReader : ContentTypeReader<Tileset>
    {
        protected override Tileset Read(ContentReader input, Tileset existingInstance)
        {
            var graphics = (GraphicsDeviceManager)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceManager));

            var name = input.ReadString();
            var location = input.ReadString();
            var tilesetWidth = input.ReadInt32();
            var tilesetHeight = input.ReadInt32();
            var tilesWide = input.ReadInt32();
            var tilesHigh = input.ReadInt32();
            var texture = input.ReadObject<ProcessedTexture>();

            var newTex = new Texture2D(graphics.GraphicsDevice, texture.Width, texture.Height, false, texture.Format);
            newTex.SetData(texture.Data);

            return new Tileset(newTex, tilesWide, tilesHigh);
        }
    }
}
