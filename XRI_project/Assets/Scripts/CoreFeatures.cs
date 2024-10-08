using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFeatures : MonoBehaviour
{
    //Properties work like variables, they are declared the same way but formatted differemt. Properties comman way to access code outside of this script/share this code to another script
    //You can create a public variable access them in  another script, or you can properties
    //Properties are encapsulated and formated as fields
    //properties have two ACCESSORS. GET and SET
    //Get Accessor (READ - ONLY) returns other encapsulated variables
    //Set Accessor (WRITE) allocating values to a property
    //Properties are Pascal casing only 
    //4 pillars of object oriented programming inheritance, emcapsulation, polymorphism, abstraction

    public bool AudioSFXSourceCreated { get; set; }

    //audio plays when door opens
    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    //Audio plays on close
    [field: SerializeField]
    public AudioClip AudioClipOnEnd {  get; set; }

    private AudioSource audioSource;

    public FeatureUseage featureUseage = FeatureUseage.Once;

    protected virtual void Awake()
    {
        MakeSFXAudioSource();
    }
    private void MakeSFXAudioSource()
    {
        //if component doesnt exist, make one

        audioSource = GetComponent<AudioSource>();

        //if this is = to null, create it here

        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  
        }

        AudioSFXSourceCreated = true;
    }

    protected void PlayOnStart()
    {
        if(AudioSFXSourceCreated  && AudioClipOnStart != null)
        {
            audioSource.clip = AudioClipOnStart;
            audioSource.Play();
        }
    }

    protected void PlayOnEnd()
    {
        if (AudioSFXSourceCreated && AudioClipOnEnd != null)
        {
            audioSource.clip = AudioClipOnEnd;
            audioSource.Play();
        }
    }

}
