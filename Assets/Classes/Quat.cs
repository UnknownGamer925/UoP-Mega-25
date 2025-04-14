using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Quat : Mathlib
{
    // Variables and Constructor
    public float w, x, y, z;
    public Quat(float angle, Vec3 axis)
    {
        float half_angle = angle / 2;
        w = Mathf.Cos(half_angle);
        x = axis.x * Mathf.Sin(half_angle);
        y = axis.y * Mathf.Cos(half_angle);
        z = axis.z * Mathf.Sin(half_angle);
    }
    public Quat(float qw, float qx, float qy, float qz)
    {
        float half_angle = qw / 2;
        w = Mathf.Cos(half_angle);
        x = qx * Mathf.Sin(half_angle);
        y = qy * Mathf.Cos(half_angle);
        z = qz * Mathf.Sin(half_angle);
    }
    public Vec3 axis
    {
        get { return new Vec3(x, y, z); }
        set { x = value.x; y = value.y; z = value.z; }
    }


    //Operator overload
    public static Quat operator *(Quat r, Quat s)
    {
        return new Quat(((s.w * r.w) - Mathlib.DotProduct(r.axis, s.axis, true)), ((s.w * r.axis) + (r.w * s.axis) + Mathlib.CrossProduct(r.axis, s.axis)));
    }

    //Functions
    public Quat Inverse()
    {
        return new Quat(w, -axis);
    }
    public Vec4 AxisAngle()
    {
        float halfAngle = Mathf.Acos(w);
        return new Vec4(
            (x / Mathf.Sin(halfAngle)),
            (y / Mathf.Sin(halfAngle)),
            (z / Mathf.Sin(halfAngle)),
            halfAngle * 2
            );
    }
    public Quat Normalised()
    {
        return new Quat(w / Length(), x / Length(), y / Length(), z / Length());
    }
    public float Length()
    {
        return Mathf.Sqrt((w*w) + (x*x) + (y*y) + (z*z));
    }


    //Conversion
    public Quaternion ToUnity() 
    {
        return new Quaternion(x, y, z, w);
    }
    public Mat4X4 ToMat4X4()
    {
        return new Mat4X4(
            new Vec4((1 - 2 * (y*y + z*z)), (2 * (x*y + z*w)), (2 * (x*z - y*w)), 0),
            new Vec4((2 * (x*y - z*w)), (1 - 2 * (x*x + z*z)), (2 * (y*z + x*w)), 0),
            new Vec4((2 * (x*z + y*w)), (2 * (y*z - x*w)), (1 - 2 * (x*x + y*y)), 0),
            new Vec4(0, 0, 0, 1)

        );
    }
    public Vec4 ToVec4()
    {
        return new Vec4 (x, y, z, w);
    }
}
