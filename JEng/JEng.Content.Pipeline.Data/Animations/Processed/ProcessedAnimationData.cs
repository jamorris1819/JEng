using JEng.Content.Pipeline.Data.Animations;
using JEng.Content.Pipeline.Data.Textures.Processed;

namespace JEng.Content.Pipeline.Data.Animations.Processed
{
    public class ProcessedAnimationData : AnimationData
    {
        public ProcessedTexture[] Frames { get; set; }

        public ProcessedAnimationData() : this(new AnimationData()) { }

        public ProcessedAnimationData(AnimationData data)
        {
            AnimationName = data.AnimationName;
            TilesetName = data.TilesetName;
            X = data.X;
            Y = data.Y;
            Length = data.Length;
            Direction = data.Direction;
            Delay = data.Delay;
        }
    }
}
