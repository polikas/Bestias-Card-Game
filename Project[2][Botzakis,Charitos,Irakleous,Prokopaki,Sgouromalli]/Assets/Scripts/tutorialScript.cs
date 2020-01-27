using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    public GameObject[] cardList;
    private GameObject tempCard;
    private int playerClicks;
    private bool isRotating;
    // private GateControl instance;
    //private GameControl gameManager;
    private ScenesManager sc;
    private string tempTag;
    private int currentPairNumber = 3;
    private bool isPasued;
    private void Start()
    {
        isPasued = false;
        playerClicks = 0;
        isRotating = false;
        //Shuffle();
        sc = GetComponent<ScenesManager>();
        tempCard = GameObject.FindGameObjectWithTag("cyclops");
        tempTag = "cyclops";
    }
    #region tut pause control 
    public void pauseGame()
    {
        isPasued = true;
        print("paused");
        Time.timeScale = 0; //set timescale to 0
        tutorialUI.Instance.panel.SetActive(true);
    }
    public void resumeGame()
    {
        isPasued = false;
        print("resumed");
        Time.timeScale = 1; //set timescale to 1
        tutorialUI.Instance.panel.SetActive(false);
        resumeGame();
    }
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPasued)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPasued)
        {
            resumeGame();
        }
        if (currentPairNumber <= 0)
        {//tutorial finish scene
            //gameManager();
            sc.loadMenu();
        }
        if (Input.GetMouseButtonDown(0) && !isPasued)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CardDeck"))) // added LayerMask  and math.infinity so it works properly
            {
                if (isRotating == false && playerClicks <= 1)
                {
                    print(hit.collider.name);
                    StartCoroutine(RotateCard(hit.collider));

                    playerClicks++;
                    if (playerClicks == 1)
                    {
                        tempTag = hit.collider.tag;
                        tempCard = hit.collider.gameObject;
                    }
                    if (playerClicks == 2)
                    {
                        if (tempTag == hit.collider.tag)
                        {

                            // tempCard.gameObject.SetActive(false);
                            //hit.collider.gameObject.SetActive(false);
                            //gameManager.UpdateTimer(+2);
                            currentPairNumber--;
                            tempCard.GetComponent<Collider>().enabled = false;
                            hit.collider.enabled = false;
                            Destroy(tempCard.gameObject, 2f);
                            Destroy(hit.collider.gameObject, 2f);

                        }
                        else if (playerClicks == 2 && hit.collider.tag != tempTag)
                        {

                            StartCoroutine(WrongMatchRotation(hit.collider, tempCard.GetComponent<Collider>()));
                            // StartCoroutine(RotateCard(tempCard.GetComponent<Collider>()));
                            // StartCoroutine(RotateCard(hit.collider));
                        }
                        playerClicks = 0;
                    }

                }
            }
        }
    }


    public void Shuffle()
    {
        for (int i = 0; i < cardList.Length; i++)
        {
            Vector3 temp = cardList[i].transform.position;
            int randomIndex = Random.Range(0, cardList.Length);
            cardList[i].transform.position = cardList[randomIndex].transform.position;
            cardList[randomIndex].transform.position = temp;
        }
    }

    public IEnumerator RotateCard(Collider objectSelected)
    {
        isRotating = true;
        print("aaa");
        for (int i = 0; i < 30; i++)
        {
            objectSelected.gameObject.transform.Rotate(new Vector3(0, 6, 0)); //mistake was here, now you are moving what your raycasthit returns (the card that you clicked)
                                                                              //previously you were rotating the gamemanager object (the empty one)
            yield return 0;
        }

        isRotating = false;
    }
    public IEnumerator WrongMatchRotation(Collider objectSelected, Collider tempSelected)
    {
        objectSelected.enabled = false;
        tempSelected.enabled = false;
        yield return new WaitForSeconds(1.5f);
        isRotating = true;
        print("aaa");
        for (int i = 0; i < 30; i++)
        {
            objectSelected.gameObject.transform.Rotate(new Vector3(0, 6, 0)); //mistake was here, now you are moving what your raycasthit returns (the card that you clicked)
            tempSelected.gameObject.transform.Rotate(new Vector3(0, 6, 0));                                                //previously you were rotating the gamemanager object (the empty one)
            yield return 0;
        }
        objectSelected.enabled = true;
        tempSelected.enabled = true;
        isRotating = false;
    }
}
