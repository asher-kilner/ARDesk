using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class findDeskSurface  : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARRaycastManager arRaycast;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private Vector2 touchposition = default;
    private Camera arCamera;
    private bool onTouchHold = false;
    // Start is called before the first frame update
    void Start()
    {
        arRaycast = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposition = touch.position;
            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touchposition);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    if(hitObject.transform.name.Contains("notepad"))
                    {
                        onTouchHold = true;
                    }
                }
            }
            if(touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;    
            }
        }
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycast.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if(placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);  
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
