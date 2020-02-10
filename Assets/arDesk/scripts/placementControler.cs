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
    private Button greenButton;

    [SerializeField]
    private Button redButton;

    [SerializeField]
    private Button blueButton;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private Text selectionText;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Button selectSreenButton;

    private PlacementObject[] placedObjects;

    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        // set initial prefab
        ChangePrefabTo("Blue");
        greenButton.onClick.AddListener(() => ChangePrefabTo("Green"));
        blueButton.onClick.AddListener(() => ChangePrefabTo("Blue"));
        redButton.onClick.AddListener(() => ChangePrefabTo("Red"));
        dismissButton.onClick.AddListener(Dismiss);
        selectSreenButton.onClick.AddListener(OpenSelect);
    }

    private void Dismiss() => optionsPanel.SetActive(false);
    private void OpenSelect() => optionsPanel.SetActive(true);

    void ChangePrefabTo(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        if (placedPrefab == null)
        {
            Debug.LogError($"Prefab with name {prefabName} could not be loaded, make sure you check the naming of your prefabs...");
        }

        switch (prefabName)
        {
            case "Blue":
                selectionText.text = $"Selected: <color='blue'>{prefabName}</color>";
                break;
            case "Red":
                selectionText.text = $"Selected: <color='red'>{prefabName}</color>";
                break;
            case "Green":
                selectionText.text = $"Selected: <color='green'>{prefabName}</color>";
                break;
        }

    }

    void Update()
    {
        if (placedPrefab == null|| optionsPanel.gameObject.activeSelf)
            return;

        Debug.LogError($"prefab is {placedPrefab}");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                //if you touch the screen
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    //if you hit an object
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (lastSelectedObject != null)//if you hit a placement object
                    {
                        //find the placement object you are clicking on
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
            }
        }

        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            //when you press down on the screen
            Pose hitPose = hits[0].pose;

            if (lastSelectedObject == null)
            {
                //if no selected object has been registered then put one down
                lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
            }
            else
            {
                if (lastSelectedObject.Selected)
                {
                    // if obect has been clicked on move its position
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
