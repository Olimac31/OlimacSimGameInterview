using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCustomBeeps : MonoBehaviour
{
    int beepCooldown = 0;
    int beepCooldownMAX = 4;
    public List<AudioClip> mySFX = new List<AudioClip>();
    // NOTE: THIS SCRIPT IS MEANT TO BE USED FOR CUSTOM TYPING BEEPS ON DIALOGUE

    /*
    0 = Default Beep
    1 = Low Beep
    2 = Olimac Beep
    */

    public void playBeep(int characterSlot)
    {
        SoundManager.instance.PlaySound(mySFX[characterSlot]); //We reference the instance because we can't make the static object play it
        beepCooldown = beepCooldownMAX;
    }

    public bool BeepIsPlaying()
    {
        if(beepCooldown <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    void FixedUpdate()
    {
        if(beepCooldown > 0)
        {
            beepCooldown--;
        }
    }
}
