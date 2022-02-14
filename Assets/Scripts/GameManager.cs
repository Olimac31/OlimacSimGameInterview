using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int GlobalPixelsPerUnit = 16; //IMPORTANT
    public static GameManager instance;

    public int globalGold = 0;
    public float playerEnergy = 100;
    public float playerStress = 0;

    public bool nightFilter = false; //Used for night time

 
    public List<int> chosenSkins = new List<int>(); //The skin numbers chosen by the player
    public GameObject skinMenuPrefab;

    //Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenSkinMenu()
    {
        GameObject clone;
        clone = Instantiate(skinMenuPrefab) as GameObject;

        GameObject.Find("objPlayer").GetComponent<playerMovement>().stopMovement(); //Disable player control
    }
    //Set Character's Skin according to what he has equipped
    public void ApplyCharacterSkin()
    {
        var thePlayer = GameObject.Find("objPlayer").GetComponent<playerMovement>();
        for(int i = 0; i < 3; i++)
        {
            //thePlayer.anim[i] = GetComponent<GlobalSkinList>().animList[i]
            switch(i)
            {
                case 0:
                thePlayer.anim[i].runtimeAnimatorController = GetComponentInChildren<GlobalSkinList>().headSkins[chosenSkins[i]];
                break;
                
                case 1:
                thePlayer.anim[i].runtimeAnimatorController = GetComponentInChildren<GlobalSkinList>().bodySkins[chosenSkins[i]];
                break;

                case 2:
                thePlayer.anim[i].runtimeAnimatorController = GetComponentInChildren<GlobalSkinList>().legSkins[chosenSkins[i]];
                break;
            }
        }
    }

}
