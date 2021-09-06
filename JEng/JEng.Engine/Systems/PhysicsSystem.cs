using JEng.Core.Components;
using JEng.Core.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace JEng.Engine.Systems
{
    public class PhysicsSystem : EntityUpdateSystem
    {
        private ComponentMapper<PhysicsComponent> _bodyMapper;
        private ComponentMapper<TransformComponent> _transformMapper;
        private readonly float _simulationSpeed;

        public PhysicsWorld Physics { get; }

        public PhysicsSystem(Vector2 gravity) : base(Aspect.All(typeof(PhysicsComponent), typeof(TransformComponent)))
        {
            Physics = new PhysicsWorld(gravity);
            _simulationSpeed = 1.0f / 60.0f;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _bodyMapper = mapperService.GetMapper<PhysicsComponent>();
            _transformMapper = mapperService.GetMapper<TransformComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            Physics.Step(_simulationSpeed);

            foreach(var entity in ActiveEntities)
            {
                var body = _bodyMapper.Get(entity);
                var transform = _transformMapper.Get(entity);
                transform.Position = body.Body.Position + body.Offset;
            }
        }

        protected override void OnEntityAdded(int entityId)
        {
            var body = _bodyMapper.Get(entityId);
            var transform = _transformMapper.Get(entityId);

            if (body != null && transform != null)
            {
                if (transform.Parent != null) throw new Exception("Child entities may not have a Rigidbody component");

                body.Body.Position = transform.Position;
            }
        }
    }
}
