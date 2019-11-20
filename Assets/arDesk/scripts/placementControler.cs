using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class placementControler : MonoBehaviour
{

    private GameObject placedPrefab;

    [SerializeField]
    private GameObject optionsmenu;

    [SerializeField]
    private Button square;

    [SerializeField]
    private Button circle;

    [SerializeField]
    private Button tree;


    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        ChangePrefab("square");

        square.onClick.AddListener(() => ChangePrefab("Tree"));
        circle.onClick.AddListener(() => ChangePrefab("Square"));
        tree.onClick.AddListener(() => ChangePrefab("Sphere"));
    }

    void ChangePrefab(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
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

                        if (!TryGetTouchPos(out Vector2 touchPos))
                        {
                            return;
                        }
                        if (arRaycastManager.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            var hitPos = hits[0].pose;
                            Instantiate(optionsmenu, hitPos.position, hitPos.rotation); 

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
