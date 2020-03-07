using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();


    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject arObject in arObjectsToPlace)
        {
            GameObject newObj = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newObj.name = arObject.name;
            arObjects.Add(arObject.name, newObj);
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // when an recognised image comes into frame update what is being shown
            Debug.Log($"object added");
            UpdateTrackingImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            Debug.Log($"object updated");
            UpdateTrackingImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Debug.Log($"object hidden");
            // when an image goes out of frame set to false
            arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateTrackingImage(ARTrackedImage trackedImage)
    {
        imageTrackedText.text = trackedImage.referenceImage.name;

        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
    }

    private void AssignGameObject(string name, Vector3 position)
    {
        if(arObjectsToPlace != null)
        {
            GameObject currObj = arObjects[name];
            currObj.SetActive(true);
            currObj.transform.position = position;
            currObj.transform.localScale = scaleFactor;
            foreach(GameObject x in arObjects.Values)
            {
                if(x.name != name)
                {
                    x.SetActive(false);
                }
            }
        }
    }
}