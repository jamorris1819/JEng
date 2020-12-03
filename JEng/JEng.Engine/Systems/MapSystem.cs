using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.TiledMap;
using JEng.Engine.Utilities;
using System;
using System.Linq;

namespace JEng.Engine.Systems
{
    public class MapSystem : UpdateSystem, IUpdateSystem, IDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TilesetUtility _tilesetUtility;
        private CameraSystem _cameraSystem;
        private Vector2 _tileDimensions;

        private float _timer;

        public MapSystem(TiledMap tiledMap, CameraSystem cameraSystem, GraphicsDevice graphicsDevice)
        {
            _tiledMap = tiledMap;
            _cameraSystem = cameraSystem;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _tileDimensions = new Vector2(_tiledMap.TileWidth, _tiledMap.TileHeight);
            _tilesetUtility = new TilesetUtility(_tiledMap.Tilesets);
            _timer = 0.0f;
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _cameraSystem.Transform);

            for (int i = 0; i < _tiledMap.Layers.Length; i++)
            {
                var layer = _tiledMap.Layers[i];

                for(int j = 0; j < layer.Data.Length; j++)
                {
                    var location = ConvertTo2D(j) * _tileDimensions;
                    var data = layer.Data[j];

                    if (data == 0) continue;

                    var tileInfo = _tiledMap.Tiles.FirstOrDefault(x => x.Id == data && x.Animation?.Length > 0);
                    if(tileInfo != null)
                    {
                        data = tileInfo.Animation[tileInfo.Index];
                    }

                    var texture = _tilesetUtility.GetTile(data);

                    _spriteBatch.Draw(texture, location, Color.White);
                }
            }

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_timer > 0.15f)
            {
                var animations = _tiledMap.Tiles.Where(x => x.Animation?.Length > 0);
                foreach(var animation in animations)
                {
                    animation.Index++;
                    if (animation.Index >= animation.Animation.Length) animation.Index = 0;
                }
                _timer = 0.0f;
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
}
