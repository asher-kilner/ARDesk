using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AddNote : MonoBehaviour
{
    public string username;
    public TextMeshProUGUI text;
    public Text db;
    string btnName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                btnName = hit.transform.name;
                if(btnName == "create")
                {
                    StartCoroutine(SaveNote());
                    text.text = "";
                }
            }
        }
    }

    IEnumerator SaveNote()
    {
        print("saving content");
        string PostURL = "http://192.168.0.98/augmented_desk/insertStickyNote.php";
        WWWForm form = new WWWForm();
        print(text.text);
        print(username);
        form.AddField("username", username);
        form.AddField("body", text.text);
        UnityWebRequest www = UnityWebRequest.Post(PostURL,form);
        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        print("successfuly saved");
           
    }
}
