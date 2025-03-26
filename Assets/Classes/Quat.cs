using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

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
        w = qw;
        x = qx;
        y = qy;
        z = qz;
    }
    public Vec3 axis
    {
        get { return new Vec3(x, y, z); }
        set { x = value.x; y = value.y; z = value.z; }
    }


    //Operator overload
    public static Quat operator *(Quat r, Quat s)
    {
        return new Quat(((s.w * r.w) - Mathlib.DotProduct(r.axis, s.axis, true)), ((s.w * r.axis) + (r.w * s.axis)));
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


    //Conversion
    public Quaternion ToUnity() 
    {
        return new Quaternion(x, y, z, w);
    }
}
