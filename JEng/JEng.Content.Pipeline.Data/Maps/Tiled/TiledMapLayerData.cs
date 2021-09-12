namespace JEng.Content.Pipeline.Data.Maps.Tiled
{
    public class TiledMapLayerData
    {
        public string Name { get; set; }
        public int[] Data { get; set; }
        public TiledMapLayerObjectData[] Objects { get; set; }
        public TiledMapLayerData[] Layers { get; set; }
    }
}
