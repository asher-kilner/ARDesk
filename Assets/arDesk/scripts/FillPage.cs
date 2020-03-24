using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FillPage : MonoBehaviour
{
    public TMP_InputField text;
    public string username = "asher";
    private BookCollection bookCollection;
    public string[] books;
    
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
            string jsonData = @www.downloadHandler.text;
            bookCollection = JsonUtility.FromJson<BookCollection>(jsonData);
            foreach(Book book in bookCollection.books)
            {
                if(book.username == username){
                    text.text = book.body;
                }
            }

        }        
    }

}
