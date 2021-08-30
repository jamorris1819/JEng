using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.TiledMap;
using JEng.Engine.Utilities;
using System;
using System.Linq;
using MonoGame.Extended.Entities;
using JEng.Core.Components;
using JEng.Core.Physics.Colliders;

namespace JEng.Engine.Systems
{
    public class MapSystem : UpdateSystem, IUpdateSystem, IDrawSystem
    {
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TilesetUtility _tilesetUtility;
        private CameraSystem _cameraSystem;
        private PhysicsSystem _physicsSystem;
        private Vector2 _tileDimensions;

        private TiledMapTile[] _animatedTiles;
        private int _playersLayer;
        private float _mapHeightStep;

        public MapSystem(TiledMap tiledMap, CameraSystem cameraSystem, PhysicsSystem physicsSystem, SpriteBatch spriteBatch)
        {
            _tiledMap = tiledMap;
            _cameraSystem = cameraSystem;
            _spriteBatch = spriteBatch;
            _tileDimensions = new Vector2(_tiledMap.TileWidth, _tiledMap.TileHeight);
            _tilesetUtility = new TilesetUtility(_tiledMap.Tilesets);
            _physicsSystem = physicsSystem;
        }

        public override void Initialize(World world)
        {
            base.Initialize(world);

            _animatedTiles = _tiledMap.Tiles.Where(x => x.Animation?.Length > 0).ToArray();
            _playersLayer = _tiledMap.Layers.ToList().FindIndex(x => x.Name == "Player");
            _mapHeightStep = 0.001f / (_tiledMap.TileHeight * _tiledMap.Height);
        }

        public void BuildColliders(Func<Entity> createEntity)
        {
            var tilesWithCollision = _tiledMap.Tiles.Where(x => x.Collider != null).ToArray();

            for (int i = 0; i < _tiledMap.Layers.Length; i++)
            {
                var layer = _tiledMap.Layers[i];

                for (int j = 0; j < layer.Data.Length; j++)
                {
                    var data = layer.Data[j];

                    if (data == 0) continue;

                    var colliderMatch = tilesWithCollision.FirstOrDefault(x => x.Id == data);
                    if (colliderMatch == null) continue;

                    // Create collider
                    var location = ConvertTo2D(j) * _tileDimensions;
                    var newEntity = createEntity();

                    newEntity.Attach(new Transform(location));
                    newEntity.Attach(_physicsSystem.Physics.CreateRigidbody(location, new PolygonCollider(colliderMatch.Collider)));
                }
            }

            // Build border around map.
            var mapWidth = _tiledMap.Width * _tiledMap.TileWidth;
            var mapHeight = _tiledMap.Height * _tiledMap.TileHeight;

            var horizontalBorder = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 4f),
                new Vector2(mapWidth, 4f),
                new Vector2(mapWidth, 0),
            };
            var topEntity = createEntity();
            topEntity.Attach(new Transform(new Vector2(0, 0.5f)));
            topEntity.Attach(_physicsSystem.Physics.CreateRigidbody(new Vector2(0, 0.5f), new PolygonCollider(horizontalBorder)));

            
            var bottomEntity = createEntity();
            bottomEntity.Attach(new Transform(new Vector2(0, mapHeight)));
            bottomEntity.Attach(_physicsSystem.Physics.CreateRigidbody(new Vector2(0, mapHeight), new PolygonCollider(horizontalBorder)));

            var verticalBorder = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, mapHeight),
                new Vector2(-1, mapHeight),
                new Vector2(-1, 0),
            };
            var leftEntity = createEntity();
            leftEntity.Attach(new Transform(new Vector2(1.5f, 0)));
            leftEntity.Attach(_physicsSystem.Physics.CreateRigidbody(new Vector2(1.5f, 0), new PolygonCollider(verticalBorder)));
            
            var rightEntity = createEntity();
            rightEntity.Attach(new Transform(new Vector2(mapWidth - 0.5f, 0)));
            rightEntity.Attach(_physicsSystem.Physics.CreateRigidbody(new Vector2(mapWidth - 0.5f, 0), new PolygonCollider(verticalBorder)));
        }

        public void Draw(GameTime gameTime)
        {
            DrawMap();

            DrawPlayerLayer();
        }

        private void DrawMap()
        {
            // Draw layers except for player's layer.
            for (int i = 0; i < _tiledMap.Layers.Length; i++)
            {
                var layer = _tiledMap.Layers[i];

                if (layer.Name == "Player") continue;

                for (int j = 0; j < layer.Data.Length; j++)
                {
                    var location = ConvertTo2D(j) * _tileDimensions;
                    var data = layer.Data[j];

                    if (data == 0) continue;

                    var tileInfo = _tiledMap.Tiles.FirstOrDefault(x => x.Id == data && x.Animation?.Length > 0);
                    if (tileInfo != null)
                    {
                        data = tileInfo.Animation[tileInfo.Index];
                    }

                    var texture = _tilesetUtility.GetTile(data);

                    _spriteBatch.Draw(texture, location, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, i * 0.01f);
                }
            }
        }

        /// <summary>
        /// Draws the content on the player layer.
        /// Makes use of the y coordinate in order to make it dynamic - ie let characters walk in front and stand behind.
        /// </summary>
        private void DrawPlayerLayer()
        {
            // Draw everything on the player's layer.
            var playerLayer = _tiledMap.Layers[_playersLayer];

            for (int j = 0; j < playerLayer.Data.Length; j++)
            {
                var location = ConvertTo2D(j) * _tileDimensions;
                var data = playerLayer.Data[j];

                if (data == 0) continue;

                var tileInfo = _tiledMap.Tiles.FirstOrDefault(x => x.Id == data && x.Animation?.Length > 0);
                if (tileInfo != null)
                {
                    data = tileInfo.Animation[tileInfo.Index];
                }

                var texture = _tilesetUtility.GetTile(data);

                _spriteBatch.Draw(texture, location, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, _playersLayer * MapDrawing.LayerInterval + _mapHeightStep * location.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var animation in _animatedTiles)
            {
                animation.AnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animation.AnimationTimer >= animation.AnimationSpeed * 0.001f)
                {
                    animation.AnimationTimer = 0;
                    animation.Index++;
                    if (animation.Index >= animation.Animation.Length) animation.Index = 0;
                }
            }
        }

        private Vector2 ConvertTo2D(int x)
        {
            int y = 0;
            while (x >= _tiledMap.Width)
            {
                y++;
                x -= _tiledMap.Width;
            }

            return new Vector2(x, y);
        }
    }

    public static class MapDrawing
    {
        /// <summary>
        /// The layerdepth interval to be used per layer.
        /// As Monogame supports layer depths 0 - 1, this allows for 100 layers in a map.
        /// </summary>
        public static float LayerInterval { get; } = 0.01f;
    }
}
