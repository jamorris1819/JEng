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
using System.Collections.Generic;

namespace JEng.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Tiled Map Processor - Custom")]
    public class TiledMapProcessor : ContentProcessor<TiledMapData, ProcessedTiledMapData>
    {
        public override ProcessedTiledMapData Process(TiledMapData input, ContentProcessorContext context)
        {
            var drawLayers = GetDrawableLayers(input.Layers);
            var transitionLayer = input.Layers.FirstOrDefault(x => x.Name == "Transitions");
            var tiledMap = new ProcessedTiledMapData
            {
                Layers = drawLayers.Select(ConvertLayer).ToArray(),
                Tiles = input.Tilesets.Where(x => x.Tiles != null).Select(ConvertTilesFromTileset).SelectMany(x => x).ToArray(),
                Tilesets = input.Tilesets.Select(x => ConvertTileset(x, context)).ToArray(),
                Objects = transitionLayer?.Objects.Select(x => new ProcessedTiledMapLayerObjectData(x) { Name = "Transition" }).ToArray() ?? new ProcessedTiledMapLayerObjectData[0],
                Height = input.Height,
                Width = input.Width,
                TileHeight = input.TileHeight,
                TileWidth = input.TileWidth
            };

            return tiledMap;
        }

        private IEnumerable<TiledMapLayerData> GetDrawableLayers(IEnumerable<TiledMapLayerData> data)
        {
            foreach (var layer in data)
            {
                if (layer.Data is null && layer.Layers is null) continue;

                if (layer.Layers is null) yield return layer;
                else
                {
                    var childLayers = UnpackChildLayers(layer);

                    foreach (var childLayer in childLayers)
                    {
                        yield return childLayer;
                    }
                }
            }
        }

        private IEnumerable<TiledMapLayerData> UnpackChildLayers(TiledMapLayerData data)
        {
            var childLayers = data.Layers;

            foreach(var child in childLayers)
            {
                if (child.Layers is null) yield return child;
                else
                {
                    var grandchildLayers = UnpackChildLayers(child);
                    foreach (var grandchild in grandchildLayers)
                    {
                        yield return grandchild;
                    }
                }
            }
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
            var texture = LoadTexture(data.Image, context, data.TileWidth, data.TileHeight);
            var tileset = ConvertTilesetData(data);
            texture.Faces[0][0].TryGetFormat(out SurfaceFormat format);

            var tilesWide = data.ImageWidth / data.TileWidth;
            var tilesHigh = data.ImageHeight / data.TileHeight;

            var paddedWidth = (data.TileWidth + 2) * tilesWide;
            var paddedHeight = (data.TileHeight + 2) * tilesHigh;

            var processedTexture = new ProcessedTexture
            {
                Data = texture.Mipmaps[0].GetPixelData(),
                Width = paddedWidth,
                Height = paddedHeight,
                Format = format
            };

            var processedTileset = new ProcessedTiledMapTilesetData(processedTexture)
            {
                TilesWide = tileset.TilesWide,
                TilesHigh = tileset.TilesHigh,
                TilesetHeight = paddedHeight,
                TilesetWidth = paddedWidth
            };

            processedTileset.StartId = data.FirstGID;

            return processedTileset;
        }

        private TilesetData ConvertTilesetData(TiledMapTilesetData data)
            => new TilesetData
            {
                TilesetHeight = data.ImageHeight,
                TilesetWidth = data.ImageWidth,
                TilesHigh = data.ImageHeight / data.TileHeight,
                TilesWide = data.ImageWidth / data.TileWidth
            };

        private Texture2DContent LoadTexture(string location, ContentProcessorContext context, int tileWidth, int tileHeight)
        {
            var filename = location.Split('/').Last().Split('.').First();
            TextureManager textureManager = new TextureManager(context);
            return textureManager.GetTilesetWithPadding("world\\" + filename, tileWidth, tileHeight);
        }
    }
}