﻿using GameLibrary;
using JEng.Core.Components;
using JEng.Core.Controllers;
using JEng.Core.Graphics;
using JEng.Core.Physics;
using JEng.Core.Physics.Colliders;
using JEng.Engine.GameScreens;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

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
            entity.Attach(Physics.CreateRigidbody(new CircleCollider(8.0f), RigidbodyType.Kinematic));
            entity.Attach(new CharacterControllerComponent() { Controller = new InputCharacterController() });

            var child = CreateEntity();
            child.Attach(new AnimationComponent(data));
            var transform = new TransformComponent(new Vector2(-20, 0))
            {
                Parent = entity.Get<TransformComponent>()
            };
            child.Attach(transform);

            entity = CreateEntity();
            entity.Attach(new TransformComponent(new Vector2(400, 180)));
            entity.Attach(new AnimationComponent(data));

            var rbody = Physics.CreateRigidbody(new CircleCollider(8.0f), RigidbodyType.Dynamic);
            rbody.LinearDrag = 10.0f;
            entity.Attach(rbody);

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
