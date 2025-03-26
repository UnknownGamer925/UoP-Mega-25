using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuatTest : MonoBehaviour
{
    [SerializeField] float angle = 0;
    [SerializeField] float t = 0;

    void TestOne()
    {
        angle += Time.deltaTime;
        
        //Define Quat that is the equiveland of rotating the Yaw axis in Euler
        Quat q = new Quat(angle, new Vec3(0, 1, 0));

        //Define vector to rotate
        Vec3 p = new Vec3(1, 2, 3);

        //Store p in a Quat
        Quat K = new Quat(0, p);

        //newK will have our position inside of it
        Quat newK = q * K * q.Inverse();

        //get position as Vec3
        Vec3 newP = newK.axis;

        //Set the position to see it working
        transform.rotation = newK.ToUnity();
    }
    
    void TestTwo()
    {
        t += Time.deltaTime * 0.5f;
        
        //Defining 2 rotations
        Quat q = new Quat(Mathf.PI * 0.5f, new Vec3(0, 1, 0));
        Quat r = new Quat(Mathf.PI * 0.25f, new Vec3(1, 0, 0));

        //This is the slerped value
        Quat slerped = Quat.Slerp(q, r, t);

        //Define vector to rotate
        Vec3 p = new Vec3(1, 2, 3);

        //Store p in a Quat
        Quat K = new Quat(0, p);

        //newK will have our new position inside of it
        Quat newK = slerped * K * slerped.Inverse();

        //get position as Vec3
        Vec3 newP = newK.axis;

        //Set the position to see it working
        transform.rotation = newK.ToUnity();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TestOne();
        TestTwo();
    }
}
