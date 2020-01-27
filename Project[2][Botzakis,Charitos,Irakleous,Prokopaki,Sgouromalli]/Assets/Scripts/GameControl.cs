using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private const float TIMER_VALUE = 45.0f;
    private bool isPaused;
    private bool timeElapsed;
    private float timeRemaining;
    private GateControl gateControl;
    private ScenesManager sm;
    
    private void Awake()
    {
        gateControl = this.GetComponent<GateControl>();
        sm = this.GetComponent<ScenesManager>();
    }
    void Start()
    {
        timeRemaining = TIMER_VALUE;
        TextManager.Instance.updateTimerText(timeRemaining);
        timeElapsed = false;
        isPaused = false;
        //TextManager.Instance
        resumeGame();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            resumeGame();
        }

      //  if (Input.GetKeyDown(KeyCode.Space))
     //   {
       //     TextManager.Instance.CreateText("+2");
        //    UpdateTimer(+2);
       // }
        if (gateControl.getGameBegun())
        {
            TimerRunning();
        }
    }
    #region PAUSE CONTROL
    public bool getPausedStatus() { return isPaused; }
    public void pauseGame()
    {
        isPaused = true;
        print("paused");
        Time.timeScale = 0; //set timescale to 0
        TextManager.Instance.panel.SetActive(true);
    }
    public void resumeGame()
    {
        isPaused = false;
        print("resumed");
        Time.timeScale = 1; //set timescale to 1
        TextManager.Instance.panel.SetActive(false);
    }
    #endregion
    #region TIMER
    private void TimerRunning()
    {
        if (!timeElapsed)
        {
            timeRemaining -= Time.deltaTime;
            //update UI here ! ! !
            TextManager.Instance.updateTimerText(timeRemaining);
            if (timeRemaining < 0) //when timer finishes
            {
                timeElapsed = true;
            }
        }
        else
        {
            finishGame();
        }
    }
    public void loadWinScene()
    {
        sm.loadVictoryScene();
    }
    public void finishGame()
    {
        sm.loadGameOverScene();
    }
    public void UpdateTimer(int amount)
    {
        timeRemaining += amount;
        TextManager.Instance.CreateText("+2");
    }
    private void timerReset()
    {
        timeRemaining = TIMER_VALUE;
        
        timeElapsed = false;
        print(timeRemaining);
    }
    #endregion
}
