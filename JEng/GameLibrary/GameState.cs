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

namespace GameLibrary
{
    /// <summary>
    /// GameState holds all the components in a level.
    /// </summary>
    public abstract partial class GameState : DrawableGameComponent
    {
        private List<GameComponent> childComponents;
        private GameState tag;
        private GameStateManager stateManager;

        /// <summary>
        /// Returns the current GameStateManager.
        /// </summary>
        protected GameStateManager StateManager
        {
            get { return stateManager; }
            set { stateManager = value; }
        }

        /// <summary>
        /// Returns a list of the components in this GameState.
        /// </summary>
        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        /// <summary>
        /// Returns the GameState tag.
        /// </summary>
        public GameState Tag
        {
            get { return tag; }
        }

        /// <summary>
        /// Constructor for GameState.
        /// </summary>
        /// <param name="game">The Game class</param>
        /// <param name="manager">The current GameStateManager</param>
        public GameState(Game game, GameStateManager manager)
            : base(game)
        {
            stateManager = manager;
            childComponents = new List<GameComponent>();
            tag = this;
        }

        /// <summary>
        /// Initializes the GameState.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Updates all components in GameState.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last update</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
                if (component.Enabled)
                    component.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws all components in GameState.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last draw</param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach(GameComponent component in childComponents)
            {
                if(component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// Triggered when the state changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void StateChange(object sender, EventArgs e)
        {
            if (stateManager.CurrentState == tag)
                Show();
            else
                Hide();
        }

        /// <summary>
        /// Makes all components visible.
        /// </summary>
        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;

            foreach(GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        /// <summary>
        /// Makes all components invisible.
        /// </summary>
        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;

            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
    }
}
