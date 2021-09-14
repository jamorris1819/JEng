using JEng.Engine.UI.Texture;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Engine.UI.Components.Widgets
{
    public class LabelButton : Button<Label>
    {
        public LabelButton(Label component, ITexture texture, ITexture hoverTexture, ITexture clickTexture) : base(component, texture, hoverTexture, clickTexture)
        {
        }
    }
}
