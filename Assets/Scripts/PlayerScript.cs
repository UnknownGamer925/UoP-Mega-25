using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public Camera cam;
    [SerializeField] public float ROTATE_SPEED;
    [SerializeField] public float MOVE_SPEED;
    
    
    void Movement()
    {
        //Local variables
        Vector3 relative_forward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 relative_right = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);
        Quaternion RotateTo = Quaternion.identity;

        // Rotate & Move player along x-axis relative to camera-facing direction
        if (Input.GetAxis("Horizontal") != 0)
        {

            //Move
            transform.position += Input.GetAxis("Horizontal") * new Vector3(cam.transform.right.x, 0f, cam.transform.right.z) * MOVE_SPEED * Time.deltaTime;
        }

        // Rotate & Move player along z-axis relative to camera-facing direction
        if (Input.GetAxis("Vertical") != 0)
        {

            //Move
            transform.position += Input.GetAxis("Vertical") * new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z) * MOVE_SPEED * Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
    
    
    