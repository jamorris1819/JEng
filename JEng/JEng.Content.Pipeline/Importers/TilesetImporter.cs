using JEng.Content.Pipeline.Data.Tilesets;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Text.Json;

namespace JEng.Content.Pipeline.Importers
{
    [ContentImporter(".json", DefaultProcessor = "TilesetProcessor", DisplayName = "Tileset Importer")]
    public class TilesetImporter : ContentImporter<TilesetData>
    {
        public override TilesetData Import(string filename, ContentImporterContext context)
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<TilesetData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
