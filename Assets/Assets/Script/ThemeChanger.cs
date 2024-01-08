using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanger : MonoBehaviour
{



    public GameObject background;
    public GameObject pipe;
    public GameObject ground;
    public Material backgroundDay;
    public Material backgroundNight;
    public Material groundDay;
    public Material groundNight;
    public Sprite bottomNight;
    public Sprite topNight;
    public Sprite bottomDay;
    public Sprite topDay;

    public void SetDayTheme()
    {
        background.GetComponent<Renderer>().material = backgroundDay;
        ground.GetComponent<Renderer>().material = groundDay;

        Transform bottomPipeTransform = pipe.transform.Find("PillarBottom");
        Transform topPipeTransform = pipe.transform.Find("PillarTop");

        if (bottomPipeTransform != null && topPipeTransform != null)
        {
            SpriteRenderer bottomPipeRenderer = bottomPipeTransform.GetComponent<SpriteRenderer>();
            SpriteRenderer topPipeRenderer = topPipeTransform.GetComponent<SpriteRenderer>();

            if (bottomPipeRenderer != null && topPipeRenderer != null)
            {
                bottomPipeRenderer.sprite = bottomDay;
                topPipeRenderer.sprite = topDay;


            }
        }
    }

    // Method to set the night theme
    public void SetNightTheme()
    {
        background.GetComponent<Renderer>().material = backgroundNight;
        ground.GetComponent<Renderer>().material = groundNight;

        Transform bottomPipeTransform = pipe.transform.Find("PillarBottom");
        Transform topPipeTransform = pipe.transform.Find("PillarTop");

        if (bottomPipeTransform != null && topPipeTransform != null)
        {
            SpriteRenderer bottomPipeRenderer = bottomPipeTransform.GetComponent<SpriteRenderer>();
            SpriteRenderer topPipeRenderer = topPipeTransform.GetComponent<SpriteRenderer>();

            if (bottomPipeRenderer != null && topPipeRenderer != null)
            {
                bottomPipeRenderer.sprite = bottomNight;
                topPipeRenderer.sprite = topNight;


            }
        }
    }


 

}
