using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    public string[] books;
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {
        UnityWebRequest bookdata = UnityWebRequest.Get("http://localhost/augmented_desk/book.php");
        yield return bookdata.SendWebRequest();
        
        
        if(bookdata.isNetworkError || bookdata.isHttpError) {
            Debug.Log(bookdata.error);
        }
        else{
            string bookString = bookdata.downloadHandler.text;
            print(bookString);
            books = bookString.Split(';');
        }        
    }

}
