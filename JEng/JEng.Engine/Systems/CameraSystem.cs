using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;
using System;
using System.Linq;

namespace JEng.Engine.Systems
{
    public class CameraSystem : EntityUpdateSystem
    {
        ComponentMapper<CameraComponent> _cameraComponentMapper;
        ComponentMapper<TransformComponent> _transformComponentMapper;

        public Matrix Transform { private set; get; }

        public CameraSystem() : base(Aspect.All(typeof(CameraComponent), typeof(TransformComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _cameraComponentMapper = mapperService.GetMapper<CameraComponent>();
            _transformComponentMapper = mapperService.GetMapper<TransformComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            int cameraEntity = ActiveEntities.First(x => _cameraComponentMapper.Get(x).Active);
            var cameraComponent = _cameraComponentMapper.Get(cameraEntity);
            var transformComponent = _transformComponentMapper.Get(cameraEntity);

            if (cameraComponent.Tracking != null)
            {
                var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var target = _transformComponentMapper.Get(cameraComponent.Tracking);
                //var pos = Vector2.Lerp(transformComponent.Position, target.Position, 0.1f);
                var pos = Vector2.SmoothStep(transformComponent.Position, target.Position, dt * 12.5f);
                var roundPos = new Vector2((float)Math.Floor(pos.X), (float)Math.Floor(pos.Y));

                transformComponent.Position = pos;// new Vector2((int)pos.X, (int)pos.Y);

                var cameraWidth = cameraComponent.Camera.BoundingRectangle.Width;
                transformComponent.Position = new Vector2(Math.Clamp(transformComponent.Position.X, cameraWidth * 0.5f, 1000000), transformComponent.Position.Y);

                cameraComponent.Camera.LookAt(transformComponent.Position);
            }

            Transform = cameraComponent.Camera.GetViewMatrix();
        }
    }
}
