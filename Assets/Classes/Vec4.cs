using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec4 : Mathlib
{
    // Variables and Constructor
    public float x;
    public float y;
    public float z; 
    public float w;
    public Vec4(float vx, float vy, float vz, float vw)
    {
        x = vx; 
        y = vy; 
        z = vz; 
        w = vw;
    }
    public Vec4 empty
    {
        get
        {
            return new Vec4(0,0,0,0);
        }
    }


    //Operator overloads
    public static Vec4 operator +(Vec4 va, Vec4 vb)
    {
        return new Vec4(va.x + vb.x, va.y + vb.y, va.z + vb.z, va.w + vb.w);
    }
    public static Vec4 operator -(Vec4 va, Vec4 vb)
    {
        return new Vec4(va.x - vb.x, va.y - vb.y, va.z - vb.z, va.w - vb.w);
    }
    public static Vec4 operator *(Vec4 va, float f)
    {
        return new Vec4(va.x * f, va.y * f, va.z * f, va.w * f);
    }
    public static Vec4 operator *(float f, Vec4 va)
    {
        return new Vec4(va.x * f, va.y * f, va.z * f, va.w * f);
    }
    public static Vec4 operator /(Vec4 va, float f)
    {
        return new Vec4(va.x / f, va.y / f, va.z / f, va.w / f);
    }
    public static Vec4 operator /(float f, Vec4 va)
    {
        return new Vec4(f / va.x, f / va.y, f / va.z, f / va.w);
    }


    //Conversion
    public Vector4 ToUnity()
    {
        return new Vector4(x, y, z, w);
    }

}
