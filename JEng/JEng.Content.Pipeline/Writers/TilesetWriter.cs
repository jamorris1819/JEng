using JEng.Content.Pipeline.Data.Tilesets;
using JEng.Content.Pipeline.Readers;
using JEng.Core.Tilesets;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace JEng.Content.Pipeline.Writers
{
    [ContentTypeWriter]
    public class TilesetWriter : ContentTypeWriter<ProcessedTilesetData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(TilesetReader).AssemblyQualifiedName;
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Tileset).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, ProcessedTilesetData value)
        {
            output.Write(value.Name);
            output.Write(value.Location);
            output.Write(value.TilesetWidth);
            output.Write(value.TilesetHeight);
            output.Write(value.TilesWide);
            output.Write(value.TilesHigh);
            output.WriteObject(value.Texture);
        }
    }
}
