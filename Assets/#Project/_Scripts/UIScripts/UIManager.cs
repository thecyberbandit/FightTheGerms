using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform playMenu;
    public RectTransform rulesMenu;
    public RectTransform instructionMenu;

    private void Start()
    {
        AudioManager.instance.PlaySound("background");
        playMenu.DOLocalMove(Vector2.zero, 0.5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlayButtonClicked()
    {
        Cursor.visible = false;
        StartCoroutine(ShowInstruction());
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator ShowInstruction()
    {
        playMenu.DOLocalMove(new Vector2(-1150f, 0f), 0.5f);
        rulesMenu.DOLocalMove(Vector2.zero, 0.5f);
        yield return new WaitForSeconds(5f);
        rulesMenu.DOLocalMove(new Vector2(-1150f, 0f), 0.5f);
        instructionMenu.DOLocalMove(Vector2.zero, 0.5f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
