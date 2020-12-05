using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;

namespace JEng.Engine.Systems
{
    public class RenderSystem : EntityUpdateSystem, IUpdateSystem, IDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private ComponentMapper<AnimationComponent> _animationComponentMapper;
        private ComponentMapper<Sprite> _spriteComponentMapper;
        private ComponentMapper<Transform> _transformComponentMapper;
        private CameraSystem _cameraSystem;

        public RenderSystem(CameraSystem cs, GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform)).One(typeof(AnimationComponent), typeof(Sprite)))
        {
            _cameraSystem = cs;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _animationComponentMapper = mapperService.GetMapper<AnimationComponent>();
            _spriteComponentMapper = mapperService.GetMapper<Sprite>();
            _transformComponentMapper = mapperService.GetMapper<Transform>();
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _cameraSystem.Transform);

            foreach (var entity in ActiveEntities)
            {
                if (HasAnimation(entity)) RenderAnimation(entity);
                if (HasSprite(entity)) RenderSprite(entity);
            }

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                if (HasAnimation(entity))
                    UpdateAnimation(gameTime, entity);
            }
        }

        private bool HasAnimation(int entity) => _animationComponentMapper.Get(entity) != null;

        private bool HasSprite(int entity) => _spriteComponentMapper.Get(entity) != null;

        private void RenderAnimation(int entity)
        {
            var animationComponent = _animationComponentMapper.Get(entity);
            var transformComponent = _transformComponentMapper.Get(entity);

            var frame = animationComponent.CurrentAnimation.Frames[animationComponent.CurrentAnimation.CurrentFrame];

            var offsetForOrigin = new Vector2(frame.Width * 0.5f, frame.Height * 0.5f);
            _spriteBatch.Draw(frame, transformComponent.GetWorldPosition() - offsetForOrigin, Color.White);
        }

        private void RenderSprite(int entity)
        {
            var spriteComponent = _spriteComponentMapper.Get(entity);
            var transformComponent = _transformComponentMapper.Get(entity);

            var texture = spriteComponent.Texture;

            var pos = transformComponent.GetWorldPosition();
            var origin = spriteComponent.OriginPoint;
            var destination = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
            
            _spriteBatch.Draw(texture, destination, null, Color.White, MathHelper.ToDegrees(transformComponent.GetWorldRotation()), origin, SpriteEffects.None, 1.0f);
        }

        private void UpdateAnimation(GameTime gameTime, int entity)
        {
            var animationComponent = _animationComponentMapper.Get(entity);
            animationComponent.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (animationComponent.Timer >= animationComponent.CurrentAnimation.Delay)
            {
                animationComponent.Timer = 0;
                animationComponent.CurrentAnimation.NextFrame();
            }
        }
    }
}
