using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
    public GameObject[] gatesArray = new GameObject[2];
    public Transform[] pointsArray = new Transform[2];
    private bool gatesMoving = false;
    private float speed = 0.5f;
    private bool beginGame;
    public CameraShake cameraShake;
    void Start()
    {
        beginGame = false;
        StartCoroutine(initialiseOpeningProcess());
    }
    IEnumerator initialiseOpeningProcess()
    {
        yield return new WaitForSeconds(3f);
        gatesMoving = true;
    }

    public bool getGameBegun()
    {
        return beginGame;
    }
   
    void Update()
    {
        if(gatesMoving) //start moving process
        {
            gatesArray[0].transform.position -= Vector3.right * speed * Time.deltaTime;
            gatesArray[1].transform.position += Vector3.right * speed * Time.deltaTime;
            StartCoroutine(cameraShake.Shake(0.15f, 0.02f));
        }
        if(gatesArray[0].transform.position.x <= pointsArray[0].position.x && gatesArray[1].transform.position.x >= pointsArray[1].position.x)
        {
            gatesMoving = false; //stop movement
            beginGame = true;
        }
     
    }
}
