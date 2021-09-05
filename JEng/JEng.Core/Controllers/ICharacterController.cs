using JEng.Core.Physics;
using Microsoft.Xna.Framework;

namespace JEng.Core.Controllers
{
    public interface ICharacterController
    {
        bool Enabled { get; set; }
        Rigidbody Rigidbody { get; set; }
        void Update(GameTime gameTime);
    }
}
