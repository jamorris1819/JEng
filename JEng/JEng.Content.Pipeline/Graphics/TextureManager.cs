﻿using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEng.Content.Pipeline.Graphics
{
    internal class TextureManager
    {
        private ContentProcessorContext _context;
        private Dictionary<string, Texture2DContent> _textures;

        public string Root { get; set; } = "C:\\rpg\\Content\\tilesets\\";

        public TextureManager(ContentProcessorContext context)
        {
            _context = context;
            _textures = new Dictionary<string, Texture2DContent>();
        }

        public Texture2DContent Get(string name)
        {
            if (_textures.ContainsKey(name)) return _textures[name];

            var texture = _context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(Root + $"{name}.png"), "TextureProcessor");

            _textures.Add(name, texture);

            return _textures[name];
        }
    }
}
