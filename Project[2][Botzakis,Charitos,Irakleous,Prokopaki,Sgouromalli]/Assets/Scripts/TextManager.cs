using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public GameObject textPrefab;
    public RectTransform canvasTransform;  
    public Transform spawnPoint;
    public float speed;
    public Vector3 direction;
    public Text timerText;
    public GameObject panel;

    private static TextManager instance;
    private Vector3 spawnPosition;
    private void Start()
    {
        spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + 5, spawnPoint.position.z);
    }
    public static TextManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<TextManager>();
            }
            return instance;
        }
    }
    public void CreateText(string text)
    {
       GameObject sct = (GameObject) Instantiate(textPrefab, spawnPosition, Quaternion.identity);

        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<TimeText>().Initialize(speed, direction);
        sct.GetComponent<Text>().text = text;
        Destroy(sct, 5f);
    }
    public void updateTimerText(float currentTime)
    {
        timerText.text = currentTime.ToString("F0");
    }

}
