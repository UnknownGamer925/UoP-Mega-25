using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
