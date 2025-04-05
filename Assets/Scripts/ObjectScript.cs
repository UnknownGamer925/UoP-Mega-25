using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    float mouse_x;
    float mouse_y;
    
    Quat CalulateYaw(float mx)
    {
        Quat YawQuat = new Quat(mx, new Vec3(0, 1, 0));

        Quat pos = new Quat(0, Mathlib.ToMathlib(transform.position));

        return YawQuat * pos * YawQuat.Inverse();
    }
    Quat CalulatePitch(float my) 
    {
        Quat PitchQuat = new Quat(my, new Vec3(0, 0, 1));

        Quat pos = new Quat(0, Mathlib.ToMathlib(transform.position));

        return PitchQuat * pos * PitchQuat.Inverse();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse_x += Input.GetAxisRaw("Mouse X") * 0.5f;
        mouse_y += Input.GetAxisRaw("Mouse Y") * 0.5f;

        Quat Rot_x = CalulateYaw(mouse_x);
        Quat Rot_y = CalulatePitch(mouse_y);

        Mat4X4 xRotMat = Rot_x.ToMat4X4();
        Mat4X4 yRotMat = Rot_y.ToMat4X4();

        Mat4X4 RotMat = xRotMat * yRotMat;

        transform.rotation = RotMat.ToQuat().ToUnity();
        
    }
}
