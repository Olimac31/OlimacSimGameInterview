using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class makeInvisibleTileset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            Color tmp = GetComponent<Tilemap>().color;
            tmp.a = 0f;
            GetComponent<Tilemap>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
