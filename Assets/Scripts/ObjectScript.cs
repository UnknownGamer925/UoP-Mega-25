using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] public Vector3 EulerVisual;
    MeshFilter mf;

    public Mat4X4 RotateMesh(Vec3 euler)
    {


        Mat4X4 rollmat = new Mat4X4(
            new Vec3(Mathf.Cos(euler.z), Mathf.Sin(euler.z), 0),
            new Vec3(-Mathf.Sin(euler.z), Mathf.Cos(euler.z), 0),
            new Vec3(0, 0, 1),
            Vec3.empty
        );

        Mat4X4 pitchmat = new Mat4X4(
            new Vec3(1, 0, 0),
            new Vec3(0, Mathf.Cos(euler.y), Mathf.Sin(euler.y)),
            new Vec3(0, -Mathf.Sin(euler.y), Mathf.Cos(euler.y)),
            Vec3.empty
        );

        Mat4X4 yawmat = new Mat4X4(
            new Vec3(Mathf.Cos(euler.x), 0, -Mathf.Sin(euler.x)),
            new Vec3(0, 1, 0),
            new Vec3(Mathf.Sin(euler.x), 0, Mathf.Cos(euler.x)),
            Vec3.empty
        );

        return yawmat * (pitchmat * rollmat);
    }

    void ApplyMeshTransform(Mat4X4 Rotation)
    {
        Vector3[] Vertices = mf.mesh.vertices;
        Vector3[] transformedVertices = new Vector3[Vertices.Length];

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = Rotation * new Vector4(Vertices[i].x, Vertices[i].y, Vertices[i].z, 1);
        }

        mf.mesh.vertices = transformedVertices;
        mf.mesh.RecalculateNormals();
        mf.mesh.RecalculateBounds();
    }

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        EulerVisual.x -= Input.GetAxisRaw("Mouse X") * 0.3f;
        EulerVisual.y += Input.GetAxisRaw("Mouse Y") * 0.3f;


        Quat M = RotateMesh(Mathlib.ToMathlib(EulerVisual)).ToQuat();
        //Debug.Log(M.x + " " + M.y + " " + M.z + " " + M.w);
        transform.rotation = M.ToUnity();
        
    }
}
