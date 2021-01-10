using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverController : MonoBehaviour
{
    public RectTransform gameOverPanel;
    public Text scoreText;

    private int finalScore;

    private void Start()
    {
        AudioManager.instance.PlaySound("background");

        finalScore = PlayerPrefs.GetInt("score");
        scoreText.text = "Your Score: " + finalScore.ToString();

        gameOverPanel.DOLocalMove(Vector2.zero, 0.5f);
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetButtonDown("Fire1") && Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
