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
using System.IO;

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
            var objects = input.ReadObject<ProcessedTiledMapLayerObjectData[]>();

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
                Height = height,
                Transitions = objects.Select(ConvertTransition).ToArray()
            };

            // CreatePaddedTileset(tilesets.First());

            var file = File.Create("C:\\Projects\\JEng.RPG\\testimage.png");
            var tex = tiledMap.Tilesets.First().Texture;
            tex.SaveAsPng(file, tex.Width, tex.Height);
            file.Dispose();

            return tiledMap;
        }

        private TiledMapTransition ConvertTransition(ProcessedTiledMapLayerObjectData data)
            => new TiledMapTransition()
            {
                Direction = GetDirection(data.Properties.First(x => x.Name == "direction").Value.ToLower()),
                Polygon = data.Polygon.Select(x => new Vector2(x.X, x.Y)).ToArray(),
                Position = new Vector2(data.X, data.Y),
                To = data.Properties.First(x => x.Name == "to").Value
            };

        private Vector2 GetDirection(string value)
        {
            return value switch
            {
                "left" => new Vector2(-1, 0),
                "right" => new Vector2(1, 0),
                "up" => new Vector2(0, -1),
                "down" => new Vector2(0, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(value)),
            };
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
                Value = property.Value
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

        /*private Texture2D CreatePaddedTileset(ProcessedTiledMapTilesetData data)
        {
            var texture = CreateImageFromData(data.Tileset);
            var tileWidth = data.TilesetWidth / data.TilesWide;
            var tileHeight = data.TilesetHeight / data.TilesHigh;

            
            for(int y = 0; y < data.TilesHigh; y++)
            {
                for(int x = 0; x < data.TilesWide; x++)
                {
                    tiles[x, y] = new Texture2D(_graphics.GraphicsDevice, tileWidth + 2, tileHeight + 2, false, data.Tileset.Format);

                    Color[] topBorder = new Color[tileWidth];
                    texture.GetData(0, new Rectangle(tileWidth * x, tileHeight * y, tileWidth, 1), topBorder, 0, tileWidth);

                    var topBorderTexture = new Texture2D(_graphics.GraphicsDevice, tileWidth, 1, false, data.Tileset.Format);

                    if (x == 1 && y == 1)
                    {
                        var file = File.Create("C:\\Projects\\JEng.RPG\\testimage.png");
                        topBorderTexture.SaveAsPng(file, tileWidth, 1);
                        file.Dispose();
                        topBorderTexture.Dispose();
                    }
                }
            }

            return null;
        }*/
    }
}
