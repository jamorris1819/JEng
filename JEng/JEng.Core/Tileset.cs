using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Tilesets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEng.Core
{
    public class Tileset
    {
        private GraphicsDevice graphicsDevice;
        private TilesetData tilesetData;

        private int startTile;
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
            get { return (int)tilesetData.TilesetWidth; }
        }

        /// <summary>
        /// Returns the height in pixels of the tileset.
        /// </summary>
        public int Height
        {
            get { return (int)tilesetData.TilesetHeight; }
        }

        /// <summary>
        /// Returns the dimensions of each individual tile.
        /// </summary>
        public Vector2 TileDimensions
        {
            get => new Vector2(
                tilesetData.TilesetWidth / tilesetData.TilesWide,
                tilesetData.TilesetHeight / tilesetData.TilesHigh
                );
        }

        public Vector2 Dimensions
        {
            get => new Vector2(Width, Height);
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

        public Tileset(GraphicsDevice graphicsDevice, Texture2D baseImage, TilesetData tilesetData)
        {
            margin = 0;
            spacing = 0;
            this.graphicsDevice = graphicsDevice;
            this.tilesetData = tilesetData;
        }

        /// <summary>
        /// Constructor for the tileset class.
        /// </summary>
        /// <param name="baseImage">Image to use for the tileset</param>
        /// <param name="tileDimensions">Size of each tile</param>
        public Tileset(GraphicsDevice graphicsDevice, Texture2D baseImage, Vector2 tileDimensions)
        {
            this.graphicsDevice = graphicsDevice;
            this.baseImage = baseImage;
            this.margin = 0;
            this.spacing = 0;
            count.X = baseImage.Width / tileDimensions.X;
            count.Y = baseImage.Height / tileDimensions.Y;
            //Process();
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
            this.graphicsDevice = graphicsDevice;
            this.baseImage = baseImage;
            this.margin = margin;
            this.spacing = separation;
            this.count = count;
            //Process();
        }

        /// <summary>
        /// Processes the tileset image and populates the tile array.
        /// </summary>
        private void Process()
        {
            tiles = new Texture2D[tilesetData.TilesWide, tilesetData.TilesHigh];
            for (int i = 0; i < count.X; i++)
            {
                for (int j = 0; j < count.Y; j++)
                {
                    Texture2D imagePiece = new Texture2D(graphicsDevice, (int)TileDimensions.X, (int)TileDimensions.Y);
                    Color[] data = new Color[(int)(TileDimensions.X * TileDimensions.Y)];
                    // Take the color data from the area we want.
                    Rectangle rect = new Rectangle();
                    rect.X = margin + (i * spacing) + (i * (int)TileDimensions.X);
                    rect.Y = margin + (j * spacing) + (j * (int)TileDimensions.Y);
                    rect.Width = (int)TileDimensions.X;
                    rect.Height = (int)TileDimensions.Y;
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
