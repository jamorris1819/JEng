using Microsoft.Xna.Framework.Graphics;

namespace JEng.Content.Pipeline.Data.Textures.Processed
{
    public class ProcessedTexture
    {
        public SurfaceFormat Format { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Data { get; set; }
    }
}
