using CSharp_ASCII_Render_Engine.ScreenRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Utils
{
    public static class ShapeStore
    {
        private static List<ShapeData> shapes = new List<ShapeData>();
        private static readonly object lockObject = new object();

        public static void AddShape(ShapeData shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
            }
        }

        public static IEnumerable<ShapeData> GetShapes()
        {
            lock (lockObject)
            {
                return shapes.ToArray(); // Return a copy to avoid outside modification.
            }
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
