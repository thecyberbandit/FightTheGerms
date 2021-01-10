using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Virus, Bacteria
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RadialProgressBar bar;
    public GameObject countDown;

    public Text scoreText;
    public Text modeText;
    public Text countdownText;

    [HideInInspector]
    public int score;
    [HideInInspector]
    public GameMode mode;

    private Text timerText;
    private bool isGameStarted;
    private float timeLeft = 60f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AudioManager.instance.StopSound("background");

        timerText = bar.GetComponentInChildren<Text>();
        isGameStarted = false;
        score = 0;
        mode = GameMode.Virus;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        AudioManager.instance.PlaySound("countdown");
        countdownText.text = "3";
        Debug.Log(countdownText.text);
        countDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);

        countdownText.text = "2";
        countDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);

        countdownText.text = "1";
        countDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);

        countdownText.text = "GO";
        countDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);

        isGameStarted = true;
    }

    private void Update()
    {
        if (mode == GameMode.Virus)
        {
            modeText.text = "Virus Mode";
        }
        else
        {
            modeText.text = "Bacteria Mode";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (isGameStarted)
        {
            bar.amount += 1.66f * Time.deltaTime;
            float amountFillled = bar.amount / 99f;
            bar.imageBackground.fillAmount = amountFillled;

            int temp = (int)timeLeft;
            timerText.text = temp.ToString();

            timeLeft -= Time.deltaTime;

            if (Input.GetMouseButtonDown(1))
                if (mode == GameMode.Virus)
                    mode = GameMode.Bacteria;
                else
                    mode = GameMode.Virus;
        }

        if (timeLeft < 0)
        {
            GameOver();
        }

        scoreText.text = "Score: " + score.ToString();
    }

    void GameOver()
    {
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene(2);
    }
}
