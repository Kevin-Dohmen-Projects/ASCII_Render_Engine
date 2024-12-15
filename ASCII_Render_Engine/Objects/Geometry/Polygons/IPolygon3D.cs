using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public interface IPolygon3D : IRenderable
{
    public Vertex3D[] Vertices { get; set; }
    public IPoly3DRenderer? Renderer { get; set; }
    public CameraConfig? Camera { get; set; }
    public IPolygon3D Copy();
}
