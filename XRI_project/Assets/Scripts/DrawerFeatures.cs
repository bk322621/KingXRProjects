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

    private Vector3 initialPosition; //Store the initial position of our drawer

    //Restrict drawer position
    private float drawerMinLimit;
    private float drawerMaxLimit;

    void Start()
    {
        //Save the initial position of the drawer at start;
        initialPosition = drawerSlide.localPosition;

        //Find the drawer min and max limits based off of initialPosition and maxDistance

        if(featureDirection == FeatureDirection.Forward)
        {
            drawerMinLimit = initialPosition.z;
            drawerMaxLimit = initialPosition.z + maxDistance;
        }
        else
        {
            drawerMinLimit = initialPosition.z - maxDistance;
            drawerMaxLimit = initialPosition.z;
        }

        //drawer with simple interactable
        simpleInteractable?.selectEntered.AddListener((s) => 
        {
            //if the drawer is not open, open it
            if(!open)
            {
                OpenDrawer();
            }
            else
            {
                CloseDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        PlayOnStart();
        open = true;
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());

    }

    private void CloseDrawer()
    {
        PlayOnEnd();
        open = false;
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }

    private IEnumerator ProcessMotion()
    {
        //Open drawer to max distance or close initial position based on "open" bool
        Vector3 targetPosition = open ? new Vector3(
            drawerSlide.localPosition.x, drawerSlide.localPosition.y, drawerMaxLimit) : initialPosition;
        while (drawerSlide.localPosition != targetPosition)
        {
            drawerSlide.localPosition = Vector3.MoveTowards(drawerSlide.localPosition, targetPosition, Time.deltaTime * drawerSpeed);

            //ensure the drawer space within our defined limits. 
            float clampedZ = Mathf.Clamp(drawerSlide.localPosition.z, drawerMinLimit, drawerMaxLimit);

            drawerSlide.localPosition = new Vector3(drawerSlide.localPosition.x, drawerSlide.localPosition.y, clampedZ);
            
            
            yield return null;
        }
        
    }
}