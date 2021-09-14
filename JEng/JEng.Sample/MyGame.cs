using JEng.Engine;
using JEng.Sample.GameScreens;

namespace JEng.Sample
{
    public class MyGame : GameBase
    {
        public MyGame() : base(1920, 1080)
        {
            var mylevel = new MyLevel(this, GameStateManager);

            ChangeState(mylevel);
        }
    }
}
