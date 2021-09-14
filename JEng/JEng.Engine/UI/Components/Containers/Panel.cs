using JEng.Engine.UI.Texture;
using JEng.Engine.UI.Texture.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JEng.Engine.UI.Components.Containers
{
    public class Panel : UIComponent
    {
        private ITextureRenderer _textureRenderer;

        public ITexture Texture { get; }

        public Panel(ITexture texture)
        {
            Texture = texture;
            _textureRenderer = TextureRendererFactory.Create(texture);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _textureRenderer.Draw(spriteBatch, new Rectangle(Position, Size), Colour);
        }
    }
}
