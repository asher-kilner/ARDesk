using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookController : MonoBehaviour
{
    public PageObject[] pages;
    PageObject currentPage;
    string buttonName;
    // Start is called before the first frame update
    void Start()
    {
        // //move all pages to the right
        // foreach (PageObject page in pages)
        // {
        //     if(page.isTurned)
        //     {
        //         turnPage("next");
        //     }
            
        // }
        // currentPage = pages[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if button clicked then turn page
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                buttonName = hit.transform.name;
                switch(buttonName)
                {
                    case "nextPage":
                        turnPage("next");
                        break;
                    case "previousPage":
                        turnPage("prevous");
                        break;
                    default:
                        break;
                }
            }
        }
    }
    static void turnPage(string dir)
    {
        
    }

}
