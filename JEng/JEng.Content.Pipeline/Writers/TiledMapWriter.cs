using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using JEng.Content.Pipeline.Data.Maps.Tiled;
using JEng.Content.Pipeline.Data.Maps.Tiled.Processed;
using JEng.Content.Pipeline.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Writers
{
    [ContentTypeWriter]
    public class TiledMapWriter : ContentTypeWriter<ProcessedTiledMapData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(TiledMapReader).AssemblyQualifiedName;
        }
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(TiledMapData).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, ProcessedTiledMapData value)
        {
            output.WriteObject(value.Layers);
            output.WriteObject(value.Tiles);
            output.WriteObject(value.Tilesets);
            output.WriteObject(value.Objects);
            output.Write(value.TileWidth);
            output.Write(value.TileHeight);
            output.Write(value.Width);
            output.Write(value.Height);
        }
    }
}
