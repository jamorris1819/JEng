using Microsoft.Xna.Framework.Content.Pipeline;
using JEng.Content.Pipeline.Data.Animations;
using System.IO;
using System.Text.Json;

namespace JEng.Content.Pipeline.Importers
{
    [ContentImporter(".anim", DefaultProcessor = "AnimationProcessor", DisplayName = "Animation Importer")]
    public class AnimationImporter : ContentImporter<AnimationSetData>
    {
        public override AnimationSetData Import(string filename, ContentImporterContext context)
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<AnimationSetData>(json);
        }
    }
}
