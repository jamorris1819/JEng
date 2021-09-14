using System;

namespace JEng.Engine.UI.Texture.Renderer
{
    public static class TextureRendererFactory
    {
        public static ITextureRenderer Create(ITexture texture)
        {
            switch (texture.Type)
            {
                case TextureType.Sliced3x3: return new SlicedTexture3x3Renderer(texture);
                default: throw new ArgumentOutOfRangeException(nameof(texture.Type));
            }
        }
    }
}
