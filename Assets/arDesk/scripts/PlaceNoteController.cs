using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNoteController : MonoBehaviour
{
    List<StickyPad> openPads = new List<StickyPad>();
    public TextMesh content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began){
                 Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    StickyPad n = new StickyPad(content);
                    openPads.Add(n);
                }
            }
           
        }

        //display all open pads
    }
}
