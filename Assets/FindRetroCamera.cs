using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRetroCamera : MonoBehaviour
{
    Camera target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("retroCamera").GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = target;
    }
}
