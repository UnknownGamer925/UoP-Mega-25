using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mathlib
{

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
    public Vector4 ToMathlib(Vector4 vector)
    {
        return new Vector4(vector.x,vector.y,vector.z,vector.w);
    }

}
