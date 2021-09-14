using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Texture.Renderer
{
    public interface ITextureRenderer
    {
        void SetTexture(ITexture texture);
        void Draw(SpriteBatch spriteBatch, Rectangle destination, Color colour);
    }
}
