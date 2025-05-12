using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Vector3 euler = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    Vec3 CalculateEuler(Vec3 dir)
    {
        Vec3 eul = Vec3.empty;

        eul.x = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        eul.y = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        eul.z = 0;

        return eul;
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = 0;
        float mouse_y = 0;
        //Get Mouse Position
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.G))
        {
            mouse_x = Input.GetAxis("Mouse X");
            mouse_y = Input.GetAxis("Mouse Y");
        }

        Vec3 direction = new Vec3(mouse_y, mouse_x, 1f);

        //updates position and rotation based on player & mouse position respetively
        euler += CalculateEuler(direction.Normalized()).ToUnity() * Time.deltaTime * 20f;
        this.transform.eulerAngles = euler;
    }
}
