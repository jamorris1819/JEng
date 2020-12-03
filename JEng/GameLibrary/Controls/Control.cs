// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameLibrary.Controls
{
    public abstract class Control
    {
        protected string name;
        protected string text;
        protected Vector2 size;
        protected Vector2 position;
        protected bool hasFocus;
        protected bool enabled;
        protected bool visible;
        protected SpriteFont spriteFont;
        protected Color color;
        protected Rectangle region;

        public EventHandler Selected;
        public EventHandler ContentChanged;

        /// <summary>
        /// The name of the control.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The text field for the control.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// The size of the control.
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// The position of the control.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                position.Y = (int)position.Y;
            }
        }

        /// <summary>
        /// Whether the control has focus.
        /// </summary>
        public bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }

        /// <summary>
        /// Whether the control is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// Whether the control is visible.
        /// </summary>
        public bool Visible
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// The SpriteFont used by the control.
        /// </summary>
        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }

        /// <summary>
        /// The color of the control.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Region of space on screen that the control covers.
        /// </summary>
        public Rectangle Region
        {
            get { return region; }
        }

        public Control()
        {
            color = Color.White;
            enabled = true;
            visible = true;
            spriteFont = ControlManager.SpriteFont;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput();

        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
        }

        protected virtual void OnContentChanged(EventArgs e)
        {
            if (ContentChanged != null)
                ContentChanged(this, e);
        }
    }
}
