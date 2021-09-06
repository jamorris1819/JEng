using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Dynamics;

namespace JEng.Core.Controllers
{
    public interface ICharacterController
    {
        bool Enabled { get; set; }

        Body Body { get; set; }

        void Update(GameTime gameTime);
    }
}
