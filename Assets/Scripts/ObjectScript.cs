using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] public Vector3 EulerVisual;
    [SerializeField] Camera Cam;
    [SerializeField] GameObject ItemPoint;
    private bool focus;
    Vec3 origin_point;

    Vector3[] ModelSpaceVertices;
    Vector3[] transformedVertices;
    MeshFilter mf;
    Vec3 Scalevec;

    Vec3 pos;
    Vec3 campos;
    bool once;
    float timer;
    bool activate;



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

    public void ScaleMesh(float scale)
    {
        Mat4X4 matrix =  new Mat4X4(
            new Vec3(1, 0, 0) * scale,
            new Vec3(0, 1, 0) * scale,
            new Vec3(0, 0, 1) * scale,
            Vec3.empty
        );

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = matrix * ModelSpaceVertices[i]; //new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
        }

        mf.mesh.vertices = transformedVertices;
        mf.mesh.RecalculateNormals();
        mf.mesh.RecalculateBounds();
    }

    public void FocusToggle()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            focus = false;
            timer = 0;


        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            focus = true;
            timer = 0;


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        ModelSpaceVertices = mf.mesh.vertices;
        transformedVertices = new Vector3[ModelSpaceVertices.Length];
        Scalevec = Vec3.empty;
        

        pos = Mathlib.ToMathlib(ItemPoint.transform.position);
        origin_point = new Vec3(-4,7,6);
    }

    // Update is called once per frame
    void Update()
    {
        FocusToggle();


        if (focus)
        {
            campos = Mathlib.ToMathlib(ItemPoint.transform.position);
            if (timer <= 1)
            {
                timer += 0.02f;
            }
            transform.position = Mathlib.Lerp(origin_point, campos, timer).ToUnity();

            EulerVisual.x += Input.GetAxisRaw("Mouse X") * 0.3f;
            EulerVisual.y += Input.GetAxisRaw("Mouse Y") * 0.3f;

            Vec3 EulerVec3 = Mathlib.ToMathlib(EulerVisual);

            Quat RelativeRot = RotateMesh(EulerVec3).ToQuat();

            transform.rotation = RelativeRot.ToUnity();
        }
        else
        {
            if (timer <= 1)
            {
                timer += Time.deltaTime;
            }
            transform.position = Mathlib.Lerp(Mathlib.ToMathlib(transform.position), origin_point, timer).ToUnity();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //Scalevec = Vec3.empty;
        }
        else if (Input.GetKey(KeyCode.G))
        {

            Scalevec.x += Input.GetAxisRaw("Mouse X") * 0.3f;
            Scalevec.y += Input.GetAxisRaw("Mouse Y") * 0.3f;
            ScaleMesh(Scalevec.x + Scalevec.y);
        }

    }
}