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
    public TMP_InputField text;
    public Text db;
    public string username;
    private BookCollection bookCollection;
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.0.98/augmented_desk/book.php");
        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError || www == null) {
            Debug.Log(www.error);
        }
        else{
            var JsonCollection = www.downloadHandler.text;
            var books = JsonConvert.DeserializeObject<List<BookCollection>>(www.downloadHandler.text);
            foreach(var book in books){
                if(book.username == username){
                    text.text = book.body;
                    print(book.body);
                }
            }
            if(books == null){
                text.text = "[connection could not be made]";
            }
            print(books[0].username);
        }        
    }

}
