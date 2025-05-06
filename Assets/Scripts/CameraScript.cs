using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = 0;
        float mouse_y = 0;
        //Get Mouse Position
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.G))
        {
            mouse_x = Input.GetAxis("Mouse X") * 5;
            mouse_y = Input.GetAxis("Mouse Y") * 5;
        }
            

        //updates position and rotation based on player & mouse position respetively
        Vector3 euler = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3((euler.x - mouse_y), (euler.y + mouse_x), 0f);
    }
}
