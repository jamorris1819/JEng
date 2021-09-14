using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Texture.Renderer
{
    public interface ITextureRenderer
    {
        void Draw(SpriteBatch spriteBatch, Rectangle destination, Color colour);
    }
}
