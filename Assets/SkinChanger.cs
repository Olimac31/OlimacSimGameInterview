using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public List<Text> bodypartsText; //Get the text objects

    public AudioClip selectionSound, acceptSound, errorSound;

    public GameObject selectionBar;
    Vector3 selectionbarDistance;

    bool selfdestructed = false;

    int selectionX = 0;
    int selectionY = 0;

    public List<int> chosenSkinsBackup;

    // Start is called before the first frame update
    void Start()
    {
        selectionX = GameManager.instance.chosenSkins[selectionY]; //Set skin index accordingly
        chosenSkinsBackup = new List<int>(GameManager.instance.chosenSkins); //Backup chosen skins creating a copy of the current ones
        updateTexts(); //Update the texts
        
        Vector3 temp = new Vector3(0, (14f/GameManager.instance.GlobalPixelsPerUnit), 0); //Fix weird pixels per unit glitch
        selectionbarDistance = temp; //Set the moving distance of the gui bar with the fix

        SoundManager.instance.PlaySound(acceptSound);
    }

    // Update is called once per frame
    void Update()
    {
        if(!selfdestructed)
        {
            //SELECTION X
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {   
                //LEFT
                if(Input.GetKeyDown(KeyCode.LeftArrow) && selectionX > 0)
                {
                    selectionX--;
                    SoundManager.instance.PlaySound(selectionSound);
                }
                else if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SoundManager.instance.PlaySound(errorSound);
                }
                
                //RIGHT
                if(Input.GetKeyDown(KeyCode.RightArrow) && selectionX < GameManager.instance.GetComponentInChildren<GlobalSkinList>().totalSkins - 1)
                {
                    selectionX++;
                    SoundManager.instance.PlaySound(selectionSound);
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SoundManager.instance.PlaySound(errorSound);
                }
                GameManager.instance.chosenSkins[selectionY] = selectionX;
            }

            //SELECTION Y
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(Input.GetKeyDown(KeyCode.UpArrow) && selectionY > 0)
                {
                    selectionY--;
                    SoundManager.instance.PlaySound(selectionSound);
                    selectionBar.transform.position += selectionbarDistance;
                    Debug.Log("moved");
                }
                
                if(Input.GetKeyDown(KeyCode.DownArrow) && selectionY < bodypartsText.Count - 1)
                {
                    selectionY++;
                    SoundManager.instance.PlaySound(selectionSound);
                    selectionBar.transform.position -= selectionbarDistance;
                    Debug.Log("moved");
                }
                selectionX = GameManager.instance.chosenSkins[selectionY];
            }

            updateTexts();

            //EXIT
            //Accept
            if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
            {
                SoundManager.instance.PlaySound(acceptSound);
                GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement();
                GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront();

                selfdestructed = true;
                GameManager.instance.ApplyCharacterSkin(); //Apply changes
                Destroy(gameObject, 0.15f);
            }
            //Cancel
            else if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Cancel"))
            {
                SoundManager.instance.PlaySound(errorSound);
                GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement(); //TODO: Take to another script
                GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront(); //TODO: Take to another script

                selfdestructed = true;
                GameManager.instance.chosenSkins = chosenSkinsBackup; //Cancel all changes
                GameManager.instance.ApplyCharacterSkin();
                Destroy(gameObject, 0.15f);
            }
        }
        else
        {
            //Closing animation
            Vector3 temp = new Vector3(0, -10f * Time.deltaTime, 0);
            if(transform.localScale.y > 0)
            {
                transform.localScale += temp;
            }
        }
    }

    void updateTexts()
    {
        for(int i = 0; i < 3; i++)
        {
            var theGameManager = GameManager.instance;
            switch(i)
            {
                case 0:
                bodypartsText[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().headNames[theGameManager.chosenSkins[i]];
                break;

                case 1:
                bodypartsText[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().bodyNames[theGameManager.chosenSkins[i]];
                break;

                case 2:
                bodypartsText[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().legNames[theGameManager.chosenSkins[i]];
                break;
            }
            //This will change the text according to what skins are chosen in the game manager.
            //The names are defined in the Global Skin List
        }
    }
}
