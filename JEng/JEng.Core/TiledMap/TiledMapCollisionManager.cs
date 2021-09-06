using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.TiledMap
{
    public class TiledMapCollisionManager
    {
        private TiledMap _tiledMap;
        private World _world;
        private List<Body> _bodies;

        public TiledMapCollisionManager(TiledMap tiledMap)
        {
            _tiledMap = tiledMap;
        }

        public void Process()
        {
            _world = new World(new Vector2(0, 0));
            _bodies = new List<Body>();

            for (int y = 0; y < _tiledMap.Height; y++)
            {
                for (int x = 0; x < _tiledMap.Width; x++)
                {
                    var collider = GetCollider(x, y);
                    if (collider.Count() == 0) break;

                    var vertices = new tainicom.Aether.Physics2D.Common.Vertices(collider);
                    var position = new Vector2(x * _tiledMap.TileWidth, y * _tiledMap.TileWidth);

                    var body = _world.CreatePolygon(vertices, 1.0f, position, 0, BodyType.Static);

                    _bodies.Add(body);
                }
            }
        }

        public Body GetCharacter(float x, float y)
        {
            var character = _world.CreateCircle(4, 0, new Vector2(x, y), BodyType.Dynamic);


            return character;
        }

        public void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private IEnumerable<Vector2> GetCollider(int x, int y)
        {
            foreach (var layer in _tiledMap.Layers)
            {
                var data = layer.Data[x + y * _tiledMap.Width];
                if (data == 0) continue;
                var tile = _tiledMap.Tiles.FirstOrDefault(c => c.Id == data);

                if (tile != null)
                {
                    return tile.Collider ?? new Vector2[0];
                }
            }

            return new Vector2[0];
        }
    }
}
