using GameLibrary;
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

namespace JEng.Engine.GameScreens
{
    public class EngineState : BaseGameState
    {
        private World _world;
        private PhysicsSystem _physicsSystem;

        protected Physics Physics { get => _physicsSystem.Physics; }

        public EngineState(Game game, GameStateManager manager) : base(game, manager) { }

        public override void Initialize()
        {
            base.Initialize();

            var cameraSystem = new CameraSystem();
            _physicsSystem = new PhysicsSystem();

            _world = new WorldBuilder()
                .AddSystem(cameraSystem)
                .AddSystem(new AnimationSystem(cameraSystem, GameRef.GraphicsDevice))
                .AddSystem(_physicsSystem)
                .AddSystem(new CharacterControllerSystem())
                .Build();
        }

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
