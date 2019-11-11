using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class placementControler : MonoBehaviour
{

    [SerializeField]
    private GameObject gameObjectToCreate;

    [SerializeField]
    private PlacementObject[] placementObjects;

    [SerializeField]
    private Color activecolour = Color.red;

    [SerializeField]
    private Color inactivecolour = Color.grey;

    [SerializeField]
    private Camera arcamera;

    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycaseManager;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject placedPrefab
    {
        get
        {
            return gameObjectToCreate;
        }
        set
        {
            gameObjectToCreate = value;
        }
    }

    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
        }
    }

    
}
