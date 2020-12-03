using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Graphics
{
    /// <summary>
    /// A class for helping to deal with the resolution scaling.
    /// </summary>
    public class Resolution
    {
        private int defaultResolutionWidth;
        private int defaultResolutionHeight;

        private float deviceAspectRatio;

        public int deviceResolutionHeight;
        public int deviceResolutionWidth;
        
        private int renderWidth;
        private int renderHeight;

        private List<Vector2> resolutions;
        private Dictionary<string, Vector2> resolutionDictionary;

        private RenderTarget2D renderTarget;
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private GameWindow window;

        public int RenderWidth
        {
            get { return renderWidth; }
            set { renderWidth = value; }
        }

        public int RenderHeight
        {
            get { return renderHeight; }
            set { renderHeight = value; }
        }

        public List<Vector2> Resolutions
        {
            get { return resolutions; }
            set
            {
                resolutions = value;

                resolutionDictionary = new Dictionary<string, Vector2>();
                for (int i = 0; i < resolutions.Count; i++)
                {
                    resolutionDictionary.Add(resolutions[i].X + "x" + resolutions[i].Y, resolutions[i]);
                }
            }
        }

        public Dictionary<string, Vector2> ResolutionDictionary
        {
            get { return resolutionDictionary; }
        }

        public Resolution(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameWindow window)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.window = window;

            defaultResolutionWidth = 1600;
            defaultResolutionHeight = 900;

            deviceResolutionWidth = 1920;
            deviceResolutionHeight = 1080;

            renderWidth = 1920;
            renderHeight = 1080;

            resolutions = new List<Vector2>();
            resolutionDictionary = new Dictionary<string, Vector2>();
        }

        /// <summary>
        /// Initialise the size of the canvas to be rendered to.
        /// </summary>
        public void Initialise()
        {
            graphics.PreferredBackBufferWidth = renderWidth;
            graphics.PreferredBackBufferHeight = renderHeight;

            ApplyResolution();
        }

        /// <summary>
        /// Changes the resolution.
        /// </summary>
        /// <param name="width">The new width</param>
        /// <param name="height">The new height</param>
        public void ChangeResolution(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            ApplyResolution();
        }

        /// <summary>
        /// Changes the resolution.
        /// </summary>
        /// <param name="index">The index of the resolutions.</param>
        public void ChangeResolution(int index)
        {
            Vector2 resolution = resolutions[index];
            graphics.PreferredBackBufferWidth = (int)resolution.X;
            graphics.PreferredBackBufferHeight = (int)resolution.Y;

            ApplyResolution();
        }

        /// <summary>
        /// Applies the current resolution.
        /// </summary>
        private void ApplyResolution()
        {
            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, renderWidth, renderHeight, false,
                SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            deviceResolutionWidth = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            deviceResolutionHeight = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            deviceAspectRatio = (float)deviceResolutionWidth / (float)deviceResolutionHeight;

            if (deviceResolutionHeight <= 1080)
            {
                // If the screen is smaller or equal to full HD, then fullscreen.
                // If the device screen resolution is equal or less than full HD, then fullscreen the application.
                Vector2 currentResolution = new Vector2(deviceResolutionWidth, deviceResolutionHeight);
                bool validResolution = resolutions.Contains(currentResolution);
                graphics.PreferredBackBufferWidth = validResolution ? deviceResolutionWidth : defaultResolutionWidth;
                graphics.PreferredBackBufferHeight = validResolution ? deviceResolutionHeight : defaultResolutionHeight;
                graphics.IsFullScreen = validResolution;
            }

            InputHandler.DeviceResolution = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            InputHandler.VirtualResolution = new Vector2(RenderWidth, RenderHeight);

            window.Position = new Point((int)(deviceResolutionWidth - graphics.PreferredBackBufferWidth) / 2,
                (int)(deviceResolutionHeight - graphics.PreferredBackBufferHeight) / 2);

            graphics.ApplyChanges();
        }

        /// <summary>
        /// Begin rendering to the render target.
        /// </summary>
        public void BeginRender()
        {
            graphics.GraphicsDevice.SetRenderTarget(renderTarget);
        }
        
        /// <summary>
        /// Stops rendering to the render target, and then draws the render target to the screen.
        /// </summary>
        public void EndRender()
        {
            // The below code is derivation from this article: http://www.infinitespace-studios.co.uk/general/monogame-scaling-your-game-using-rendertargets-and-touchpanel/

            float chosenRatio = window.ClientBounds.Width / (float)window.ClientBounds.Height;
            float preferredRatio = 16.0f / 9.0f;
            bool barsOnTop = chosenRatio <= preferredRatio;

            // Calculate the width and height of the bars.
            // If the aspect ratio is smaller than default, calculate the width of bars, and vice versa.
            int presentHeight = barsOnTop ? (int)((window.ClientBounds.Width / preferredRatio) + 0.5f) : window.ClientBounds.Height;
            int presentWidth = barsOnTop ? window.ClientBounds.Width : (int)((window.ClientBounds.Height * preferredRatio) + 0.5f);
            int barHeight = (window.ClientBounds.Height - presentHeight) / 2;
            int barWidth = (window.ClientBounds.Width - presentWidth) / 2;

            Rectangle letterbox = new Rectangle(barWidth, barHeight, presentWidth, presentHeight);

            InputHandler.LetterboxSize = new Vector2(letterbox.X, letterbox.Y);

            graphics.GraphicsDevice.SetRenderTarget(null);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            spriteBatch.Draw(renderTarget, letterbox, Color.White);
            spriteBatch.End();
        }
    }
}
