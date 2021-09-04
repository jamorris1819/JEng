using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Maps.Tiled;
using JEng.Content.Pipeline.Data.Maps.Tiled.Processed;
using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Content.Pipeline.Data.Tilesets;
using JEng.Content.Pipeline.Graphics;
using System;
using System.Linq;

namespace JEng.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Tiled Map Processor - Custom")]
    public class TiledMapProcessor : ContentProcessor<TiledMapData, ProcessedTiledMapData>
    {
        public override ProcessedTiledMapData Process(TiledMapData input, ContentProcessorContext context)
        {
            var drawLayers = input.Layers.Where(x => x.Data != null);
            var transitionLayer = input.Layers.First(x => x.Name == "Transitions");
            var tiledMap = new ProcessedTiledMapData
            {
                Layers = drawLayers.Select(ConvertLayer).ToArray(),
                Tiles = input.Tilesets.Where(x => x.Tiles != null).Select(ConvertTilesFromTileset).SelectMany(x => x).ToArray(),
                Tilesets = input.Tilesets.Select(x => ConvertTileset(x, context)).ToArray(),
                Objects = transitionLayer.Objects.Select(x => new ProcessedTiledMapLayerObjectData(x) { Name = "Transition" }).ToArray(),
                Height = input.Height,
                Width = input.Width,
                TileHeight = input.TileHeight,
                TileWidth = input.TileWidth
            };

            return tiledMap;
        }

        private ProcessedTiledMapLayerData ConvertLayer(TiledMapLayerData data)
            => new ProcessedTiledMapLayerData
            {
                Name = data.Name,
                Data = data.Data            
            };

        private ProcessedTiledMapTileData ConvertTiles(TiledMapTileData data, int baseId)
        {
            var tiledMapTile = new ProcessedTiledMapTileData();
            tiledMapTile.Id = data.Id + baseId;
            tiledMapTile.Properties = data.Properties?.Select(ConvertTileProperty).ToArray();
            tiledMapTile.Animation = data.Animation?.Select(x => x.TileId + baseId).ToArray();
            tiledMapTile.AnimationSpeed = data.Animation?.Select(x => x.Duration).First() ?? 0;

            var objectData = data.ObjectGroup?.Objects.FirstOrDefault();
            if (objectData != null) {
                var polygonData = objectData?.Polygon.Select(ConvertPolygonDataPoint);
                tiledMapTile.Collider = polygonData.Select(x => AlignPolygonData(x, objectData)).ToArray();
            }

            return tiledMapTile;
        }

        private Vector2 ConvertPolygonDataPoint(TiledMapTilePolygonPointData data)
            => new Vector2
            {
                X = data.X,
                Y = data.Y
            };

        private Vector2 AlignPolygonData(Vector2 vec, TiledMapTileObjectData data)
            => new Vector2
            {
                X = vec.X + data.X,
                Y = vec.Y + data.Y
            };

        private ProcessedTiledMapTileData[] ConvertTilesFromTileset(TiledMapTilesetData data)
        {
            return data.Tiles?.Select(x => ConvertTiles(x, data.FirstGID)).Where(x => !(x.Properties == null && x.Animation == null && x.Collider == null)).ToArray();
        }

        private ProcessedTiledMapTilePropertyData ConvertTileProperty(TiledMapTilePropertyData data)
            => new ProcessedTiledMapTilePropertyData
            {
                Name = data.Name,
                Value = data.Value
            };

        private ProcessedTiledMapTilesetData ConvertTileset(TiledMapTilesetData data, ContentProcessorContext context)
        {
            var texture = LoadTexture(data.Image, context);
            var tileset = ConvertTilesetData(data);
            texture.Faces[0][0].TryGetFormat(out SurfaceFormat format);

            var processedTexture = new ProcessedTexture
            {
                Data = texture.Mipmaps[0].GetPixelData(),
                Width = tileset.TilesetWidth,
                Height = tileset.TilesetHeight,
                Format = format
            };

            var processedTileset = new ProcessedTiledMapTilesetData(processedTexture)
            {
                TilesWide = tileset.TilesWide,
                TilesHigh = tileset.TilesHigh,
                TilesetHeight = tileset.TilesetHeight,
                TilesetWidth = tileset.TilesetWidth
            };

            processedTileset.StartId = data.FirstGID;

            return processedTileset;
        }

        private ProcessedTexture[] SliceTilesheet(Texture2DContent texture, TilesetData tileset)
        {
            var frameReader = new FrameReader(texture, tileset);

            var textures = new ProcessedTexture[tileset.TilesWide * tileset.TilesHigh];

            for (int y = 0; y < tileset.TilesHigh; y++)
            {
                for(int x = 0; x < tileset.TilesWide; x++)
                {
                    var frame = frameReader.GetFrame(x, y);
                    frame.Faces[0][0].TryGetFormat(out SurfaceFormat format);
                    textures[x + y * tileset.TilesWide] = new ProcessedTexture
                    {
                        Data = frame.Mipmaps[0].GetPixelData(),
                        Width = tileset.TilesetWidth / tileset.TilesWide,
                        Height = tileset.TilesetHeight / tileset.TilesHigh,
                        Format = format
                    };
                }
            }

            return textures;
        }

        private TilesetData ConvertTilesetData(TiledMapTilesetData data)
            => new TilesetData
            {
                TilesetHeight = data.ImageHeight,
                TilesetWidth = data.ImageWidth,
                TilesHigh = data.ImageHeight / data.TileHeight,
                TilesWide = data.ImageWidth / data.TileWidth
            };

        private Texture2DContent LoadTexture(string location, ContentProcessorContext context)
        {
            var filename = location.Split('/').Last().Split('.').First();
            TextureManager textureManager = new TextureManager(context);
            return textureManager.Get("world\\" + filename);
        }
    }
}