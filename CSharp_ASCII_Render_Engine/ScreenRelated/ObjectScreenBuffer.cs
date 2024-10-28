using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class ObjectScreenBuffer
    {
        int PosX;
        int PosY;
        int Width;
        int Height;

        List<List<Vec2>> ObjectBuffer;

        public ObjectScreenBuffer()
        {
            ObjectBuffer = new();
            ObjectBuffer[20][50] = new Vec2(2, 3);
        }
    }
}
