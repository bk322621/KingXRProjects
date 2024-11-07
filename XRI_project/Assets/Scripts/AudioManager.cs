using DefaultCompany.Singleton; //make sure the namespace matches company name
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music Tracks")]

    [SerializeField]
    private AudioClip[] tracks; //select from list and eventually randomized
    private AudioSource audioSource;

    [Header("Events")]

    public Action onCurrentTrackEnded;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenStopsPlaying());
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing)
    {
        if(tracks.Length > 0)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);

            audioSource.clip = tracks[UnityEngine.Random.Range(0,tracks.Length - 1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenStopsPlaying() //play another track when one completes
    {
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnded?.Invoke(); // invokes an action to anything listening
        }
    }


}
