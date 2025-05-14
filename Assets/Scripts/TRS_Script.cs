using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRS_Script : MonoBehaviour
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Vector3 rotation;
    [SerializeField] public Vector3 scale;

    Vec3 LoopVec; //control variable

    MeshFilter meshFilter;
    bool flip;

    Mesh mesh;
    Vector3[] originalVertices;
    Vector3[] transformedVertices;

    enum STATE
    {
        UP, 
        DOWN, 
        LEFT, 
        RIGHT,
        NONE
    }
    STATE state;

    public Mat4X4 TranslateMesh()
    {
        return new Mat4X4(
            new Vec4(1, 0, 0, 0),
            new Vec4(0, 1, 0, 0),
            new Vec4(0, 0, 1, 0),
            new Vec4(position.x - transform.position.x, position.y - transform.position.y, position.z - transform.position.z, 1)
            );
    }
    

    public float DegToRad(float deg)
    {
        return (Mathf.PI / 180f) * deg;
    }
    
    public Mat4X4 RotateMesh(Vec3 euler_deg)
    {
        Vec3 euler = new Vec3(DegToRad(euler_deg.x), DegToRad(euler_deg.y), DegToRad(euler_deg.z));

        Mat4X4 rollmat = new Mat4X4(
            new Vec4(Mathf.Cos(euler.z), Mathf.Sin(euler.z), 0, 0),
            new Vec4(-Mathf.Sin(euler.z), Mathf.Cos(euler.z), 0, 0),
            new Vec4(0, 0, 1, 0),
            Vec4.empty
        );

        Mat4X4 pitchmat = new Mat4X4(
            new Vec4(1, 0, 0, 0),
            new Vec4(0, Mathf.Cos(euler.y), Mathf.Sin(euler.y), 0),
            new Vec4(0, -Mathf.Sin(euler.y), Mathf.Cos(euler.y), 0),
            Vec4.empty
        );

        Mat4X4 yawmat = new Mat4X4(
            new Vec4(Mathf.Cos(euler.x), 0, -Mathf.Sin(euler.x), 0),
            new Vec4(0, 1, 0, 0),
            new Vec4(Mathf.Sin(euler.x), 0, Mathf.Cos(euler.x), 0),
            Vec4.empty
        );

        return yawmat * (pitchmat * rollmat);
    }

    public Mat4X4 ScaleMesh()
    {
        return new Mat4X4(
            new Vec3(scale.x, 0, 0),
            new Vec3(0, scale.y, 0),
            new Vec3(0, 0, scale.z),
            Vec3.empty
        );
    }



    void SquareLoop()
    {

        if (LoopVec.x <= 0 && LoopVec.y >= 30)
        { // if in top left
            state = STATE.DOWN;

        }
        else if (LoopVec.x >= 30 && LoopVec.y >= 30)
        { // if in top right
            state = STATE.LEFT;

        }
        else if (LoopVec.x >= 30 && LoopVec.y <= 0)
        { // if in bottom right
            state = STATE.UP;

        }
        else if (LoopVec.x <= 0 && LoopVec.y <= 0)
        { //if in starting corner bottom left
            state = STATE.RIGHT;
            
        }
       

        switch (state)
        {
            case STATE.LEFT:
                position.z -= 5f * Time.deltaTime;
                LoopVec.x -= 30f * Time.deltaTime;
            break;
            case STATE.DOWN:
                position.y -= 5f * Time.deltaTime;
                LoopVec.y -= 30f * Time.deltaTime;
            break;
            case STATE.RIGHT:
                position.z += 5f * Time.deltaTime;
                LoopVec.x += 30f * Time.deltaTime;
            break;
            case STATE.UP:
                position.y += 5f * Time.deltaTime;
                LoopVec.y += 30f * Time.deltaTime;
            break;
            default:
                break;
        }
        
    }


    void AutoSet()
    {
        rotation.x += Time.deltaTime * 80f;
        rotation.y += Time.deltaTime * 80f;
        rotation.z += Time.deltaTime * 80f;

        if (scale.x <= 0.5f)
        {
            flip = false;
        }
        else if (scale.x >= 2f)
        {
            flip = true;
        }


        if (flip)
        {
            scale.x -= Time.deltaTime * 2f;
            scale.y -= Time.deltaTime * 2f;
            scale.z -= Time.deltaTime * 2f;
        }
        else if (!flip)
        {
            scale.x += Time.deltaTime * 2f;
            scale.y += Time.deltaTime * 2f;
            scale.z += Time.deltaTime * 2f;
        }
    }

    void Movement()
    {
        int axis_x = 0;
        int axis_y = 0;
        if (Input.GetKey(KeyCode.K))
        {
            axis_y = -1;
        }
        else if (Input.GetKey(KeyCode.I))
        {
            axis_y = 1;
        }
        else
        {
            axis_y = 0;
        }

        if (Input.GetKey(KeyCode.J))
        {
            axis_x = 1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            axis_x = -1;
        }
        else
        {
            axis_x = 0;
        }

        position.z += axis_x * Time.deltaTime;
        position.y += axis_y * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        originalVertices = mesh.vertices;
        transformedVertices = new Vector3[originalVertices.Length];
        position = transform.position;
        scale = new Vec3(1, 1, 1).ToUnity();
        LoopVec = new Vec3(0, 0, 0);
   }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
        AutoSet();
        //Movement();
        SquareLoop();
        Mat4X4 scalemesh = ScaleMesh();
        Mat4X4 rotatemesh = RotateMesh(Mathlib.ToMathlib(rotation));
        Mat4X4 translatemesh = TranslateMesh();

        Mat4X4 trs = translatemesh * rotatemesh * scalemesh;
        for (int i = 0; i < originalVertices.Length; i++)
        {
            transformedVertices[i] = trs.ToUnity().MultiplyPoint3x4(originalVertices[i]);
        }

        mesh.vertices = transformedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
