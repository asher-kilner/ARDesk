using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FillPage : MonoBehaviour
{
    public TMP_InputField text;
    public string username;
    private BookCollection bookCollection;
    private string[] books;
    
    string GetPostURL = "http://localhost/augmented_desk/book.php";
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        UnityWebRequest www = UnityWebRequest.Get(GetPostURL);
        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else{
            var JsonCollection = www.downloadHandler.text;
            var books = JsonConvert.DeserializeObject<List<BookCollection>>(www.downloadHandler.text);
            foreach(var book in books){
                if(book.username == username){
                    text.text = book.body;
                }
            }
            print(books[0].username);
        }        
    }

}
