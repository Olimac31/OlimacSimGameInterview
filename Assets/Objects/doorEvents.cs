using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorEvents : MonoBehaviour
{
    GameObject myPlayer, theScreenFade;

    public string sceneToLoad;
    bool transitionStart = false;
    float screenfadeAmount = 0f;

    void Start()
    {
        myPlayer = GameObject.Find("objPlayer");
        theScreenFade = GameObject.Find("NightFilter");
        GetComponent<Renderer>().enabled = false; //Make sprite invisible
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            transitionStart = true;
            myPlayer.GetComponent<playerMovement>().stopMovement();
        }
    }

    void Update()
    {
        //screenfadeAmount fix
        if(screenfadeAmount < 0f) screenfadeAmount = 0f;
        if(screenfadeAmount > 1f) screenfadeAmount = 1f;

        //Transition Fade
        if(transitionStart && screenfadeAmount < 1f)
        {
            var temp = theScreenFade.GetComponent<NightFilterScript>().myScreenFade.GetComponent<SpriteRenderer>();

            screenfadeAmount += Time.deltaTime * 2f;
            Color tmpcol = temp.color;
            tmpcol.a = screenfadeAmount;

            theScreenFade.GetComponent<NightFilterScript>().myScreenFade.GetComponent<SpriteRenderer>().color = tmpcol;

        }
        else if(transitionStart && screenfadeAmount >= 1f)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }

    }
}
