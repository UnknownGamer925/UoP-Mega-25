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
        //return new Quat(((s.w * r.w) - ));
        return new Quat(0,0,0,0);
    }

    //Functions
    public Quat Inverse()
    {
        return new Quat(w, -axis);
    }


    //Conversion
    public Quaternion ToUnity() 
    {
        return new Quaternion(x, y, z, w);
    }
}
