using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Transform.Rotation;

public interface IRotation
{
    public Vec3 RotateVector(Vec3 vector);
    public Vec3 InverseRotateVector(Vec3 vector);
    public IRotation Inverse();
    public IRotation Combine(IRotation rotation);
    public IRotation Clone();
}
