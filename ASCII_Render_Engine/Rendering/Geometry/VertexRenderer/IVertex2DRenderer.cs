using ASCII_Render_Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public interface IVertex2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj) { }
}
