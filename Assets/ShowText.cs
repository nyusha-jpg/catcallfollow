using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    //This was inspired by the man who catcalled me while I was getting lunch near the game center. He followed me for about a block, and then walked away when he got bored. 
    //When he left me, I wondered where he had walked off to.
    //When I got back to the game center after lunch, there he was, sitting in the exact same spot as before!
    //For some reason this struck me as extremely funny, like he had returned to his 0, 0, 0 origin after executing a function.
    //I passed him again, and he stood up and made a comment at me again, and I thought he might try to walk with me again.
    //I imagined him as an NPC, trapped in an endless loop. Waiting at startPos, then moveTowards woman who gets close, timer/interest expires, return to startPos.

    public GameObject text; //speech bubble and text
    public bool contact; //has the mouse touched the character

    public AudioSource boomBox;
    public AudioClip sound;

    public float timer; //for separating yell sounds
    public float followTimer; //how long they follow for

    Vector3 firstPosition; //starting position

    public bool imYelling; //sound is happening, or not
    public float maxMoveSpeed = 10;
    public float smoothTime = 0.3f;

    public float followMaxMoveSpeed = 10;
    public float followSmoothTime = 0.3f;
    Vector2 currentVelocity;

    

    void Start() //in the beginning!
    {
        contact = false; //character has not been touched
        imYelling = false; //is silent
        timer = 1f;
        followTimer = 6f;
        firstPosition = this.transform.position; //saves starting position
    }
    void Update(){

       if ((contact == true) && (followTimer > 0)){ //if they've been touched and the follow timer is running
           Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //find the mouse position
        transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxMoveSpeed); //move the character to the mouse
                            }

        if (imYelling == true){ //if they're yelling
            timer = timer - Time.deltaTime; //count down the timer... i made this because 1000 of one character's yells were playing simultaneously and it was madness
        }

        if (timer < 0){ //when it gets below zero
            imYelling = false; //you're not making a sound anymore
            timer = 1f; //reset it to one second
        }

        if ((contact == true) && (followTimer > 0)){ //if you've been touched and the follow timer is above zero
            followTimer = followTimer - Time.deltaTime; //count down the follow timer
        }

         if (followTimer < 0 ){ //if the follow timer goes into the negatives
            contact = false; //it's like we never even met :'''( ...also effectively stop running the mouse follow code
            transform.position = Vector2.SmoothDamp(transform.position, firstPosition, ref currentVelocity, followSmoothTime, followMaxMoveSpeed); //move back to inital position

              if (transform.position == firstPosition) //if you made it back to your starting position
              {
                  followTimer = 6f; //reset follow timer so that following can re-happen
              }
        }

        Debug.Log(followTimer);
    
    }

    void OnMouseOver(){ //when mouse touches 
        text.SetActive(true); //show text and speech bubble
        contact = true; //we have made contact!!!!!

        if (imYelling == false){ //if sound is not currently playing
            PlaySound(); //run play sound
        }

    }

    void PlaySound()
    {
        imYelling = true; //you are making a sound
        boomBox.PlayOneShot(sound); //make the sound

    }

    void OnMouseExit(){ //when mouse stops touching
        text.SetActive(false); //stop showing the speech bubble
    }
}
