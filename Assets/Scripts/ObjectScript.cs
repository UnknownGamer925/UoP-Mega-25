using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] public Vector2 EulerVisual;
    float mouse_x;
    float mouse_y;
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
            new Vec3(0, Mathf.Cos(euler.x), Mathf.Sin(euler.x)),
            new Vec3(0, -Mathf.Sin(euler.x), Mathf.Cos(euler.x)),
            Vec3.empty
        );

        Mat4X4 yawmat = new Mat4X4(
            new Vec3(Mathf.Cos(euler.y), 0, -Mathf.Sin(euler.y)),
            new Vec3(0, 1, 0),
            new Vec3(Mathf.Sin(euler.y), 0, Mathf.Cos(euler.y)),
            Vec3.empty
        );

        return yawmat * (pitchmat * rollmat);
    }
    Quat CalulateYaw(float mx)
    {
        Quat YawQuat = new Quat(mx, new Vec3(0, 1, 0));

        Quat pos = new Quat(1, Mathlib.ToMathlib(transform.position));

        return YawQuat * pos * YawQuat.Inverse();
    }
    Quat CalulatePitch(float my) 
    {
        Quat PitchQuat = new Quat(my, new Vec3(0, 0, 1));

        Quat pos = new Quat(1, Mathlib.ToMathlib(transform.position));

        return PitchQuat * pos * PitchQuat.Inverse();
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
        mouse_x += Input.GetAxisRaw("Mouse X") * 0.5f;
        mouse_y += Input.GetAxisRaw("Mouse Y") * 0.5f;
        EulerVisual.x = mouse_x;
        EulerVisual.y = mouse_y;


        transform.rotation = RotateMesh(new Vec3(mouse_x, mouse_y, 1)).ToQuat().ToUnity();

        
    }
}
