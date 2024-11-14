using ASCII_Render_Engine.ScreenRelated;

namespace ASCII_Render_Engine.Utils
{
    public static class ShapeStore
    {
        private static List<ShapeData> shapes = new List<ShapeData>();
        private static readonly object lockObject = new object();
        public static int id = 0;
        public static void AddShape(ShapeData shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
                id++;
            }
        }
        public static IEnumerable<ShapeData> GetShapes()
        {
            lock (lockObject)
            {
                return shapes.ToArray(); // Return a copy to avoid outside modification.
            }
        }
        public static void RemoveShape(string name)
        {
            lock (lockObject)
            {
                shapes.RemoveAll(shape => shape.name == name);
            }
        }
        public static void ClearShapes() {
            lock (lockObject) { shapes.Clear(); }
        }
    }


    public struct ShapeData
    {
        public string name;
        public IRenderable shape;
        public ShapeData(string name, IRenderable shape)
        {
            this.name = name;
            this.shape = shape;
        }
    }
}
