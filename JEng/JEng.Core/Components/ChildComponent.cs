using System.Collections.Generic;
using System.Linq;

namespace JEng.Core.Components
{
    public class ChildComponent
    {
        public int[] Children { get; set; }

        public ChildComponent(IEnumerable<int> children)
        {
            Children = children.ToArray();
        }

        public ChildComponent(int child) : this(new[] { child }) { }
    }
}
