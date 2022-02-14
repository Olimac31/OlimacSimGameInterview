using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTest : MonoBehaviour
{
    private void onTriggerEnter2D(Collider2D other)
    {
      Debug.Log("trigger");
    }
}
