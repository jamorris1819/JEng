using Microsoft.Xna.Framework;

namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapLayerObjectData
    {
        public bool Ellipse { get; set; }
        public TiledMapTilePolygonPointData[] Polygon { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public TiledMapTilePropertyData[] Properties { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
