using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightFilterScript : MonoBehaviour
{
    Camera target;

    public GameObject myNightFade, myScreenFade;
    public bool forceDay = false;
    public float screenfadeAmount = 1f;
    bool initialFadeDone = false;

    Color transparentColor;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("retroCamera").GetComponent<Camera>();
        GetComponentInChildren<Canvas>().worldCamera = target;

        //Transparency, hide filters, etc
        //Screen Fade on Scene start
        transparentColor = myScreenFade.GetComponent<SpriteRenderer>().color;
        transparentColor.a = screenfadeAmount;
        myScreenFade.GetComponent<SpriteRenderer>().color = transparentColor;
    }

    void Start()
    {
        //Transparency, hide filters, etc
        //Night Filter
        if(!forceDay)
        {
            NightFilterSet();
        }
        else
        {
            NightFilterForceDay();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //screenfadeAmount fix
        if(screenfadeAmount < 0f) screenfadeAmount = 0f;
        if(screenfadeAmount > 1f) screenfadeAmount = 1f;

        //Scene start screen fade
        if(screenfadeAmount > 0f && !initialFadeDone)
        {
            //Transparency fade code
            screenfadeAmount -= Time.deltaTime * 2f;
            Color tmp = myScreenFade.GetComponent<SpriteRenderer>().color;
            tmp.a = screenfadeAmount;

            myScreenFade.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if(screenfadeAmount <= 0f && !initialFadeDone)
        {
            initialFadeDone = true;
        }
        //FINISH FADE----------------------------
        
        if(target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
    }

    public void NightFilterSet()
    {
        Debug.Log(GameManager.instance.nightFilter);
        if(GameManager.instance.nightFilter)
        {
            Color tmp = myNightFade.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.4f;
            myNightFade.GetComponent<SpriteRenderer>().color = tmp;
        }
        else
        {
            Color tmp = myNightFade.GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            myNightFade.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    public void NightFilterForceDay()
    {
        //Force the night filter to be set to day for this specific scene
        Color tmp = myNightFade.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        myNightFade.GetComponent<SpriteRenderer>().color = tmp;
    }
}
