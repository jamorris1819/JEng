using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Animations;
using JEng.Content.Pipeline.Data.Animations.Processed;
using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Content.Pipeline.Data.Tilesets;
using JEng.Content.Pipeline.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace JEng.Content.Pipeline.Processor
{
    [ContentProcessor(DisplayName = "Animation Processor - Custom")]
    public class AnimationProcessor : ContentProcessor<AnimationSetData, ProcessedAnimationSetData>
    {
        private TextureManager _textureManager;
        public Dictionary<string, Texture2DContent> Textures;

        public override ProcessedAnimationSetData Process(AnimationSetData input, ContentProcessorContext context)
        {
            _textureManager = new TextureManager(context);

            /* var data = new ProcessedAnimationSetData(input)
             {
                 Animations = input.Animations.Where(x => x != null)
                                             .Select(x => LoadAnimation(x, input.Tilesets.First(y => y.Name == x.TilesetName))).ToArray()
             };*/

            var tilesetDictionary = new Dictionary<string, ProcessedTilesetData>();
            var tilesets = ImportTilesets(input.Tilesets).ToArray();

            foreach(var tileset in tilesets)
            {
                tilesetDictionary.Add(tileset.Name, tileset);
            }

            var data = new ProcessedAnimationSetData(input)
            {
                Animations = input.Animations.Where(x => x != null).Select(x => new ProcessedAnimationData(x)).ToArray(),
                Tilesets = tilesetDictionary
            };

            return data;
        }

        private IEnumerable<ProcessedTilesetData> ImportTilesets(IEnumerable<TilesetData> tilesetData)
        {
            foreach(var data in tilesetData)
            {
                var texture = _textureManager.Get(data.Location);
                texture.Faces[0][0].TryGetFormat(out SurfaceFormat format);
                var processedTexture = new ProcessedTexture()
                {
                    Width = data.TilesetWidth,
                    Height = data.TilesetHeight,
                    Data = texture.Mipmaps[0].GetPixelData(),
                    Format = format
                };

                yield return new ProcessedTilesetData(data)
                {
                    Texture = processedTexture
                };
            }
        }

        private ProcessedAnimationData LoadAnimation(AnimationData animation, TilesetData tileset)
        {
            var frames = ExtractFrames(animation, tileset);
            ProcessedTexture[] data = new ProcessedTexture[frames.Length];

            for (int i = 0; i < frames.Length; i++)
            {
                frames[i].Faces[0][0].TryGetFormat(out SurfaceFormat format);
                data[i] = new ProcessedTexture() {
                    Width = tileset.TilesetWidth / tileset.TilesWide,
                    Height = tileset.TilesetHeight / tileset.TilesHigh,
                    Data = frames[i].Mipmaps[0].GetPixelData(),
                    Format = format
                };
            }

            return new ProcessedAnimationData(animation);
        }

        private Texture2DContent[] ExtractFrames(AnimationData animation, TilesetData tileset)
        {
            Texture2DContent texture = _textureManager.Get(tileset.Location);
            FrameReader frameReader = new FrameReader(texture, tileset);

            return Enumerable.Range(0, animation.Length).Select(x => GetFrame(x, animation, frameReader)).ToArray();
        }

        private Texture2DContent GetFrame(int index, AnimationData data, FrameReader frameReader)
        {

            if(data.Direction == "right") return frameReader.GetFrame(data.X + index, data.Y);
            else return frameReader.GetFrame(data.X, data.Y + index);
        }
    }
}
