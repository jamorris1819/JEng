using JEng.Core.State;
using JEng.Engine.GameScreens;

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
