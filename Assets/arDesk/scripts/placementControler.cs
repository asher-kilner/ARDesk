﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class placementControler : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    [SerializeField]
    private Camera arCamera;

    private PlacementObject[] placedObjects;

    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;

    private float touchTime;
    private float longPress = 1;
    private GameObject PlacedPrefab
    {
        get
        {
            return placedPrefab;
        }
        set
        {
            placedPrefab = value;
        }
    }


    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                touchTime += Time.deltaTime;
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (lastSelectedObject != null)
                    {
                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach (PlacementObject placementObject in allOtherObjects)
                        {
                            placementObject.Selected = placementObject == lastSelectedObject;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lastSelectedObject.Selected = false;
                touchTime = 0;
            }
        }

        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (lastSelectedObject == null)
            {
                lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
            }
            else
            {
                if (lastSelectedObject.Selected)
                {
                    lastSelectedObject.transform.position = hitPose.position;
                    lastSelectedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
//{
//    [SerializeField]
//    private GameObject placedPrefab;

//[SerializeField]
//private Camera arCamera;

//public GameObject PlacedPrefab
//{
//    get
//    {
//        return placedPrefab;
//    }
//    set
//    {
//        placedPrefab = value;
//    }
//}

//private bool newTouch;
//private float touchTime;
//private float longPress = 1;
//private bool calledLong = false;
//private Pose placementPose;

//private ARRaycastManager arRaycastManager;

//void Awake()
//{
//    arRaycastManager = GetComponent<ARRaycastManager>();
//}

//bool TryGetTouchPosition(out Vector2 touchPosition)
//{
//    if (Input.touchCount > 0)
//    {
//        touchPosition = Input.GetTouch(0).position;
//        return true;
//    }

//    touchPosition = default;

//    return false;
//}

//void Update()
//{
//    if (!TryGetTouchPosition(out Vector2 touchPosition))
//        return;
//    if (Input.touchCount > 0)
//    {
//        Touch touch = Input.GetTouch(0);
//        if (touch.phase == TouchPhase.Began)
//        {
//            newTouch = true;
//            //write here code to be done imidiately on touch
//        }

//            if (newTouch)
//            {
//                //write here code that is done whilst touching
//                touchTime += Time.deltaTime;
//                if (touchTime > longPress)
//                {
//                    //write here code that is called every frame after long press
//                    if (!calledLong)
//                    {
//                        //code to be called once after long press
//                        calledLong = true;
//                        if (!TryGetTouchPosition(out Vector2 touchPos))
//                        {
//                            return;
//                        }
//                        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
//                        {
//                            Pose hitPos = hits[0].pose;
//                            Instantiate(placedPrefab, hitPos.position, hitPos.rotation).GetComponent<PlacementObject>();
//                        }
//                    }
//                }
//            }
//            if (touch.phase == TouchPhase.Ended)
//        {
//            touchTime = 0;
//            calledLong = false;
//            newTouch = false;
//        }
//    }
//}
//static List<ARRaycastHit> hits = new List<ARRaycastHit>();
//}
