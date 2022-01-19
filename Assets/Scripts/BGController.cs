using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    public PlayerMovement pm;

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
   // Update is called once per frame
    void Update()
    {
        if (pm.gameWon == true)
        {
           audioSource.Stop();
        }

        if(pm.gameLost == true)
        {
            audioSource.Stop();
        }
    }
}
