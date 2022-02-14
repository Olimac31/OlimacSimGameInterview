using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;
    public Vector2 offset = new Vector2(0.25f, -0.25f);

    public float camLimitUp, camLimitDown, camLimitLeft, camLimitRight;

    bool cameraWaiting = true;
    int cameraWaitingTime = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Fix odd bug where objects load weirdly for one frame
        if(cameraWaiting)
        {
            turnOnCamera();
        }

        if(target.transform.position.x < camLimitRight && target.transform.position.x > camLimitLeft)
        {
            transform.position = new Vector3(target.transform.position.x + offset.x, transform.position.y, transform.position.z);
        }

        if(target.transform.position.y < camLimitUp && target.transform.position.y > camLimitDown)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + offset.y, transform.position.z);
        }
        //transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
    }

    void turnOnCamera()
    {
        //Camera starts disabled to allow for objects to load first
        if(cameraWaitingTime > 0)
        {
            cameraWaitingTime--;
        }
        else
        {
            GetComponent<Camera>().enabled = true;
            cameraWaiting = false;
        }
    }
}
