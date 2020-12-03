namespace JEng.Content.Pipeline.Data.Animations
{
    public class AnimationData
    {
        public string AnimationName { get; set; }
        public string TilesetName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }
        public string Direction { get; set; }

        public AnimationData()
        {
            AnimationName = string.Empty;
            TilesetName = string.Empty;
            X = 0;
            Y = 0;
            Length = 0;
            Direction = string.Empty;
        }
    }
}
