using JEng.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEng.Engine
{
    public abstract partial class BaseGameState : GameState
    {
        protected GameBase GameRef { get; private set; }
        protected ContentManager Content { get => GameRef.Content; }

        protected BaseGameState(Game game, GameStateManager manager) : base(game, manager)
        {
            GameRef = (GameBase)game;
        }
    }
}
