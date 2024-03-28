using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayEnd : MonoBehaviour
{
    public static GameplayEnd Instance;

    [SerializeField] private GameObject gameplayEndPanel;
    [SerializeField] private TextMeshProUGUI resultTMP;
    [SerializeField] private GameObject turnStatusPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void DisplayResult()
    {
        StartCoroutine(DisplayPanel());
        WriteGameRecord();
    }

    public void WriteGameRecord()
    {
        Occupant winner = BoardConditionChecker.Instance.GetWinner();
        switch (winner)
        {
            case Occupant.None:
                GameHistoryManager.WriteGameRecord("平局");
                break;
            case Occupant.AI:
                GameHistoryManager.WriteGameRecord("失败");
                break;
            case Occupant.Player:
                GameHistoryManager.WriteGameRecord("胜利");
                break;
        }
    }

    IEnumerator DisplayPanel()
    {
        yield return new WaitForSeconds(0.5f);

        gameplayEndPanel.SetActive(true);
        Occupant winner = BoardConditionChecker.Instance.GetWinner();
        turnStatusPanel.SetActive(false);

        switch (winner)
        {
            case Occupant.None:
                resultTMP.text = "平局";
                break;
            case Occupant.AI:
                resultTMP.text = "失败";
                break;
            case Occupant.Player:
                resultTMP.text = "胜利";
                break;
        }
    }

    public void ReloadGameplayScene()
    {
        AudioManager.Instance.PlayButtonClickSound();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayButtonClickSound();

        TicSceneManager.Instance.EnterMainMenuScene();
    }

}
