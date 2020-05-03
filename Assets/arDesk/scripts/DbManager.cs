using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DbManager
{
    public static string username;
    public static int id;

    public static bool loggedIn {get {return username != null;}}

    public static void logOut(){
        username = null; 
        id = 0;
    }
}
