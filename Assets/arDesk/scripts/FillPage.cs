using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FillPage : MonoBehaviour
{
    public TMP_InputField notepad;
    void Start()
    {
        StartCoroutine(GetText());
    }
    

    IEnumerator GetText() {

        yield return new WaitUntil(() => DbManager.loggedIn == true);
        
        WWWForm form = new WWWForm();
        form.AddField("id", DbManager.id);
        string PostURL = "http://localhost/augmented_desk/retrieve_notes.php";
        UnityWebRequest www = UnityWebRequest.Post(PostURL,form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text[0] == '0'){
            print("notes loaded");
            notepad.text = www.downloadHandler.text.Split('\t')[1];
        } else if(www.downloadHandler.text[0] == '7'){
            print("no current book");
            notepad.text = "{create notes here}";
        } else {
            print("user log in failed: " +  www.downloadHandler.text);
        }
    }

}
