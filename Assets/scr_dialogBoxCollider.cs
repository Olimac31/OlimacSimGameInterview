using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dialogBoxCollider : MonoBehaviour
{
    bool intrigger = false;
    GameObject myPlayer;

    void Start()
    {
        myPlayer = GameObject.Find("objPlayer");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            intrigger = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            intrigger = false;
        }

    }
    void Update()
    {
        //Get the Dialog Box script to access its functions
        textboxSystem myDialogBox = GetComponent<textboxSystem>();

        if(intrigger && !myPlayer.GetComponent<playerMovement>().reading && Input.GetButtonDown("Fire1") && !myDialogBox.opened && myDialogBox.cooldown <= 0) //If the player isn't interacting with anything and dialogBox is available
        {
            myDialogBox.startText();
            Debug.Log("Se inicia el dialogo");
        }
    }
}
