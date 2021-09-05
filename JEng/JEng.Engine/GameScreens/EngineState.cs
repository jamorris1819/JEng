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
        private Vector2 _gravity;

        private List<ISystem> _additionalSystems = new List<ISystem>();

        protected CameraSystem _cameraSystem;
        protected PhysicsSystem _physicsSystem;

        protected WorldBuilder _worldBuilder;
        protected World _world;

        protected Physics Physics { get => _physicsSystem.Physics; }

        public EngineState(Game game, GameStateManager manager) : this(game, manager, Vector2.Zero) { }

        public EngineState(Game game, GameStateManager manager, Vector2 gravity) : base(game, manager)
        {
            _gravity = gravity;
        }

        public override void Initialize()
        {
            base.Initialize();

            _cameraSystem = new CameraSystem();
            _physicsSystem = new PhysicsSystem(_gravity);

            _worldBuilder = new WorldBuilder()
                .AddSystem(_cameraSystem)
                .AddSystem(_physicsSystem)
                .AddSystem(new CharacterControllerSystem());
        }

        protected void AddAdditionalSystems(IEnumerable<ISystem> additionalSystems)
            => _additionalSystems.AddRange(additionalSystems);

        protected void BuildWorld()
        {
            foreach (var system in _additionalSystems)
            {
                _worldBuilder = _worldBuilder.AddSystem(system);
            }

            _world = _worldBuilder.Build();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _world.Dispose();
            _additionalSystems = new List<ISystem>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameRef.SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, _cameraSystem.Transform);
            _world.Draw(gameTime);
            GameRef.SpriteBatch.End();
        }

        protected Entity CreateEntity() => _world.CreateEntity();
    }
}
