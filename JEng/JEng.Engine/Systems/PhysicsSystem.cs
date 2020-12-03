using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;
using JEng.Core.TiledMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.Systems
{
    public class PhysicsSystem : EntityUpdateSystem
    {
        private ComponentMapper<RigidbodyComponent> _rigidbodyMapper;
        private ComponentMapper<TransformComponent> _transformMapper;

        public PhysicsSystem() : base(Aspect.All(typeof(RigidbodyComponent), typeof(TransformComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _rigidbodyMapper = mapperService.GetMapper<RigidbodyComponent>();
            _transformMapper = mapperService.GetMapper<TransformComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var entity in ActiveEntities)
            {
                var rigidbody = _rigidbodyMapper.Get(entity);
                var transform = _transformMapper.Get(entity);
                transform.Position = rigidbody.Body.Position + rigidbody.Offset;
            }
        }
    }
}
