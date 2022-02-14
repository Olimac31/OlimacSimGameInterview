using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WARNING: This script is obsolete, I found a much better way to handle interactions
public class playerInteractions : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        //Get the player movement script to access its functions
        playerMovement myPlayer = GetComponent<playerMovement>();

        if(other.gameObject.CompareTag("dialogBox") && !myPlayer.reading) //If the player isn't interacting with anything
        {
            //Get access to the entire dialogBox object, not just the transform, then show text
            GameObject obj = this.transform.parent.gameObject;
            obj.GetComponent<textboxSystem>().hideText();

            myPlayer.stopMovement();
        }
    }
}
