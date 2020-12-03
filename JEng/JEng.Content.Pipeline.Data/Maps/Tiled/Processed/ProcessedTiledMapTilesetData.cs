using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Textures.Processed;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Data.Maps.Tiled.Processed
{
    public class ProcessedTiledMapTilesetData
    {
        public int StartId { get; set; }
        public ProcessedTexture[] Tiles { get; private set; }

        public ProcessedTiledMapTilesetData() : this(new ProcessedTexture[0]) { }

        public ProcessedTiledMapTilesetData(ProcessedTexture[] tiles)
        {
            Tiles = tiles;
        }
    }
}
