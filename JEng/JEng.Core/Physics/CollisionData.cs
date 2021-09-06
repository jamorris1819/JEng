using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace JEng.Core.Physics
{
    public struct CollisionData
    {
        public Fixture Sender { get; set; }
        public Fixture Other { get; set; }
        public Contact Contact { get; set; }
    }
}
