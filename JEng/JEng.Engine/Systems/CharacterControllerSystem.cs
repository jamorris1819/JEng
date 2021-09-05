using JEng.Core.Components;
using JEng.Core.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace JEng.Engine.Systems
{
    public class CharacterControllerSystem : EntityUpdateSystem
    {
        private ComponentMapper<CharacterControllerComponent> _controllerMapper;
        private ComponentMapper<Rigidbody> _rigidbodyMapper;

        public CharacterControllerSystem() : base(Aspect.All(typeof(CharacterControllerComponent), typeof(Transform))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _controllerMapper = mapperService.GetMapper<CharacterControllerComponent>();
            _rigidbodyMapper = mapperService.GetMapper<Rigidbody>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var entity in ActiveEntities)
            {
                var controller = _controllerMapper.Get(entity);
                if (controller.Controller.Rigidbody == null)
                {
                    var transform = _rigidbodyMapper.Get(entity);
                    controller.Controller.Rigidbody = transform;
                }
                controller.Controller.Update(gameTime);
            }
        }
    }
}
