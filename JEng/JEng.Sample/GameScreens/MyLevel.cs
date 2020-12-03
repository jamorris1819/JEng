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
using JEng.Engine.GameScreens;

namespace JEng.Sample.GameScreens
{
    public class MyLevel : EngineState
    {
        public MyLevel(MyGame game, GameStateManager manager) : base(game, manager) { }

        public override void Initialize()
        {
            base.Initialize();

            var entity = CreateEntity();
            entity.Attach(new TransformComponent(new Vector2(180, 180)));

            AnimationSet data = Content.Load<AnimationSet>("wizard1");
            entity.Attach(new AnimationComponent(data));
            entity.Attach(new RigidbodyComponent(CreateRigidbody(new Vector2(180, 180))));
            entity.Attach(new CharacterControllerComponent() { Controller = new InputCharacterController() });



            var cam = CreateEntity();
            cam.Attach(new TransformComponent(new Vector2(0, 0)));
            cam.Attach(new CameraComponent()
            {
                Active = true,
                Camera = new OrthographicCamera(new BoxingViewportAdapter(Game.Window, GameRef.GraphicsDevice, (int)(300 * 1.7777f), 300))
            });
        }
    }
}
