using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeatures : CoreFeatures
{
    [Header("Drawer Configuration")]

    [SerializeField]
    private Transform drawerSlide;
    
    [SerializeField]
    private float maxDistance = 1.0f; //under

    [SerializeField]
    private FeatureDirection featureDirection = FeatureDirection.Forward;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private float drawerSpeed = 1.0f;  //over

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    void Start()
    {
        //drawer with simple interactable
        simpleInteractable?.selectEntered.AddListener((s) => 
        {
            //if the drawer is not open, open it
            if(!open)
            {
                OpenDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        PlayOnStart();
        open = true;
        StartCoroutine(ProcessMotion());
    }

    private IEnumerator ProcessMotion()
    {
        while(open)
        {
            if(featureDirection == FeatureDirection.Forward && drawerSlide.localPosition.z <= maxDistance)
            {
                drawerSlide.Translate(Vector3.forward * Time.deltaTime * drawerSpeed);
            }
            else if(featureDirection == FeatureDirection.Backward && drawerSlide.localPosition.z >= maxDistance)
            {
                drawerSlide.Translate(-Vector3.forward * Time.deltaTime * drawerSpeed);
            }
            else
            {
                open = false; //ending the loop if no condition is met
            }

            yield return null; 
        }
    }
}