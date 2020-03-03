using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class bookControler : MonoBehaviour
{
    [SerializeField]
    private PlacementObject placedPrefab;

    [SerializeField]
    private Button placeButton;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Button openButton;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    private PlacementObject[] placedObjects;
    
    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    //private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        placeButton.onClick.AddListener(Dismiss);
        openButton.onClick.AddListener(OpenSelect);
    }

    private void Dismiss() => optionsPanel.SetActive(false);
    private void OpenSelect() => optionsPanel.SetActive(true);

    void Update()
    {
        if (placedPrefab == null|| optionsPanel.gameObject.activeSelf)
            return;

         Debug.LogError($"update");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Stationary)
            {
                Debug.LogError($"TOUCHING STATIONary");
                OpenSelect();
            }
            if (touch.phase == TouchPhase.Began)
            {
                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    //when you press down on the screen
                    Pose hitPose = hits[0].pose;
                    Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                }
                Debug.LogError($"TOUCHING DOWN");
                //if you touch the screen
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                //RaycastHit hitObject;
                //if (Physics.Raycast(ray, out hitObject))
                // {
                //     //if you hit an object
                //     lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                //     if (lastSelectedObject != null)//if you hit a placement object
                //     {
                //         //find the placement object you are clicking on
                //         PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                //         foreach (PlacementObject placementObject in allOtherObjects)
                //         {
                //             //if the position of the object is the same as what youre clicking on then that is the objct
                //             placementObject.Selected = placementObject == lastSelectedObject;
                //         }
                        
                //         SelectedObject(lastSelectedObject);
                //     }
                // }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.LogError($"TOUCHING released");
                lastSelectedObject.Selected = false;
            }
        }
    }
void SelectedObject(PlacementObject selected)
    {
        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
        foreach (PlacementObject current in allOtherObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            if(selected != current) 
            {
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else 
            {
                current.Selected = true;
                meshRenderer.material.color = activeColor;  
            }
            
        }
    }
}
