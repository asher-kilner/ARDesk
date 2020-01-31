using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isSelected;

    public bool Selected
    {
        get
        {
            return this.isSelected;
        }
        set
        {
            isSelected = value;
        }
    }
}
