using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Core.Graphics
{
    public class Resolution
    {
        private readonly int _virtualWidth;
        private readonly int _virtualHeight;

        private int _width;
        private int _height;

        private GraphicsDeviceManager _device;
        private bool _dirtyMatrix;

        public Matrix ScaleMatrix { get; private set; }

        public bool Fullscreen { get; set; }

        public float VirtualAspectRatio { get; }

        public Resolution(int width, int height, GraphicsDeviceManager device)
        {
            _virtualWidth = width;
            _virtualHeight = height;

            VirtualAspectRatio = (float)_virtualWidth / (float)_virtualHeight;

            _device = device;
            _dirtyMatrix = true;
            ApplyResolutionSettings();
        }

        public void SetResolution(int width, int height)
        {
            _width = width;
            _height = height;

            ApplyResolutionSettings();
        }

        public void BeginDraw()
        {
            SetVirtualViewport();
            _device.GraphicsDevice.Clear(Color.Black);
            SetViewport();
            _device.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        private void ApplyResolutionSettings()
        {
            if (Fullscreen)
            {
                ValidateAdapterSupport();

                SetDeviceSettings();
            }
            else
            {
                if (_width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width && _height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                {
                    SetDeviceSettings();
                }
                // TODO: and if not?
            }

            _dirtyMatrix = true;

            _width = _device.PreferredBackBufferWidth;
            _height = _device.PreferredBackBufferHeight;
        }

        private void SetDeviceSettings()
        {
            _device.PreferredBackBufferWidth = _width;
            _device.PreferredBackBufferHeight = _height;
            _device.IsFullScreen = Fullscreen;
            _device.ApplyChanges();
        }

        private void ValidateAdapterSupport()
        {
            foreach (var mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (mode.Width == _width && mode.Height == _height) return;
            }

            throw new Exception($"The provided resolution ({_width}, {_height}) is not supported by the display adapter.");
        }

        private void BuildMatrix()
        {
            ScaleMatrix = Matrix.CreateScale(
                (float)_device.GraphicsDevice.Viewport.Width / _virtualWidth,
                (float)_device.GraphicsDevice.Viewport.Height / _virtualHeight,
                1f);
            _dirtyMatrix = false;
        }

        private void SetVirtualViewport() =>_device.GraphicsDevice.Viewport = new Viewport(0, 0, _width, _height);
        
        private void SetViewport()
        {
            int width = _device.PreferredBackBufferWidth;
            int height = (int)(width / VirtualAspectRatio + 0.5f);
            bool changed = false;

            if (height > _device.PreferredBackBufferHeight)
            {
                height = _device.PreferredBackBufferHeight;

                // Pillar
                width = (int)(height * VirtualAspectRatio + 0.5f);
                changed = true;
            }

            Viewport viewport = new Viewport();

            viewport.X = (_device.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (_device.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed) _dirtyMatrix = true;

            _device.GraphicsDevice.Viewport = viewport;
        }
    }
}
