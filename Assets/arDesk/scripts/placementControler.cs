using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class placementControler : MonoBehaviour
{

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

    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

    }

    private bool TryGetTouchPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }



    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPos(out Vector2 touchPos))
        {
            return;
        }
        if (arRaycastManager.Raycast(touchPos,hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPos = hits[0].pose;

            Instantiate(placedPrefab, hitPos.position, hitPos.rotation);
        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
}
