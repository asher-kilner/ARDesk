using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;
    public GameObject WelcomePage;
    
    public Button Submitbtn;
    public void callRegister(){
        StartCoroutine(RegisterUser());
    }
    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username.text);
        form.AddField("email", email.text);
        form.AddField("password", password.text); 
        string PostURL = "http://localhost/augmented_desk/register_user.php";
        UnityWebRequest www = UnityWebRequest.Post(PostURL,form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text == "0"){
            print("user created successfully");
            DbManager.username = username.text;
        }
        else{
            print(www.downloadHandler.text);
        }
        
    }
    
}
