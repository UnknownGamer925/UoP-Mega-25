using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Mathlib
{

    // Vector Functions
    public static float ToRadian(Vector2 vec)
    {  
        return Mathf.Atan(vec.y / vec.x);
    }
    public static Vec3 EulerToDirection(Vec3 v3)
    {
        return new Vec3(
            Mathf.Cos(v3.y) * Mathf.Sin(v3.z),
            Mathf.Sin(v3.z),
            Mathf.Cos(v3.z) * Mathf.Sin(v3.y)
        );
    }
    public static Vec3 CrossProduct(Vec3 va, Vec3 vb)
    {
        return new Vec3(
            (va.y * vb.z) - (va.z * vb.y),
            (va.z * vb.x) - (va.x - vb.z),
            (va.x * vb.y) - (va.y * va.x)
            );
    }


    //Conversions to Mathlib class
    public static Vec3 ToMathlib(Vector3 vector)
    {
        return new Vec3(vector.x,vector.y,vector.z);
    }
    public static Vec4 ToMathlib(Vector4 vector)
    {
        return new Vec4(vector.x,vector.y,vector.z,vector.w);
    }
    public static Quat ToMathLib(Quaternion quat)
    {
        return new Quat(quat.w, quat.x, quat.y, quat.z);
    }

}
