using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;

namespace JEng.Engine.Systems
{
    public class AnimationSystem : EntityUpdateSystem, IUpdateSystem, IDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private ComponentMapper<AnimationComponent> _animationComponentMapper;
        private ComponentMapper<TransformComponent> _transformComponentMapper;
        private CameraSystem _cameraSystem;

        public AnimationSystem(CameraSystem cs, GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(AnimationComponent), typeof(TransformComponent)))
        {
            _cameraSystem = cs;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _animationComponentMapper = mapperService.GetMapper<AnimationComponent>();
            _transformComponentMapper = mapperService.GetMapper<TransformComponent>();
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _cameraSystem.Transform);

            foreach (var entity in ActiveEntities)
            {
                var animationComponent = _animationComponentMapper.Get(entity);
                var transformComponent = _transformComponentMapper.Get(entity);

                var frame = animationComponent.CurrentAnimation.Frames[animationComponent.CurrentAnimation.CurrentFrame];

                var offsetForOrigin = new Vector2(frame.Width * 0.5f, frame.Height * 0.5f);
                _spriteBatch.Draw(frame, transformComponent.GetWorldPosition() - offsetForOrigin, Color.White);
            }

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var animationComponent = _animationComponentMapper.Get(entity);
                animationComponent.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if(animationComponent.Timer >= animationComponent.CurrentAnimation.Delay)
                {
                    animationComponent.Timer = 0;
                    animationComponent.CurrentAnimation.NextFrame();
                }

                //_transformComponentMapper.Get(entity).Position += new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds * 10);
            }
        }
    }
}
