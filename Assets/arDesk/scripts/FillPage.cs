using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FillPage : MonoBehaviour
{
    public GameObject text;
    public string username = "asher";
    
    string GetPostByIdURL = "http://localhost/augmented_desk/bookByUser.php";
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        UnityWebRequest www = UnityWebRequest.Post(GetPostByIdURL, form);
        yield return www.SendWebRequest();
        
        
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else{
            var bookString = www.downloadHandler.text;
            print(bookString);
            
        }        
    }
}
