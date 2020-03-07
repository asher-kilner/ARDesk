using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageObject : MonoBehaviour
{
    public bool isTurned;

    public bool Turned
    {
        get
        {
            return this.isTurned;
        }
        set
        {
            isTurned = value;
        }
    }
}
