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
    /// <summary>
    /// The ControlManager keeps track of all the controls.
    /// </summary>
    public class ControlManager : List<Control>
    {
        public event EventHandler FocusChanged;
        private bool acceptInput = true;
        static SpriteFont spriteFont;

        /// <summary>
        /// The SpriteFont to be used by the child controls by default.
        /// </summary>
        public static SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        /// <summary>
        /// Whether the manager is accepting input.
        /// </summary>
        public bool AcceptInput
        {
            get { return acceptInput; }
            set { acceptInput = value; }
        }
        
        /// <summary>
        /// Creates the ControlManager.
        /// </summary>
        /// <param name="spriteFont">SpriteFont to be used as default by children</param>
        public ControlManager(SpriteFont spriteFont)
            : base()
        {
            ControlManager.spriteFont = spriteFont;
        }

        /// <summary>
        /// Creates the ControlManager.
        /// </summary>
        public ControlManager()
            : base()
        {
            ControlManager.spriteFont = null;
        }

        /// <summary>
        /// Creates the ControlManager.
        /// </summary>
        /// <param name="spriteFont">SpriteFont to be used as default by children</param>
        /// <param name="capacity">Capacity of the ControlManager</param>
        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            ControlManager.spriteFont = spriteFont;
        }

        /// <summary>
        /// Creates the ControlManager.
        /// </summary>
        /// <param name="spriteFont">SpriteFont to be used as default by children</param>
        /// <param name="collection">Collection of controls</param>
        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection)
            : base(collection)
        {
            ControlManager.spriteFont = spriteFont;
        }

        /// <summary>
        /// Updates the ControlManager.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last update</param>
        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;
            foreach (Control control in this)
            {
                if (control.Enabled)
                    control.Update(gameTime);
                if (control.HasFocus)
                    control.HandleInput();
            }
        }

        /// <summary>
        /// Draws all of the controls held in the ControlManager.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control control in this)
                if (control.Visible)
                    control.Draw(spriteBatch);
        }
    }
}
