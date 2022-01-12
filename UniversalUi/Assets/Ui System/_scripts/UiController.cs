using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    #region Singleton Instantiation
    public static UiController UI;

    private void Awake()
    {
        Time.timeScale = 1;
        if (UI == null)
        {
            UI = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    #endregion

    #region Start and Update Logic
    private void Start()
    {
        StartLoading(5f);
    }

    private void Update()
    {
        if (GameManager.GM.IS_GAME_STARTED && GameResultMenu.activeSelf == false)
        {
            if (GameManager.GM.IS_GAME_PAUSED)
            {
                ShowGameResult(GameResultType.Game_Paused);
            }
            else if (GameManager.GM.IS_GAME_COMPLETED)
            {
                ShowGameResult(GameResultType.Game_Completed);
            }
            else if (GameManager.GM.IS_GAME_OVER)
            {
                Debug.LogError("OVER");
                ShowGameResult(GameResultType.Game_Over);
            }
        }
    }
    #endregion

    #region Loading Screen Logic
    [SerializeField]
    private GameObject LoadingScreenMenu;
    [SerializeField]
    private TMP_Text LoadingTxt;
    [SerializeField]
    private Slider LoadingBar;
    private float loadingTimer = 2f;
    private float addTime = 0.05f;

    public void StartLoading(float loadTime = 2.0f)
    {
        loadingTimer = loadTime;
        LoadingBar.value = 0.0f;
        LoadingBar.maxValue = loadingTimer;
        LoadingScreenMenu.SetActive(true);
        StartCoroutine(Loading_CR());
    }

    private IEnumerator Loading_CR()
    {
        

        while (LoadingBar.value < LoadingBar.maxValue)
        {
            LoadingBar.value += addTime;
            yield return new WaitForEndOfFrame();
        }

        LoadingScreenMenu.SetActive(false);
    }
    #endregion

    #region Main Menu Logic
    [SerializeField]
    private GameObject MainMenuPanel;
    [SerializeField]
    private GameObject GameplayPanel;
    [SerializeField]
    private GameObject OptionMenuPanel;

    public void PlayPressed()
    {
        //Play Game or Load Next Menu Screen
        MainMenuPanel.SetActive(false);
        GameplayPanel.SetActive(true);
        GameManager.GM.IS_GAME_STARTED = true;
    }

    public void ViewOptions()
    {
        //load options prefs

        //open options menu
        OptionMenuPanel.SetActive(true);
    }


    public void RateUs()
    {
        Application.OpenURL("some URL");
    }

    public void Share()
    {
        //share code here
    }

    public void MoreApps()
    {
        Application.OpenURL("some URL");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Gameplay & Game Result Logic
    public GameObject GameResultMenu;
    public GameObject ResumeBtn;
    public GameObject NextBtn;

    public TMP_Text gameResultTxt;
    public TMP_Text currScoreTxt;
    public TMP_Text highScoreTxt;


    public void ShowGameResult(GameResultType type)
    {
        
        GameResultMenu.SetActive(true);
        if (type == GameResultType.Game_Paused)
        {
            Time.timeScale = 0;
            gameResultTxt.text = "Game Paused!";
            currScoreTxt.gameObject.SetActive(false);
            highScoreTxt.gameObject.SetActive(false);
            
            ResumeBtn.SetActive(true);
            NextBtn.SetActive(false);
        }
        else if(type == GameResultType.Game_Over)
        {
            Debug.LogError("OVER 2");
            gameResultTxt.text = "Game Over!";
            currScoreTxt.text = "Score: 0000";
            highScoreTxt.text = "High Score: 0000";
            currScoreTxt.gameObject.SetActive(true);
            highScoreTxt.gameObject.SetActive(true);
            ResumeBtn.SetActive(false);
            NextBtn.SetActive(false);
            GameplayPanel.SetActive(false);


        }
        else if (type == GameResultType.Game_Completed)
        {
            gameResultTxt.text = "Level Completed!";
            currScoreTxt.text = "Score: 0000";
            highScoreTxt.text = "High Score: 0000";
            currScoreTxt.gameObject.SetActive(true);
            highScoreTxt.gameObject.SetActive(true);
            ResumeBtn.SetActive(false);
            NextBtn.SetActive(true);
            GameplayPanel.SetActive(false);
        }
        
    }


    public void PauseMenu()
    {
        GameManager.GM.IS_GAME_PAUSED = true;
        
    }

    public void ResumeFunc()
    {
        GameManager.GM.IS_GAME_PAUSED = false;
        Time.timeScale = 1;
        GameResultMenu.SetActive(false);
    }

    public void NextLvl()
    {
        GameManager.GM.ResetVals();
        //to-do
        //load next scene or level
        Time.timeScale = 1;
        GameResultMenu.SetActive(false);

        StartLoading(3f);
    }

    public void Restart()
    {
        GameManager.GM.ResetVals();
        //to-do
        //increment lvl and other stuff here
        GameResultMenu.SetActive(false);
        GameplayPanel.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        StartLoading(3f);
        MainMenuPanel.SetActive(true);

    }

    public void LoadMainMenu()
    {
        GameManager.GM.ResetVals();
        // to -do
        //increment lvl and other stuff here

        GameResultMenu.SetActive(false);
        GameplayPanel.SetActive(false);
        //SceneManager.LoadScene("SimpleScene");
        StartLoading(3f);
        MainMenuPanel.SetActive(true);
    }

    #endregion
}
