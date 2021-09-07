using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Content.Pipeline.Data.Tilesets;
using JEng.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Tileset Processor - Custom")]
    public class TilesetProcessor : ContentProcessor<TilesetData, ProcessedTilesetData>
    {
        public override ProcessedTilesetData Process(TilesetData input, ContentProcessorContext context)
        {
            var textureManager = new TextureManager(context);
            var texture = textureManager.Get(input.Location);
            texture.Faces[0][0].TryGetFormat(out SurfaceFormat format);
            var processedTexture = new ProcessedTexture()
            {
                Width = input.TilesetWidth,
                Height = input.TilesetHeight,
                Data = texture.Mipmaps[0].GetPixelData(),
                Format = format
            };

            return new ProcessedTilesetData(input)
            {
                Texture = processedTexture
            };
        }
    }
}
