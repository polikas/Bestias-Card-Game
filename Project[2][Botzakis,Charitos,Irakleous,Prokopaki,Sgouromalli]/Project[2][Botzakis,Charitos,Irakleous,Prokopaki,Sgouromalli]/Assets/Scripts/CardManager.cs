using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardList;
    private GameObject tempCard;
    private int playerClicks;
    private bool isRotating;
    private GateControl instance;
    private GameControl gameManager;
    private string tempTag;
    private int currentPairNumber = 9;
    private void Start()
    {
        playerClicks = 0;
        isRotating = false;
        Shuffle();
        instance = this.GetComponent<GateControl>();
        gameManager = this.GetComponent<GameControl>();
        tempCard = GameObject.FindGameObjectWithTag("medousa");
        tempTag = "medousa";
    }

    private void Update()
    {
        if(currentPairNumber <=0)
        {
            gameManager.loadWinScene();
        }
        if (Input.GetMouseButtonDown(0) && instance.getGameBegun())
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
                    if(playerClicks == 1)
                    {
                        tempTag = hit.collider.tag;
                        tempCard = hit.collider.gameObject;
                    }               
                    if(playerClicks == 2)
                    {
                        if(tempTag == hit.collider.tag)
                        {

                            // tempCard.gameObject.SetActive(false);
                            //hit.collider.gameObject.SetActive(false);
                            gameManager.UpdateTimer(+2);
                            currentPairNumber--;
                            tempCard.GetComponent<Collider>().enabled = false;
                            hit.collider.enabled = false;
                            Destroy(tempCard.gameObject, 2f);
                            Destroy(hit.collider.gameObject, 2f);
                        
                        }
                        else if(playerClicks == 2 && hit.collider.tag != tempTag)
                        {
                           
                            
                            StartCoroutine(RotateCard(tempCard.GetComponent<Collider>()));
                            StartCoroutine(RotateCard(hit.collider));
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
}
