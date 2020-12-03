using JEng.Content.Pipeline.Data.Animations;

namespace JEng.Content.Pipeline.Data.Animations.Processed
{
    public class ProcessedAnimationSetData : AnimationSetData
    {
        public new ProcessedAnimationData[] Animations { get; set; }

        public ProcessedAnimationSetData(AnimationSetData data)
        {
            Id = data.Id;
            Category = data.Category;
        }
    }
}
