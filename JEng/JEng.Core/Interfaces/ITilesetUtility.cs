using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEng.Core.Interfaces
{
    public interface ITilesetUtility
    {
        Tileset GetTileset(string name);
    }
}
