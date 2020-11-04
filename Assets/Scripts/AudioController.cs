using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource backGroundSource;

    // Start is called before the first frame update
    void Start()
    {
        backGroundSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        if (!backGroundSource)
        {
            Debug.Log("Error! Could not find game background music");
        }
        else
        {
            backGroundSource.enabled = true;
            backGroundSource.ignoreListenerPause = true;    // this sound will not be affected by PauseAudio() / UnpauseAudio()
            backGroundSource.loop = true;
            backGroundSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameManager.State.MENU:
            {
                PauseAudio();
            } break;

            case GameManager.State.GAME:
            { 
                UnpauseAudio();
            } break;

            case GameManager.State.DEATH:
            {
                PauseAudio();
            } break;

            case GameManager.State.WIN:
            {
                PauseAudio();
            } break;

            case GameManager.State.PAUSE:
            {
                PauseAudio();
            } break;

            default:
            {
                Debug.Log("Error! Unknown game state!");

            } break;
        }
    }

    public static void PauseAudio()
    {
        AudioListener.pause = true;
    }

    public static void UnpauseAudio()
    {
        AudioListener.pause = false;
    }
}
