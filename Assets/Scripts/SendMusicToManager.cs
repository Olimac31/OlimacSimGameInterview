using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMusicToManager : MonoBehaviour
{
    public AudioClip myMusic;
    // Start is called before the first frame update

    void Start()
    {
        SoundManager.instance.PlayMusic(myMusic); //We reference the instance because we can't make the static object play it
    }
}
