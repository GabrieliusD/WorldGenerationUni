using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 2.0f;
    public float cameraScrollSpeed = 100.0f;

    public float maxHeight = 50.0f;

    public float minHeight = 10.0f;
    void Update()
    {
        if(Input.GetMouseButton(2))
        {
            float moveX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSpeed;
            float moveY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSpeed;

            transform.position += new Vector3(moveX, 0 ,moveY);
        }

        float scroll = -Input.GetAxisRaw("Mouse ScrollWheel") * cameraScrollSpeed;
        transform.position += new Vector3(0,scroll,0);
        if(transform.position.y >= maxHeight)
            transform.position = new Vector3(transform.position.x,maxHeight,transform.position.z);
        //if(transform.position.y <= minHeight)
            //transform.position = new Vector3(transform.position.x,minHeight,transform.position.z);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down,out hit, 5.0f))
        {
            transform.position = new Vector3(transform.position.x,hit.point.y + minHeight,transform.position.z);
        }

        
    }
}
