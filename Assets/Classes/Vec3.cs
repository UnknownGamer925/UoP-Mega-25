using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vec3 : Mathlib
{
    //Variables and Constructor
    public float x;
    public float y;
    public float z;
    public Vec3(float vx, float vy, float vz)
    {
        x = vx; 
        y = vy; 
        z = vz;
    }
    public Vec3 empty
    {
        get {  return new Vec3(0, 0, 0); }
    }


    // Operator overlaods
    public static Vec3 operator +(Vec3 va, Vec3 vb)
    {
        return new Vec3(va.x + vb.x, va.y + vb.y, va.z + vb.z);
    }
    public static Vec3 operator -(Vec3 va, Vec3 vb)
    {
        return new Vec3(va.x - vb.x, va.y - vb.y, va.z - vb.z);
    }
    public static Vec3 operator *(Vec3 va, float f)
    {
        return new Vec3(va.x * f, va.y * f, va.z * f);
    }
    public static Vec3 operator *(float f, Vec3 va)
    {
        return new Vec3(va.x * f, va.y * f, va.z * f);
    }
    public static Vec3 operator /(Vec3 va, float f)
    {
        return new Vec3(va.x / f, va.y / f, va.z / f);
    }
    public static Vec3 operator /(float f, Vec3 va)
    {
        return new Vec3(f / va.x, f / va.y, f / va.z);
    }


    //Functions
    public static Vec3 Lerp(Vec3 va, Vec3 vb, float t)
    {
        return (va * (1.0f - t) + (vb * t));
    }


    // Conversion
    public Vector3 ToUnity()
    {
        return new Vector3(x, y, z);
    }
    public Vec4 ToVec4()
    {
        return new Vec4(x, y, z, 0);
    }
}
