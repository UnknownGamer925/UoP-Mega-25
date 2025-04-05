using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat4X4 : Mathlib
{
    public float[,] table; // [row, col]

    public Mat4X4(Vec4 va, Vec4 vb, Vec4 vc, Vec4 vd)
    {
        table = new float[4, 4];
        //     column 1    |      column2      |      column 3     |      column 4     |
        table[0, 0] = va.x; table[0, 1] = vb.x; table[0, 2] = vc.x; table[0, 3] = vd.x;
        table[1, 0] = va.y; table[1, 1] = vb.y; table[1, 2] = vc.y; table[1, 3] = vd.y;
        table[2, 0] = va.z; table[2, 1] = vb.z; table[2, 2] = vc.z; table[2, 3] = vd.z;
        table[3, 0] = va.w; table[3, 1] = vb.w; table[3, 2] = vc.w; table[3, 3] = vd.w;
    }
    public Mat4X4(Vec3 va, Vec3 vb, Vec3 vc, Vec3 vd)
    {
        table = new float[4, 4];
        //     column 1    |      column2      |      column 3     |      column 4     |
        table[0, 0] = va.x; table[0, 1] = vb.x; table[0, 2] = vc.x; table[0, 3] = vd.x;
        table[1, 0] = va.y; table[1, 1] = vb.y; table[1, 2] = vc.y; table[1, 3] = vd.y;
        table[2, 0] = va.z; table[2, 1] = vb.z; table[2, 2] = vc.z; table[2, 3] = vd.z;
        table[3, 0] = 0;    table[3, 1] = 0;    table[3, 2] = 0;    table[3, 3] = 0;
    }
    public Mat4X4 identity
    {
        get {
            return new Mat4X4(
                new Vec4(1, 0, 0, 0),
                new Vec4(0, 1, 0, 0),
                new Vec4(0, 0, 1, 0),
                new Vec4(0, 0, 0, 1)
            );
        }
    }
    public Vec4 col_1
    {
        get { return new Vec4(table[0, 0], table[1, 0], table[2, 0], table[3, 0]); }
    }
    public Vec4 col_2
    {
        get { return new Vec4(table[0, 1], table[1, 1], table[2, 1], table[3, 1]); }
    }
    public Vec4 col_3
    {
        get { return new Vec4(table[0, 2], table[1, 2], table[2, 2], table[3, 2]); }
    }
    public Vec4 col_4
    {
        get { return new Vec4(table[0, 3], table[1, 3], table[2, 3], table[3, 3]); }
    }


    //Operator overlaods
    public static Vector4 operator *(Mat4X4 mtx, Vector4 vec)
    {
        return new Vector4(
            ((mtx.table[0, 0] * vec.x) + (mtx.table[0, 1] * vec.y) + (mtx.table[0, 2] * vec.z) + (mtx.table[0, 3] * vec.w)),
            ((mtx.table[1, 0] * vec.x) + (mtx.table[1, 1] * vec.y) + (mtx.table[1, 2] * vec.z) + (mtx.table[1, 3] * vec.w)),
            ((mtx.table[2, 0] * vec.x) + (mtx.table[2, 1] * vec.y) + (mtx.table[2, 2] * vec.z) + (mtx.table[2, 3] * vec.w)),
            ((mtx.table[3, 0] * vec.x) + (mtx.table[3, 1] * vec.y) + (mtx.table[3, 2] * vec.z) + (mtx.table[3, 3] * vec.w))
        );
    }
    public static Mat4X4 operator *(Mat4X4 mta, Mat4X4 mtb)
    {
        return new Mat4X4(
            new Vec4(
                mta.table[0, 0] * mtb.table[0, 0],
                mta.table[0, 1] * mtb.table[0, 1],
                mta.table[0, 2] * mtb.table[0, 2],
                mta.table[0, 3] * mtb.table[0, 3]
            ),
            new Vec4(
                mta.table[1, 0] * mtb.table[1, 0],
                mta.table[1, 1] * mtb.table[1, 1],
                mta.table[1, 2] * mtb.table[1, 2],
                mta.table[1, 3] * mtb.table[1, 3]
                ),
            new Vec4(
                mta.table[2, 0] * mtb.table[2, 0],
                mta.table[2, 1] * mtb.table[2, 1],
                mta.table[2, 2] * mtb.table[2, 2],
                mta.table[2, 3] * mtb.table[2, 3]
            ),
            new Vec4(
                mta.table[3, 0] * mtb.table[3, 0],
                mta.table[3, 1] * mtb.table[3, 1],
                mta.table[3, 2] * mtb.table[3, 2],
                mta.table[3, 3] * mtb.table[3, 3]
            )
        );
    }   

    //Conversion
    public Matrix4x4 ToUnity()
    {
        return new Matrix4x4(col_1.ToUnity(), col_2.ToUnity(), col_3.ToUnity(), col_4.ToUnity());
    }
    public Quat ToQuat()
    {
        float T =  1 + table[0,0] + table[1,1] + table[2,2];

        float S = Mathf.Sqrt(T);
        return new Quat(
            (0.25f * S) * 2, 
            ((table[1, 2] - table[2, 1]) / S), 
            ((table[1, 0] - table[0, 2]) / S), 
            ((table[0, 1] - table[1, 0]) / S)
        );
    }
}
