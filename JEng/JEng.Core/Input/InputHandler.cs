using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.Input
{
    public enum ButtonPressed { Left, Right, Middle }
    /// <summary>
    /// InputHandler deals with input.
    /// </summary>
    public class InputHandler : GameComponent
    {
        public static Vector2 VirtualResolution;
        public static Vector2 DeviceResolution;
        public static Vector2 LetterboxSize;
        private static KeyboardState keyboardState;
        private static KeyboardState lastKeyboardState;
        private static MouseState mouseState;
        private static MouseState lastMouseState;

        private static GamePadState gamePad;

        private static bool keyboardInputListening = false;

        private static Texture2D cursorTexture;
        private static bool showCustomCursor;

        /// <summary>
        /// Returns the current state of the keyboard.
        /// </summary>
        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        /// <summary>
        /// Returns whether the keyboard is listening for input.
        /// </summary>
        public static bool KeyboardIsListening
        {
            get { return keyboardInputListening; }
        }

        /// <summary>
        /// Returns the state of the keyboard from the last update.
        /// </summary>
        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        /// <summary>
        /// Returns the current state of the mouse.
        /// </summary>
        public static MouseState MouseState
        {
            get { return mouseState; }
        }

        /// <summary>
        /// Returns the state of the mouse from the last update.
        /// </summary>
        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        public static Texture2D CursorTexture
        {
            get { return cursorTexture; }
            set { cursorTexture = value; }
        }

        public static bool ShowCustomCursor
        {
            get { return showCustomCursor; }
            set { showCustomCursor = value; }
        }

        /// <summary>
        /// Returns a rectangle where the mouse pointer is.
        /// </summary>
        public static Rectangle Mouse
        {
            get
            {
                MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();
                Vector2 mousePos = new Vector2(state.X, state.Y);

                return new Rectangle((int)mousePos.X, (int)mousePos.Y, 2, 2);
            }
        }

        /// <summary>
        /// Constructor for the Input Handler.
        /// </summary>
        /// <param name="game">The Game class</param>
        public InputHandler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
            gamePad = GamePad.GetState(0);
            mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Update the states of the keyboard and mouse.
        /// </summary>
        /// <param name="gameTime">time elapsed since last update</param>
        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastMouseState = mouseState;
            mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            gamePad = GamePad.GetState(0);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the required components to screen.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used to render to.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (showCustomCursor)
            {
                Vector2 mousePos = new Vector2(Mouse.X, Mouse.Y);
                spriteBatch.Draw(cursorTexture, mousePos, Color.White);
            }
        }

        /// <summary>
        /// Checks whether a key was released since the last update.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks whether a key was pressed since the last update.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Checks whether a key is down.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks whether a mouse button was pressed since the last update.
        /// </summary>
        /// <returns></returns>
        public static bool MousePressed(ButtonPressed buttonPressed)
        {
            if (buttonPressed == ButtonPressed.Left && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                return true;
            if (buttonPressed == ButtonPressed.Right && mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                return true;
            if (buttonPressed == ButtonPressed.Middle && mouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether a mouse button was released since the last update.
        /// </summary>
        /// <param name="buttonPressed"></param>
        /// <returns></returns>
        public static bool MouseReleased(ButtonPressed buttonPressed)
        {
            if (buttonPressed == ButtonPressed.Left && mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                return true;
            if (buttonPressed == ButtonPressed.Right && mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                return true;
            if (buttonPressed == ButtonPressed.Middle && mouseState.MiddleButton == ButtonState.Released && lastMouseState.MiddleButton == ButtonState.Pressed)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether a mouse button is down.
        /// </summary>
        /// <param name="buttonPressed"></param>
        /// <returns></returns>
        public static bool MouseDown(ButtonPressed buttonPressed)
        {
            if (buttonPressed == ButtonPressed.Left && mouseState.LeftButton == ButtonState.Pressed)
                return true;
            if (buttonPressed == ButtonPressed.Right && mouseState.RightButton == ButtonState.Pressed)
                return true;
            if (buttonPressed == ButtonPressed.Middle && mouseState.MiddleButton == ButtonState.Pressed)
                return true;
            return false;
        }

        /// <summary>
        /// Starts the keyboard listening for input.
        /// </summary>
        public static void KeyboardListen()
        {
            keyboardInputListening = true;
        }

        /// <summary>
        /// Stops the keyboard listening for input.
        /// </summary>
        public static void KeyboardStopListening()
        {
            keyboardInputListening = false;
        }

        /// <summary>
        /// Gets the keys being pressed.
        /// </summary>
        /// <returns></returns>
        public static List<Keys> GetKeys()
        {
            Keys[] keys = keyboardState.GetPressedKeys();
            List<Keys> keysList = new List<Keys>();
            foreach (Keys key in keys)
                if (KeyPressed(key))
                    keysList.Add(key);
            return keysList;
        }
    }
}
