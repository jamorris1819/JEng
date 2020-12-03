using System;
using JEng.Engine;

namespace JEng.Sample.OpenGL
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MyGame())
                game.Run();
        }
    }
}
