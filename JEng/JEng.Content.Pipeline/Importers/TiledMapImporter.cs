using Microsoft.Xna.Framework.Content.Pipeline;
using JEng.Content.Pipeline.Data.Maps.Tiled;
using System.IO;
using System.Text.Json;

namespace JEng.Content.Pipeline.Importers
{
    [ContentImporter(".map", DefaultProcessor = "TiledMapProcessor", DisplayName = "Tiled Map Importer")]
    public class TiledMapImporter : ContentImporter<TiledMapData>
    {
        public override TiledMapData Import(string filename, ContentImporterContext context)
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<TiledMapData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
