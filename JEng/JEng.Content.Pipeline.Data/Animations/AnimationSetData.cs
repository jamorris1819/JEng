using JEng.Content.Pipeline.Data.Tilesets;

namespace JEng.Content.Pipeline.Data.Animations
{
    public class AnimationSetData
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public AnimationData[] Animations { get; set; }
        public TilesetData[] Tilesets { get; set; }
    }
}
