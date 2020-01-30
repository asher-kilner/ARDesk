﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class placementControler : MonoBehaviour
{
    //[SerializeField]
    //private GameObject placedPrefab;

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

    //[SerializeField]
    //private Camera arcamera;

    //private Vector2 touchPosition = default;

    //private ARRaycastManager arRaycaseManager;

    //private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //private ARRaycastManager arRaycastManager;
    //private ARReferencePointManager arReferencePointManager;
    //private ARPlaneManager arPlaneManager;
    //private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();

    //void Awake()
    //{
    //    arRaycastManager = GetComponent<ARRaycastManager>();
    //    arReferencePointManager = GetComponent<ARReferencePointManager>();
    //    arPlaneManager = GetComponent<ARPlaneManager>();
    //}

    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if(Input.touchCount > 0)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }
    //    touchPosition = default;
    //    return false;
    //}
    //private void Start()
    //{

    //}

    //

    //// Update is called once per frame
    //void Update()
    //{
    //    if (placedPrefab == null)
    //    {
    //        return;
    //    }
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            newTouch = true;
    //            //write here code to be done imidiately on touch
    //        }
    //        if (newTouch)
    //        {
    //            //write here code that is done whilst touching
    //            //optionsMenu.SetActive(false);
    //            //foreach (ARReferencePoint referencePoint in referencePoints)
    //            //{
    //            //    arReferencePointManager.RemoveReferencePoint(referencePoint);
    //            //}
    //            touchTime += Time.deltaTime;
    //            if (touchTime > longPress)
    //            {
    //                //write here code that is called every frame after long press
    //                //var cameraForward = Camera.current.transform.forward;
    //                //var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
    //                //placementPose.rotation = Quaternion.LookRotation(cameraBearing);
    //                if (!calledLong)
    //                {
    //                    //code to be called once after long press
    //                    calledLong = true;

    //                    if (!TryGetTouchPosition(out Vector2 touchPos))
    //                    {
    //                        return;
    //                    }
    //                    if (arRaycastManager.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
    //                    {
    //                        Pose hitPos = hits[0].pose;
    //                        ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPos);
    //                        referencePoints.Add(referencePoint);

    //                        //optionsMenu.SetActive(true);
    //                        Instantiate(placedPrefab, hitPos.position, hitPos.rotation);             

    //                    }
    //                }
    //            }
    //        }
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            touchTime = 0;
    //            calledLong = false;
    //            newTouch = false;
    //        }
    //    }
    //}
    [SerializeField]
    private GameObject placedPrefab;

    public GameObject PlacedPrefab
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

    private bool newTouch;
    private float touchTime;
    private float longPress = 1;
    private bool calledLong = false;
    private Pose placementPose;

    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                newTouch = true;
                //write here code to be done imidiately on touch
            }
            if (newTouch)
            {
                //write here code that is done whilst touching
                touchTime += Time.deltaTime;
                if (touchTime > longPress)
                {
                    //write here code that is called every frame after long press
                    if (!calledLong)
                    {
                        //code to be called once after long press
                        calledLong = true;

                        if (!TryGetTouchPosition(out Vector2 touchPos))
                        {
                            return;
                        }
                        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            Pose hitPos = hits[0].pose;
                            Instantiate(placedPrefab, hitPos.position, hitPos.rotation);

                        }
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchTime = 0;
                calledLong = false;
                newTouch = false;
            }
        }
    }
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
