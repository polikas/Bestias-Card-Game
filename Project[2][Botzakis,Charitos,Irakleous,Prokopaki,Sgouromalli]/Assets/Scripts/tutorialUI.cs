using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialUI : MonoBehaviour
{
    public GameObject panel;
    private static tutorialUI instance;
    // Start is called before the first frame update
    public static tutorialUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<tutorialUI>();
               
            }
            return instance;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
