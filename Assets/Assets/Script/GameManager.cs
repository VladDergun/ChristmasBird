using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;

    public GameObject themeChanger;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject newGame;


    public Button continueGame;
    public Button ExitButton;
    public Button nightButton;
    public Button dayButton;
    
    public Text Best;
    public Text coinText;
    public Text bestScore;
    public Text scoreText;
    public Text countdown;

    private int coin;
    private int score;
    private int bscore;
    bool mode = true; //false - daymode, true - nightmode
    
    const float countdownTime = 3f; 


    private const string CoinKey = "Coin";
    private const string ScoreKey = "Score";

    public void Awake(){
        
        continueGame.gameObject.SetActive(false);
        newGame.SetActive(false);
        gameOver.SetActive(false);
        MoveButton(new Vector2(0, 150f));
        Pause();
       
    }
    void Start()
    {
        // Sync the frame rate to the screen's refresh rate
        countdown.gameObject.SetActive(false);
        ChangeMode();
        LoadScore();
        LoadCoins();
        QualitySettings.vSyncCount = 1;
    }
    
    public void DisableMenu()
    {
        countdown.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        playButton.SetActive(false);
        gameOver.SetActive(false);
        newGame.SetActive(false);
        Best.gameObject.SetActive(false);
        bestScore.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        nightButton.gameObject.SetActive(false);
        dayButton.gameObject.SetActive(false);
    }
    public void Play(){
        
        score = 0;
        scoreText.text = score.ToString();
        player.continueGame = false;
        DisableMenu();
        

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for(int i =0; i < pipes.Length; i++){
            Destroy(pipes[i].gameObject);
        }

    }
    public void Continue()
    {  
       
       DisableMenu();
       countdown.gameObject.SetActive(true);
       StartCoroutine(CountdownAndEnablePlayer(countdownTime));
       



    }
    public void Pause(){
        LoadScore();
        SaveScore();
        LoadScore();
        
        
        Time.timeScale = 0f;
        
        player.enabled = false;
    }

    public void GameOver()
    {
        Pause();
        gameOver.SetActive(true);
        Best.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(true);
        StartCoroutine(WaitForMenuToEnable(0.2f));   
    }
    public void EnableMenu() {

        
        newGame.SetActive(true);
        
        continueGame.gameObject.SetActive(true);
        ExitButton.gameObject .SetActive(true);
        nightButton.gameObject .SetActive(true);
        dayButton.gameObject .SetActive(true);
        MoveButton(new Vector2(0, 69f));
        continueGame.interactable = false;
        if (coin >= 50)
        {
            continueGame.interactable = true;
            continueGame.gameObject.SetActive(true);
        }
       
       

    }

    public void ChangeMode()
    {
        mode = !mode;
        ThemeChanger tChanger = themeChanger.GetComponent<ThemeChanger>();

        if (mode == false)
        {
            
            tChanger.SetDayTheme();
            dayButton.interactable = true;
            nightButton.interactable = false;
        }
        else if (mode == true) 
        {
            tChanger.SetNightTheme();
            dayButton.interactable = false;
            nightButton.interactable = true;
        }
        
    }
    



    public void Exit() {
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

    public void AddScore(){
        
        score++;
        scoreText.text = score.ToString();
    }
    public void LoadScore()
    {
        bscore = PlayerPrefs.GetInt(ScoreKey, 0);
        bestScore.text = bscore.ToString();
    }

    public void SaveScore()
    {
        if (score >= bscore)
        {
            bscore = score;
            PlayerPrefs.SetInt(ScoreKey, bscore);
            PlayerPrefs.Save();
        }
    }


    public void AddCoin()
    {
        coin++;
        coinText.text = coin.ToString();
    }
    public void SaveCoins(){
        PlayerPrefs.SetInt(CoinKey, coin);
        PlayerPrefs.Save();

    }
    public void LoadCoins()
    {
        coin = PlayerPrefs.GetInt(CoinKey, 0);
        coinText.text = coin.ToString();
    }
    void MoveButton(Vector2 newpos)
    {

        RectTransform buttonRectTransform = ExitButton.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = newpos;

    }

    IEnumerator CountdownAndEnablePlayer(float countdownDuration)
    {
        
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;

        while (elapsedTime < countdownDuration)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            int displayTime = Mathf.CeilToInt(countdownDuration - elapsedTime);
            countdown.text = displayTime.ToString();
            yield return null;
        }

        
        Time.timeScale = 1f;

        
        player.continueGame = true;
        player.enabled = true;
        countdown.gameObject.SetActive(false);
        coin -= 50;
        SaveCoins();
        LoadCoins();
        yield return null;
    }
    IEnumerator WaitForMenuToEnable(float seconds)
    {

        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < seconds)
        {

            yield return null; // Yielding null makes the coroutine wait for the next frame
        }

        EnableMenu();
    }




}