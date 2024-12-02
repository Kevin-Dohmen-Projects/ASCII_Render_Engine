using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Rendering.Geometry.LineRenderer;

public interface ILine2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Line2D obj) { }
}
