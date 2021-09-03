using JEng.Content.Pipeline.Data.Animations;
using JEng.Content.Pipeline.Data.Tilesets;
using System.Collections.Generic;

namespace JEng.Content.Pipeline.Data.Animations.Processed
{
    public class ProcessedAnimationSetData : AnimationSetData
    {
        public new ProcessedAnimationData[] Animations { get; set; }
        public new Dictionary<string, ProcessedTilesetData> Tilesets { get; set; }

        public ProcessedAnimationSetData(AnimationSetData data)
        {
            Id = data.Id;
            Category = data.Category;
        }
    }
}
