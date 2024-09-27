using CSharp_ASCII_Render_Engine.ScreenRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Geometry.Lines
{
    internal class LineRenderer
    {
    }

    public interface ILineRenderer
    {
        public void Draw();
        public ScreenBuffer Render();
    }
}
