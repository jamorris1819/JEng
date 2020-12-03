using Microsoft.Xna.Framework;
using JEng.Core.Components;
using JEng.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.Controllers
{
    public interface ICharacterController
    {
        RigidbodyComponent RigidbodyComponent { get; set; }
        CharacterDirection GetDirection();
        CharacterAction GetAction();
        void Update(GameTime gameTime);
    }
}
