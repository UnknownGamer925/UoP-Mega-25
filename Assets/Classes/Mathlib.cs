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
    public static float DotProduct(Vec3 va, Vec3 vb, bool normalize)
    {
        Vec3 van;
        Vec3 vbn;
        
        if (normalize) 
        {
            van = va.Normalized();
            vbn = vb.Normalized();
        }
        else
        {
            van = va;
            vbn = vb;
        }

        return ((van.x * vbn.x) + (van.y * vbn.y) + (van.z * vbn.z));
    }
    public static float DotProduct(Vec3 va, Vec3 vb)
    {
        return ((va.x * vb.x) + (va.y * vb.y) + (va.z * vb.z));
    }


    // Interpolation Functions
    public static Vec3 Lerp(Vec3 va, Vec3 vb, float t)
    {
        return (va * (1.0f - t) + (vb * t));
    }
    public static Quat Slerp(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);

        Quat d = r * q.Inverse();
        Vec4 axis_angle = d.AxisAngle();

        return new Quat(axis_angle.w * t, new Vec3(axis_angle.x, axis_angle.y, axis_angle.z)) * q; // return dT * q
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
    public static Quat ToMathlib(Quaternion quat)
    {
        return new Quat(quat.w, quat.x, quat.y, quat.z);
    }

}
