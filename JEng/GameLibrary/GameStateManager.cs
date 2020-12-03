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
    /// GameStateManager keeps track of all GameStates and their components.
    /// </summary>
    public class GameStateManager : GameComponent
    {
        private Stack<GameState> gameStates = new Stack<GameState>();
        private int drawOrder;
        private const int startDrawOrder = 5000;
        private const int drawOrderInc = 100;

        public event EventHandler OnStateChange;

        /// <summary>
        /// Returns the state that's currently being drawn.
        /// </summary>
        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        /// <summary>
        /// Constructor for the GameStateManager class.
        /// </summary>
        /// <param name="game">The Game class</param>
        public GameStateManager(Game game)
            : base(game)
        {
            drawOrder = startDrawOrder;
        }

        /// <summary>
        /// Initializes the GameStateManager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Updates the GameStateManager.
        /// </summary>
        /// <param name="gameTime">Time passed since the last update.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Pops the visible GameState.
        /// </summary>
        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;
                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
        }

        /// <summary>
        /// Removes the GameState on top of the stack.
        /// </summary>
        private void RemoveState()
        {
            GameState state = gameStates.Peek();
            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            gameStates.Pop();
        }

        /// <summary>
        /// Pushes a GameState into the manager.
        /// </summary>
        /// <param name="newState">New GameState to push</param>
        public void PushState(GameState newState)
        {
            drawOrder += drawOrderInc;
            newState.DrawOrder = drawOrder;
            AddState(newState);
            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        /// <summary>
        /// Adds a GameState to the stack.
        /// </summary>
        /// <param name="newState">New GameState to add</param>
        private void AddState(GameState newState)
        {
            gameStates.Push(newState);
            Game.Components.Add(newState);
            OnStateChange += newState.StateChange;
        }

        /// <summary>
        /// Clears the stack and adds a new GameState
        /// </summary>
        /// <param name="newState">New GameState to add</param>
        public void ChangeState(GameState newState)
        {
            while (gameStates.Count > 0)
                RemoveState();
            newState.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;
            AddState(newState);
            if (OnStateChange != null)
                OnStateChange(this, null);
        }
    }
}
