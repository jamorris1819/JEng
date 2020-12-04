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
            entity.Attach(new RigidbodyComponent(Physics.CreateRigidbody(new Vector2(180, 180))));
            entity.Attach(new CharacterControllerComponent() { Controller = new InputCharacterController() });

            entity = CreateEntity();
            entity.Attach(new TransformComponent(new Vector2(400, 180)));
            entity.Attach(new AnimationComponent(data));

            var rbody = Physics.CreateCircleRigidbody(8.0f, 0.1f, new Vector2(400, 180), RigidbodyType.Dynamic);
            rbody.LinearDrag = 10.0f;
            entity.Attach(new RigidbodyComponent(rbody));



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
