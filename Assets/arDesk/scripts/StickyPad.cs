using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPad : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isSelected;
    public TextMesh body;
    public StickyPad(TextMesh contents){
        isSelected = false;
        body = contents;
    }
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
