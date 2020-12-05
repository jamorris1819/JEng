using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.Controllers
{
    public class InputCharacterController : CharacterController, ICharacterController
    {
        public override void Update(GameTime gameTime)
        {
            int x = 0;
            int y = 0;
            if (InputHandler.KeyDown(Keys.D)) x = 1;
            else if (InputHandler.KeyDown(Keys.A)) x = -1;
            
            if (InputHandler.KeyDown(Keys.W)) y = -1;
            else if (InputHandler.KeyDown(Keys.S)) y = 1;
            Vector2 vec = new Vector2(x, y) * Speed;
            Move(vec, gameTime);
        }
    }
}
