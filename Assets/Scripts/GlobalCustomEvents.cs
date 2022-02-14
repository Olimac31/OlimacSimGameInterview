using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCustomEvents : MonoBehaviour
{
    public static GlobalCustomEvents instance;

    //SINGLETON CODE
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
    /*
    This object is meant to handle custom events that are global
    to the entire game, such as opening the shop, being arbitrarily teleported to a place,
    changing rooms between dialogue, etc, but mostly important events

    Every Custom Event has a defined ID, which can be triggered through the dialogue system
    using the executeCustomEvent() function
    
    However, this object can also be accessed by other GameObjects and its events can be
    arbitrality executed, without the need of using the ID
    (This would be helpful if I were to make a Cutscene System, for example)

    Read the comments in textboxSystem.cs to know how these functions can be triggered
    in the middle of dialogue

    */

    //EVENT EXECUTION:
    public void executeCustomEvent(string eventID)
    {
        switch(eventID)
        {
            case "00":
            testGlobalEvent();
            break;

            case "01":
            toggleNightTime();
            break;
        }

    }

    //EVENT ID 00:
    public void testGlobalEvent()
    {
        var customClip = Resources.Load<AudioClip>("SFX/testjingle");
        SoundManager.instance.PlaySound(customClip);
    }

    //EVENT ID 01:
    public void toggleNightTime()
    {
        //Toggle the Night Time global variable
        GameManager.instance.nightFilter = !GameManager.instance.nightFilter;
        GameObject.Find("NightFilter").GetComponent<NightFilterScript>().NightFilterSet();

        //Play a small sound
        var customClip = Resources.Load<AudioClip>("SFX/startEffect");
        SoundManager.instance.PlaySound(customClip);
    }

    //EVENT ID 02:
}
