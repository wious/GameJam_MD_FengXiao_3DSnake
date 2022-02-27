using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    // Start is called before the first frame update
    public AudioClip pickUp_Sound, dead_Sound;

    private void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void Player_PickUpSound()
    {
        AudioSource.PlayClipAtPoint(pickUp_Sound,transform.position);
    }
    public void Player_DeadSound()
    {
        AudioSource.PlayClipAtPoint(dead_Sound,transform.position);
    }

}
