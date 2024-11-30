using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Camera;

public class CameraConfig
{
    public ICamera Camera { get; set; }

    public CameraConfig(ICamera camera)
    {
        Camera = camera;
    }
}
