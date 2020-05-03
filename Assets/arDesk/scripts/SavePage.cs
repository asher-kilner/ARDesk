using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SavePage : MonoBehaviour
{
    public TMP_InputField notepad;
    public void savenotes()
    {
        StartCoroutine(Savenotebook());
    }
    

    IEnumerator Savenotebook()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", DbManager.id);
        form.AddField("content", notepad.text); 
        string PostURL = "http://localhost/augmented_desk/update_notes.php";
        UnityWebRequest www = UnityWebRequest.Post(PostURL, form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text == "0"){
            print("notes saved successfully");
        }
        else{
            print("error: " + www.downloadHandler.text);
        }
    }

}
