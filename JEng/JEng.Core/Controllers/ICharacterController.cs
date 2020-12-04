using JEng.Core.Enums;
using JEng.Core.Physics;
using Microsoft.Xna.Framework;

namespace JEng.Core.Controllers
{
    public interface ICharacterController
    {
        Rigidbody Rigidbody { get; set; }
        CharacterDirection GetDirection();
        CharacterAction GetAction();
        void Update(GameTime gameTime);
    }
}
