using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public TextMeshProUGUI user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DbManager.loggedIn){
            user.text = "welcome: " + DbManager.username;
        }
    }
}
