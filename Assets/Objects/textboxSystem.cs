using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textboxSystem : MonoBehaviour
{
    public List<string> message = new List<string>();
    public int message_current = 0;
    public int message_end = 0;
    int textStartSkip = 0; //Used for Special Functions
    Text target;
    public bool active = false;
    public bool opened = false;

    public float characters = 0.0f;
    float hold = 0.0f;
    GameObject myPlayer;
    SendCustomBeeps customBeeps;
    int customBeepSlot = 0;
    
    public int cooldown = 0; //Cooldown for re-interacting
    int cooldownMAX = 3;

    public int testlength;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInChildren<Text>(); //get the Text Writer
        myPlayer = GameObject.Find("objPlayer");
        customBeeps = GetComponentInChildren<SendCustomBeeps>(); //Custom beep sounds
        customBeepSlot = 0;
        //GetComponent<SpriteRenderer>().enabled = false; //To hide only the debug sprite
        hideText();
        opened = false;
        characters = 0;
        message_current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        testlength = message[message_current].Length;
        if(opened)
        {
            //SPECIAL TEXT FUNCTIONS AND COMMANDS---------------------
            SpecialTextFunctions();
            //-------------------------------------------

            
            int fullTextLength = message[message_current].Length - textStartSkip;
            //Typewriter Text + Message Handler-------------------------------
            if(characters < fullTextLength)
            {
                characters += 1 + hold;
                target.text = message[message_current].Substring(textStartSkip, Mathf.RoundToInt(characters)); //Typewrite text
                if(!customBeeps.BeepIsPlaying()) //Check to not overlap beeps
                {
                    customBeeps.playBeep(customBeepSlot); //Play the sound beep with the character that is currently speaking
                }
            }
            else if(characters >= fullTextLength)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    if(message_current < message_end) //If there's more messages
                    {
                        characters = 0;
                        target.text = "";
                        textStartSkip = 0; //Reset the start index
                        message_current++;
                    }
                    else //If there aren't any messages left, exit
                    {
                        endText();
                    }
                }
            }
        }

        if(cooldown > 0)
        {
            cooldown--;
        }

        if(Input.GetButton("Fire1"))
        {
            hold = 0.5f;
        }
        else
        {
            hold = 0.0f;
        }
    }

//Functions-----------------------------------------------
    public void hideText()
    {
        Color newColor;
        //Get the renderer from each child and hide it
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
        //Get the text and make it invisible
        newColor = GetComponentInChildren<Text>().color;
        newColor.a = 0.0f;
        GetComponentInChildren<Text>().color = newColor;
    }

    public void showText()
    {
        Color newColor;
        //Get the renderer from each child and hide it
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
        //Get the text and make it invisible
        newColor = GetComponentInChildren<Text>().color;
        newColor.a = 1.0f;
        GetComponentInChildren<Text>().color = newColor;

        //MAKE SURE THE DEBUG SPRITE IS NEVER VISIBLE
        GetComponent<Renderer>().enabled = false;
    }

    public void startText() //Initiate all the process
    {
        showText();
        opened = true;
        myPlayer.GetComponent<playerMovement>().stopMovement();
    }
    public void endText() //Reset all variables
    {
        hideText();
        opened = false;
        characters = 0;
        message_current = 0;
        cooldown = cooldownMAX;
        customBeepSlot = 0;
        textStartSkip = 0;
        myPlayer.GetComponent<playerMovement>().resumeMovement();
    }

    public void SpecialTextFunctions()
    {
        /*
        TEMPLATE:
        -sp XXX XX

        Functions:
        - "-sp chr XX" -> Allows you to change the Character Beep, XX being the slot (00, 01, 02, etc)
        - "-sp /yn 00" -> Asks the player a YES/NO question [WIP]
        - "-sp col 00" -> Changes the text color to a preset color (Custom Colors not available yet) [WIP]
        - "-sp scr XX" -> Reads a script attached to the GameObject [WIP]

        */

        //If a special function is invoked using -sp at the start of the dialogue...
        //Any amount of special functions can be invoked per message thanks to the textStartSkip variable
        if(message[message_current].Substring(0 + textStartSkip, 3).Contains("-sp"))
        {
            switch(message[message_current].Substring(4  + textStartSkip, 3))
            {
                // Change Character Beep
                // This would be "-sp chr XX", XX being the character slot (00, 01, 02, etc)
                case "chr":
                int characterToChangeTo = int.Parse(message[message_current].Substring(8  + textStartSkip, 2));
                customBeepSlot = characterToChangeTo;
                break;

                //Execute Custom Global Events by ID
                //Example: "-sp scr 00" will trigger the test event
                case "scr":
                GlobalCustomEvents.instance.executeCustomEvent(message[message_current].Substring(8 + textStartSkip, 2));
                break;
            }
            textStartSkip += 11;
        }

    }
}
