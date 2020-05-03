using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public GameObject WelcomePage;
    
    public Button Submitbtn;

    public void callLogin(){
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser(){
        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("password", password.text); 
        string PostURL = "http://localhost/augmented_desk/login_user.php";
        UnityWebRequest www = UnityWebRequest.Post(PostURL,form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text[0] == '0'){
            print("user logged in successfully");
            DbManager.id = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            DbManager.username = www.downloadHandler.text.Split('\t')[2];
            WelcomePage.GetComponent<Canvas>().enabled = false;
        }
        else{
            print("user log in failed: " +  www.downloadHandler.text);
        }
        
        print(www.responseCode);

    }
}
