using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Maps.Tiled.Processed;
using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Core.TiledMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JEng.Content.Pipeline.Readers
{
    public class TiledMapReader : ContentTypeReader<TiledMap>
    {
        GraphicsDeviceManager _graphics;

        protected override TiledMap Read(ContentReader input, TiledMap existingInstance)
        {
            _graphics = (GraphicsDeviceManager)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceManager));

            var layers = input.ReadObject<ProcessedTiledMapLayerData[]>();
            var tiles = input.ReadObject<ProcessedTiledMapTileData[]>();
            var tilesets = input.ReadObject<ProcessedTiledMapTilesetData[]>();


            var tileWidth = input.ReadInt32();
            var tileHeight = input.ReadInt32();
            var width = input.ReadInt32();
            var height = input.ReadInt32();

            var tiledMap = new TiledMap
            {
                Layers = layers.Select(ConvertLayer).ToArray(),
                Tiles = tiles.Select(ConvertTile).ToArray(),
                Tilesets = tilesets.Select(ConvertTileset).ToArray(),
                TileHeight = tileHeight,
                TileWidth = tileWidth,
                Width = width,
                Height = height
            };

            return tiledMap;
        }

        private TiledMapLayer ConvertLayer(ProcessedTiledMapLayerData layer)
            => new TiledMapLayer
            {
                Name = layer.Name,
                Data = layer.Data
            };

        private TiledMapTile ConvertTile(ProcessedTiledMapTileData tile)
        {
            var newTile = new TiledMapTile
            {
                Animation = tile.Animation,
                AnimationSpeed = tile.AnimationSpeed,
                Id = tile.Id,
                Properties = tile.Properties?.Select(ConvertProperty).ToArray(),
                Collider = tile.Collider
            };

            // Add middle frame at the end for smooth animations
            if(newTile.Animation?.Length == 3)
            {
                var newAnimation = new int[4];
                for (int i = 0; i < 3; i++) newAnimation[i] = tile.Animation[i];
                newAnimation[3] = newAnimation[1];
                newTile.Animation = newAnimation;
            }

            return newTile;
        }

        private TiledMapTileProperty ConvertProperty(ProcessedTiledMapTilePropertyData property)
            => new TiledMapTileProperty
            {
                Name = property.Name,
                Value = property.Value,
                Type = property.Type
            };

        private TiledMapTileset ConvertTileset(ProcessedTiledMapTilesetData data)
        {
            var tileset = CreateImageFromData(data.Tileset);

            return new TiledMapTileset(tileset)
            {
                StartId = data.StartId,
                TilesWide = data.TilesWide,
                TilesHigh = data.TilesHigh,
                TilesetHeight = data.TilesetHeight,
                TilesetWidth = data.TilesetWidth
            };
        }

        private Texture2D CreateImageFromData(ProcessedTexture data)
        {
            var texture = new Texture2D(_graphics.GraphicsDevice, data.Width, data.Height, false, data.Format);
            texture.SetData(data.Data);

            return texture;
        }
    }
}
