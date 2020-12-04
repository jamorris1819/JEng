﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using JEng.Core.Components;
using JEng.Core.TiledMap;
using System;
using System.Collections.Generic;
using System.Text;
using JEng.Core.Physics;

namespace JEng.Engine.Systems
{
    public class PhysicsSystem : EntityUpdateSystem
    {
        private tainicom.Aether.Physics2D.Dynamics.World _world;
        private ComponentMapper<Rigidbody> _rigidbodyMapper;
        private ComponentMapper<TransformComponent> _transformMapper;
        private readonly float _simulationSpeed;

        public Physics Physics { get; }

        public PhysicsSystem() : base(Aspect.All(typeof(Rigidbody), typeof(TransformComponent)))
        {
            _world = new tainicom.Aether.Physics2D.Dynamics.World(new tainicom.Aether.Physics2D.Common.Vector2(0, 0));
            Physics = new Physics(_world);
            _simulationSpeed = 1.0f / 60.0f;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _rigidbodyMapper = mapperService.GetMapper<Rigidbody>();
            _transformMapper = mapperService.GetMapper<TransformComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            _world.Step(_simulationSpeed);

            foreach(var entity in ActiveEntities)
            {
                var rigidbody = _rigidbodyMapper.Get(entity);
                var transform = _transformMapper.Get(entity);
                transform.Position = rigidbody.Position;// + rigidbody.Offset;
            }
        }

        protected override void OnEntityAdded(int entityId)
        {
            var rigidbody = _rigidbodyMapper.Get(entityId);
            var transform = _transformMapper.Get(entityId);
            if (rigidbody == null || transform == null) return;

            rigidbody.Position = transform.Position;
        }
    }
}
