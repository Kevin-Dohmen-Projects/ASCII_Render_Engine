using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Types.Pixels
{
    public struct ShaderPixel
    {
        Vec2? ScreenPos;
        Vec2? ScreenRes;
        Vec2 UV;
        double? Time;


        public ShaderPixel(Vec2 screenPos, Vec2 screnRes, Vec2 uv, double time)
        {
            ScreenPos = screenPos;
            ScreenRes = screnRes;
            UV = uv;
            Time = time;
        }

        public ShaderPixel()
        {
            Vec2 UV = new Vec2();
        }


    }
}
