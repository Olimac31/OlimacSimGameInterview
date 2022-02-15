using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    int menuPhase = 0;
    /*
    0 = Main menu
    1 = Skin Menu, Item Menu, etc
    */
    int menuChoice = 0; //0 = Skin Menu, 1 = Item Menu

    int choiceCooldownMAX = 5;
    int choiceCooldown = 0;
    Vector3 scaleZero = new Vector3(1, 0, 1);
    Vector3 scaleNormal = new Vector3(1, 1, 1);

    public List<Text> bodypartsTextSkin; //Get the text objects
    public GameObject bodypartsTextItems;

    public AudioClip selectionSound, acceptSound, errorSound;

    public GameObject selectionBar, mainMenuBar, itemMenuBar;
    public GameObject skinSection, itemSection;
    Vector3 selectionbarDistance, menubarDistance, itembarDistance;

    bool selfdestructed = false;

    int selectionX = 0;
    int selectionY = 0;

    public List<int> chosenSkinsBackup;

    // Start is called before the first frame update
    void Start()
    {
        //MAIN MENU
        skinSection.transform.localScale = scaleZero;
        itemSection.transform.localScale = scaleZero;
        
        //SKIN MENU
        selectionX = GameManager.instance.chosenSkins[selectionY]; //Set skin index accordingly
        chosenSkinsBackup = new List<int>(GameManager.instance.chosenSkins); //Backup chosen skins creating a copy of the current ones
        updateSkinTexts(); //Update the texts
        
        Vector3 temp = new Vector3(0, (16f/GameManager.instance.GlobalPixelsPerUnit), 0); //Fix weird pixels per unit glitch
        Vector3 temp2 = new Vector3(0, (13f/GameManager.instance.GlobalPixelsPerUnit), 0); //Fix weird pixels per unit glitch
        selectionbarDistance = temp; //Set the moving distance of the gui bar with the fix
        menubarDistance = temp;
        itembarDistance = temp2;

        SoundManager.instance.PlaySound(acceptSound);
    }

    // Update is called once per frame
    void Update()
    {
        //CHOICE COOLDOWN
        if(choiceCooldown > 0)
        {
            choiceCooldown--;
        }
        if(!selfdestructed)
        {
            //MAIN PHASE----------------------------------------
            if(menuPhase == 0 && choiceCooldown <= 0)
            {
                //SELECTION MENU PHASE 0
                if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(Input.GetKeyDown(KeyCode.UpArrow) && menuChoice > 0)
                    {
                        menuChoice--;
                        SoundManager.instance.PlaySound(selectionSound);
                        mainMenuBar.transform.position += menubarDistance;
                        Debug.Log("moved");
                    }
                    
                    if(Input.GetKeyDown(KeyCode.DownArrow) && menuChoice < 3)
                    {
                        menuChoice++;
                        SoundManager.instance.PlaySound(selectionSound);
                        mainMenuBar.transform.position -= menubarDistance;
                        Debug.Log("moved");
                    }
                }
                //ACCEPT AND PROCCEED TO PHASE 1
                if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
                {
                    SoundManager.instance.PlaySound(acceptSound);
                    menuPhase = 1;
                    choiceCooldown = choiceCooldownMAX; //IMPORTANT ON EVERY CHOICE
                }
                //Cancel
                if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Cancel"))
                {
                    SoundManager.instance.PlaySound(errorSound);
                    GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement(); //TODO: Take to another script
                    GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront(); //TODO: Take to another script

                    menuPhase = -1;
                    Destroy(gameObject, 0.15f);
                }
            }
            
            //Closing animation (for any section)
            if(menuPhase == -1)
            {
                closingAnimation();
            }
            //PLACEHOLDER ERROR INDEX
            if(menuPhase == 1 && menuChoice > 1)
            {
                menuPhase = -1;
                Destroy(gameObject, 0.15f);
                GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement();
            }
            //SKIN PHASE----------------------------------------
            if(menuPhase == 1 && menuChoice == 0 && choiceCooldown <= 0)
            {
                skinPhase();
            }
            else if(menuPhase == 1 && menuChoice == 1 && choiceCooldown <= 0)
            {
                //Item Menu code
                updateItemTexts();

                //GO BACK TO MAIN MENU
                //Accept
                if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
                {
                    SoundManager.instance.PlaySound(acceptSound);

                    menuPhase = 0;
                    menuChoice = 1;
                    choiceCooldown = choiceCooldownMAX; //IMPORTANT ON EVERY CHOICE
                }
                //Cancel
                else if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Cancel"))
                {
                    SoundManager.instance.PlaySound(errorSound);
                    GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement();
                    GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront(); //TODO: Take to another script

                    menuPhase = -1;
                    Destroy(gameObject, 0.15f);
                }
            }
            checkSectionScale();
        }
    }
    void skinPhase()
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
            
            if(Input.GetKeyDown(KeyCode.DownArrow) && selectionY < bodypartsTextSkin.Count - 1)
            {
                selectionY++;
                SoundManager.instance.PlaySound(selectionSound);
                selectionBar.transform.position -= selectionbarDistance;
                Debug.Log("moved");
            }
            selectionX = GameManager.instance.chosenSkins[selectionY];
        }
        updateSkinTexts();
        //GO BACK TO MAIN MENU
        //Accept
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
        {
            SoundManager.instance.PlaySound(acceptSound);

            GameManager.instance.ApplyCharacterSkin(); //Apply changes
            GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront(); //TODO: Take to another script
            menuPhase = 0;
            menuChoice = 0;
            choiceCooldown = choiceCooldownMAX; //IMPORTANT ON EVERY CHOICE
        }
        //Cancel
        else if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Cancel"))
        {
            SoundManager.instance.PlaySound(errorSound);
            GameObject.Find("objPlayer").GetComponent<playerMovement>().resumeMovement(); //TODO: Take to another script
            GameObject.Find("objPlayer").GetComponent<playerMovement>().ForceFaceFront(); //TODO: Take to another script

            menuPhase = -1;
            GameManager.instance.chosenSkins = chosenSkinsBackup; //Cancel all changes
            GameManager.instance.ApplyCharacterSkin();
            Destroy(gameObject, 0.15f);
        }
    }
    void updateSkinTexts()
    {
        for(int i = 0; i < 3; i++)
        {
            var theGameManager = GameManager.instance;
            switch(i)
            {
                case 0:
                bodypartsTextSkin[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().headNames[theGameManager.chosenSkins[i]];
                break;

                case 1:
                bodypartsTextSkin[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().bodyNames[theGameManager.chosenSkins[i]];
                break;

                case 2:
                bodypartsTextSkin[i].text = theGameManager.GetComponentInChildren<GlobalSkinList>().legNames[theGameManager.chosenSkins[i]];
                break;
            }
            //This will change the text according to what skins are chosen in the game manager.
            //The names are defined in the Global Skin List
        }
    }
    void updateItemTexts()
    {
        string temp = "";
        for(int i = 0; i < GameManager.instance.playerItems.Count - 1; i++)
        {
            int tempItem = GameManager.instance.playerItems[i];

            temp += GameManager.instance.GetComponentInChildren<GlobalItemList>().globalItems[tempItem] + "\n"; //Obtain Item Name by ID stored in Player's Inventory
        }
        bodypartsTextItems.GetComponent<Text>().text = temp;
    }
    void closingAnimation()
    {
        Vector3 temp = new Vector3(0, -10f * Time.deltaTime, 0);
        if(transform.localScale.y > 0)
        {
            transform.localScale += temp;
        }
    }
    void checkSectionScale()
    {
        switch(menuPhase)
        {
            //MAIN MENU
            case 0:
            skinSection.transform.localScale = scaleZero;
            itemSection.transform.localScale = scaleZero;
            break;

            //ITEM, INVENTORY
            case 1:
            if(menuChoice == 0)
            {
                skinSection.transform.localScale = scaleNormal;
            }
            else if(menuChoice == 1)
            {
                itemSection.transform.localScale = scaleNormal;
            }
            break;
        }
    }
}
