using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 50;
    public float scrollSpeed = 1000;
    public float Min_X = -100;
    public float Max_X = 35;
    public float Min_Y = 30;
    public float Max_Y = 120;
    public float Min_Z = -50;
    public float Max_Z = 50;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A) )
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {
            transform.position +=Vector3.up* scroll * scrollSpeed * Time.deltaTime;
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, Min_X,Max_X);
        pos.y = Mathf.Clamp(pos.y, Min_Y, Max_Y);
        pos.z = Mathf.Clamp(pos.z, Min_Z, Max_Z);
        transform.position = pos;
    }
}
