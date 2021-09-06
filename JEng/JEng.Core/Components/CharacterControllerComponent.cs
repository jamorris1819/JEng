using JEng.Core.Controllers;

namespace JEng.Core.Components
{
    public class CharacterControllerComponent
    {
        public ICharacterController Controller { get; set; }

        public bool Enabled
        {
            get => Controller.Enabled;
            set => Controller.Enabled = value;
        }
    }
}
