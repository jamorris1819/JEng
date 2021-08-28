using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using JEng.Core.Components;
using JEng.Core.Controllers;
using JEng.Core.Graphics;
using JEng.Core.Physics;
using JEng.Core.TiledMap;
using JEng.Engine.Systems;
using System.Linq;
using JEng.Engine;
using MonoGame.Extended.Entities.Systems;
using System.Collections.Generic;
using JEng.Core.State;

namespace JEng.Engine.GameScreens
{
    public class EngineState : BaseGameState
    {
        private World _world;
        private PhysicsSystem _physicsSystem;
        private Vector2 _gravity;

        private IEnumerable<ISystem> _additionalSystems = new ISystem[0];

        protected Physics Physics { get => _physicsSystem.Physics; }

        public EngineState(Game game, GameStateManager manager) : this(game, manager, Vector2.Zero) { }

        public EngineState(Game game, GameStateManager manager, Vector2 gravity) : base(game, manager)
        {
            _gravity = gravity;
        }

        public override void Initialize()
        {
            base.Initialize();

            var cameraSystem = new CameraSystem();
            _physicsSystem = new PhysicsSystem(_gravity);

            var worldBuilder = new WorldBuilder()
                .AddSystem(cameraSystem)
                .AddSystem(new RenderSystem(cameraSystem, GameRef.GraphicsDevice))
                .AddSystem(_physicsSystem)
                .AddSystem(new CharacterControllerSystem());

            foreach (var system in _additionalSystems)
            {
                worldBuilder = worldBuilder.AddSystem(system);
            }

            _world = worldBuilder.Build();
        }

        protected void SetAdditionalSystems(IEnumerable<ISystem> additionalSystems)
            => _additionalSystems = additionalSystems;

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _world.Draw(gameTime);
        }

        protected Entity CreateEntity() => _world.CreateEntity();
    }
}
