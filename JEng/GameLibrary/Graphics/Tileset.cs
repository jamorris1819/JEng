// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary.Graphics
{
    /// <summary>
    /// A class which holds all the individual images in a tileset.
    /// </summary>
    public class Tileset
    {
        private GraphicsDevice graphicsDevice;

        private Vector2 dimensions;
        private Vector2 tileDimensions;
        private int margin;
        private int spacing;
        private Vector2 count;

        private Texture2D baseImage;
        private Texture2D[,] tiles;

        /// <summary>
        /// Returns the width in pixels of the tileset.
        /// </summary>
        public int Width
        {
            get { return (int)dimensions.X; }
        }

        /// <summary>
        /// Returns the height in pixels of the tileset.
        /// </summary>
        public int Height
        {
            get {  return (int)dimensions.Y; }
        }

        /// <summary>
        /// Returns the dimensions of each individual tile.
        /// </summary>
        public Vector2 TileDimensions
        {
            get { return tileDimensions; }
        }

        public Texture2D this[int i, int j]
        {
            get { return GetTile(i, j); }
        }

        public Texture2D this[int i]
        {
            get
            {
                int x = i;
                int y = 0;
                while (x > count.X)
                {
                    x -= (int)count.X;
                    y++;
                }
                return this[x, y];
            }
        }

        /// <summary>
        /// Constructor for the tileset class.
        /// </summary>
        /// <param name="baseImage">Image to use for the tileset</param>
        /// <param name="tileDimensions">Size of each tile</param>
        public Tileset(GraphicsDevice graphicsDevice, Texture2D baseImage, Vector2 tileDimensions)
        {
            this.dimensions = new Vector2(baseImage.Width, baseImage.Height);
            this.graphicsDevice = graphicsDevice;
            this.tileDimensions = tileDimensions;
            this.baseImage = baseImage;
            this.margin = 0;
            this.spacing = 0;
            count.X = baseImage.Width / tileDimensions.X;
            count.Y = baseImage.Height / tileDimensions.Y;
            Process();
        }

        /// <summary>
        /// Constructor for the tileset class.
        /// </summary>
        /// <param name="baseImage">Image to use for the tileset</param>
        /// <param name="tileDimensions">Size of each tile</param>
        /// <param name="margin">Initial offset in pixels</param>
        /// <param name="separation">Spacing between each tile</param>
        public Tileset(GraphicsDevice graphicsDevice, Texture2D baseImage, Vector2 tileDimensions, int margin, int separation, Vector2 count)
        {
            this.dimensions = new Vector2(baseImage.Width, baseImage.Height);
            this.graphicsDevice = graphicsDevice;
            this.tileDimensions = tileDimensions;
            this.baseImage = baseImage;
            this.margin = margin;
            this.spacing = separation;
            this.count = count;
            Process();
        }

        /// <summary>
        /// Processes the tileset image and populates the tile array.
        /// </summary>
        private void Process()
        {
            tiles = new Texture2D[(int)(dimensions.X / tileDimensions.X), (int)(dimensions.Y / tileDimensions.Y)];
            for (int i = 0; i < count.X; i++)
            {
                for (int j = 0; j < count.Y; j++)
                {
                    Texture2D imagePiece = new Texture2D(graphicsDevice, (int)tileDimensions.X, (int)tileDimensions.Y);
                    Color[] data = new Color[(int)(tileDimensions.X * tileDimensions.Y)];
                    // Take the color data from the area we want.
                    Rectangle rect = new Rectangle();
                    rect.X = margin + (i * spacing) + (i * (int)tileDimensions.X);
                    rect.Y = margin + (j * spacing) + (j * (int)tileDimensions.Y);
                    rect.Width = (int)tileDimensions.X;
                    rect.Height = (int)tileDimensions.Y;
                    baseImage.GetData(0, rect, data, 0, data.Length);
                    // Copy it into our new piece.
                    imagePiece.SetData(data);
                    tiles[i, j] = imagePiece;
                }
            }
        }

        /// <summary>
        /// Returns the tile at the specified position.
        /// </summary>
        /// <param name="x">X coordinate of tile</param>
        /// <param name="y">Y coordinate of tile</param>
        /// <returns></returns>
        public Texture2D GetTile(int x, int y)
        {
            return tiles[x, y];
        }
    }
}
