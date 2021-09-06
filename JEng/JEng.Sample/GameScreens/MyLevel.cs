using JEng.Core.Components;
using JEng.Core.Controllers;
using JEng.Core.Graphics;
using JEng.Core.Physics;
using JEng.Core.Physics.Colliders;
using JEng.Core.State;
using JEng.Core.TiledMap;
using JEng.Engine.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        }
    }
}
