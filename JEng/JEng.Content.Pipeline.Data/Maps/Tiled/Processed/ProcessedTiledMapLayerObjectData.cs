using Microsoft.Xna.Framework;

namespace JEng.Content.Pipeline.Data.Maps.Tiled.Processed
{
    public class ProcessedTiledMapLayerObjectData
    {
        public string Name { get; set; }
        public TiledMapTilePolygonPointData[] Polygon { get; set; }
        public bool Ellipse { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public TiledMapTilePropertyData[] Properties { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public ProcessedTiledMapLayerObjectData() { }
        
        public ProcessedTiledMapLayerObjectData(TiledMapLayerObjectData data)
        {
            Ellipse = data.Ellipse;
            Height = data.Height;
            Width = data.Width;
            Properties = data.Properties;
            X = data.X;
            Y = data.Y;
            Polygon = data.Polygon;
        }
    }
}
