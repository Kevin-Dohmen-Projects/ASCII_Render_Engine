using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Mesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Rendering.Geometry.Mesh3DRenderer;

public interface IMesh3DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Mesh3D obj) { }
}
