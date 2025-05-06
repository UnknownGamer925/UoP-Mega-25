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

    Vector3[] ModelSpaceVertices;
    Vector3[] transformedVertices;
    MeshFilter mf;
    Vec3 Scalevec;

    Vec3 pos;
    Vec3 campos;
    bool once;


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

    public void ScaleMesh(Vec3 vec)
    {
        Mat4X4 matrix =  new Mat4X4(
            new Vec3(1, 0, 0) * vec.x,
            new Vec3(0, 1, 0) * vec.x,
            new Vec3(0, 0, 1) * vec.x,
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            focus = true;


        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            focus = false;
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        ModelSpaceVertices = mf.mesh.vertices;
        transformedVertices = new Vector3[ModelSpaceVertices.Length];

        pos = Mathlib.ToMathlib(ItemPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        FocusToggle();
        
        
        if (Input.GetKey(KeyCode.E))
        {
            if (!focus)
            {
                //campos = Mathlib.ToMathlib(ItemPoint.transform.position);
                transform.position = Mathlib.Lerp(campos, pos, 0.8f).ToUnity();
            }
            else if (focus)
            {
                campos = Mathlib.ToMathlib(ItemPoint.transform.position);
                transform.position = Mathlib.Lerp(pos, campos, 0.8f).ToUnity();

            }
            Debug.Log(pos.ToUnity());

            EulerVisual.x += Input.GetAxisRaw("Mouse X") * 0.3f;
            EulerVisual.y += Input.GetAxisRaw("Mouse Y") * 0.3f;

            Vec3 EulerVec3 = Mathlib.ToMathlib(EulerVisual);

            Quat RelativeRot = RotateMesh(EulerVec3).ToQuat();

            transform.rotation = RelativeRot.ToUnity();
        }

        if (Input.GetKey(KeyCode.G))
        {
            if (!once)
            {
                Scalevec = Vec3.empty;
                once = true;
            }
            
            Scalevec.x += Input.GetAxisRaw("Mouse X") * 0.3f;
            Scalevec.y += Input.GetAxisRaw("Mouse Y") * 0.3f;
            ScaleMesh(Scalevec);
        }
        else
        {
            once = false;
        }


        

        
        
    }
}