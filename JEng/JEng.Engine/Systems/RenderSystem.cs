using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;

namespace JEng.Engine.Systems
{
    public class RenderSystem : EntityUpdateSystem, IUpdateSystem, IDrawSystem
    {
        private SpriteBatch _spriteBatch;
        private ComponentMapper<AnimationComponent> _animationComponentMapper;
        private ComponentMapper<Sprite> _spriteComponentMapper;
        private ComponentMapper<Transform> _transformComponentMapper;
        private CameraSystem _cameraSystem;

        public RenderSystem(CameraSystem cs, SpriteBatch spriteBatch) : base(Aspect.All(typeof(Transform)).One(typeof(AnimationComponent), typeof(Sprite)))
        {
            _cameraSystem = cs;
            _spriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _animationComponentMapper = mapperService.GetMapper<AnimationComponent>();
            _spriteComponentMapper = mapperService.GetMapper<Sprite>();
            _transformComponentMapper = mapperService.GetMapper<Transform>();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                if (HasAnimation(entity)) RenderAnimation(entity);
                if (HasSprite(entity)) RenderSprite(entity);
            }
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
            _spriteBatch.Draw(frame, transformComponent.GetWorldPosition() - offsetForOrigin, null, Color.White, MathHelper.ToDegrees(transformComponent.GetWorldRotation()), default, 1f, SpriteEffects.None, animationComponent.Layer);
        }

        private void RenderSprite(int entity)
        {
            var spriteComponent = _spriteComponentMapper.Get(entity);
            var transformComponent = _transformComponentMapper.Get(entity);

            var texture = spriteComponent.Texture;

            var pos = transformComponent.GetWorldPosition();
            var origin = spriteComponent.OriginPoint;
            
            _spriteBatch.Draw(texture, pos, null, Color.White, MathHelper.ToDegrees(transformComponent.GetWorldRotation()), origin, 1f, SpriteEffects.None, spriteComponent.Layer);
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
