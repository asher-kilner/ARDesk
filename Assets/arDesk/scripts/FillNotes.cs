using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FillNotes : MonoBehaviour
{
    public string username;
    public Text db;
    private NoteCollection noteCollection;
    private List<string> userNotes;
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.0.98/augmented_desk/getStickyNotes.php");
        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else{
            var JsonCollection = www.downloadHandler.text;
            var notes = JsonConvert.DeserializeObject<List<BookCollection>>(www.downloadHandler.text);
            foreach(var note in notes){
                if(note.username == username){
                    userNotes.Add(note.body);
                }
            }
            if(notes == null){
                
            }
            foreach( var i in userNotes){
                print(i);
            }
        }        
    }

}
