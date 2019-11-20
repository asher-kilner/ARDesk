using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        get => gameObjectToCreate;
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

    void ChangePrefab(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
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
    private void Start()
    {

    }

    private bool newTouch;
    private float touchTime;
    private float longPress = 1;
    private bool calledLong = false;
    // Update is called once per frame
    void Update()
    {
        if (placedPrefab == null)
        {
            return;
        }
        if (Input.touchCount == 1)
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
                        if (arRaycastManager.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            var hitPos = hits[0].pose;
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

}
