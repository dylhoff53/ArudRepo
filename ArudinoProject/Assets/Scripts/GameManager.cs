using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public TMP_Text scoreText;
    public float WholeLevelTime;
    public float currentGameTime;
    public GameObject timesUpScreen;
    public GameObject blackScreen;
    public bool startFade;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.SetText("Score: " + playerScore);
    }

    public void ChangeScore(int add)
    {
        playerScore += add;
        scoreText.SetText("Score: " + playerScore);
    }

    public void Update()
    {
        currentGameTime += Time.deltaTime;
        if(currentGameTime >= WholeLevelTime)
        {
            timesUpScreen.SetActive(true);
            Invoke("TimesUp", 1.0f);
            Invoke("ChangeScene", 3.0f);
        }

        if(startFade == true)
        {
            blackScreen.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
        }
    }

    public void TimesUp()
    {
        startFade = true;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
