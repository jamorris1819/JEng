using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Animations.Processed;
using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Core.Graphics;
using System.Collections.Generic;
using System.Linq;
using JEng.Content.Pipeline.Data.Tilesets;
using JEng.Core.Tilesets;

namespace JEng.Content.Pipeline.Readers
{
    public class AnimationReader : ContentTypeReader<AnimationSet>
    {
        GraphicsDeviceManager _graphics;

        protected override AnimationSet Read(ContentReader input, AnimationSet existingInstance)
        {
            _graphics = (GraphicsDeviceManager)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceManager));

            string id = input.ReadString();
            string category = input.ReadString();
            var animations = input.ReadObject<ProcessedAnimationData[]>();
            var tilesets = input.ReadObject<Dictionary<string, Tileset>>();

            var set = new AnimationSet();

            var finalTilesets = new Dictionary<string, Tileset>();

            foreach(var tileset in tilesets)
            {
                finalTilesets.Add(tileset.Key, tileset.Value);
            }

            set.SetTilesets(finalTilesets);

            var finalAnimations = animations.Select(x => CreateAnimation(set, x)).ToArray();
            set.SetAnimations(finalAnimations);

            return set;
        }

        private Animation CreateAnimation(AnimationSet parent, ProcessedAnimationData data)
        {
            var rects = new Rectangle[data.Length];
            var tileset = parent.Tilesets[data.TilesetName];

            for(int i = 0; i < rects.Length; i++)
            {
                var offset = data.Direction == "right" ? new Point(1, 0) : new Point(0, 1);
                rects[i] = new Rectangle(
                    (data.X + offset.X * i) * tileset.TileWidth,
                    (data.Y + offset.Y * i) * tileset.TileHeight,
                    tileset.TileWidth,
                    tileset.TileHeight);
            }

            return new Animation(parent, data.AnimationName, rects, data.Delay)
            {
                TilesetId = data.TilesetName
            };
        }

        private Texture2D CreateImageFromData(ProcessedTexture data)
        {
            var texture = new Texture2D(_graphics.GraphicsDevice, data.Width, data.Height, false, data.Format);
            texture.SetData(data.Data);

            return texture;
        }

        private Tileset CreateTileset(ProcessedTilesetData data)
            => new Tileset(CreateImageFromData(data.Texture), data.TilesWide, data.TilesHigh);
    }
}