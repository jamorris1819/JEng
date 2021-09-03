using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using JEng.Content.Pipeline.Data.Animations;
using JEng.Content.Pipeline.Data.Animations.Processed;
using JEng.Content.Pipeline.Readers;

namespace JEng.Content.Pipeline.Writers
{
    [ContentTypeWriter]
    public class AnimationWriter : ContentTypeWriter<ProcessedAnimationSetData>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(AnimationSetData).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, ProcessedAnimationSetData value)
        {
            output.Write(value.Id);
            output.Write(value.Category);
            output.WriteObject(value.Animations);
            output.WriteObject(value.Tilesets);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(AnimationReader).AssemblyQualifiedName;
        }
    }
}
