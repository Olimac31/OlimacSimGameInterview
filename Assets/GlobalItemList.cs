using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalItemList : MonoBehaviour
{
    public List<string> globalItems; //GLOBAL ITEMS LIST
    public List<string> globalItemType; //GLOBAL ITEM TYPE LIST

    public AudioClip errorSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void useItem(string itemName)
    {
        switch(itemName)
        {
            case "Energy Drink":
            Debug.Log("Energy Drink used");
            break;

            case "Health Potion":
            Debug.Log("Used Health Potion");
            break;

            case "placeholderitem":
            Debug.Log("test item consumed");
            break;

            case "_":
            Debug.Log("Item no implementado / Item no valido");
            SoundManager.instance.PlaySound(errorSound);
            break;
        }
    }
}
