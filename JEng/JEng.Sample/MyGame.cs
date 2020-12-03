using JEng.Engine;
using JEng.Sample.GameScreens;

namespace JEng.Sample
{
    public class MyGame : GameBase
    {
        public MyGame() : base()
        {
            var mylevel = new MyLevel(this, GameStateManager);

            ChangeState(mylevel);
        }
    }
}
