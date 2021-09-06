using JEng.Core.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace JEng.Engine.Systems
{
    public class CharacterControllerSystem : EntityUpdateSystem
    {
        private ComponentMapper<CharacterControllerComponent> _controllerMapper;
        private ComponentMapper<PhysicsComponent> _bodyMapper;

        public CharacterControllerSystem() : base(Aspect.All(typeof(CharacterControllerComponent), typeof(TransformComponent))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _controllerMapper = mapperService.GetMapper<CharacterControllerComponent>();
            _bodyMapper = mapperService.GetMapper<PhysicsComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var entity in ActiveEntities)
            {
                var controller = _controllerMapper.Get(entity);
                if (controller.Controller.Body == null)
                {
                    var physics = _bodyMapper.Get(entity);
                    controller.Controller.Body = physics.Body;
                }
                controller.Controller.Update(gameTime);
            }
        }
    }
}
