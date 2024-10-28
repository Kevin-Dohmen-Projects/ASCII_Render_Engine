using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Shader;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Geometry.Primitives
{
    public class FullScreenShaderObject
    {
        Vec2 Color;
        IShader? Shader;

        public FullScreenShaderObject(IShader shader)
        {
            Shader = shader;
            Color = new Vec2();
        }
        public FullScreenShaderObject(Vec2 color)
        {
            Color = color;
            Shader = null;
        }

        //public void Render(ScreenBuffer buffer)
        //{
        //    int posx = (int)Math.Round(Pos.x);
        //    int posy = (int)Math.Round(Pos.y);
        //    int sizex = (int)Math.Round(Size.x);
        //    int sizey = (int)Math.Round(Size.y);

        //    for (int y = int.Max(posy, 0); y < int.Min(posy + sizey, buffer.Height); y++)
        //    {
        //        for (int x = int.Max(posx, 0); x < int.Min(posx + sizex, buffer.Height); x++)
        //        {
        //            buffer.Buffer[y][x] = Color;
        //        }
        //    }
        //}
    }
}
