using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour
{

    [SerializeField] private GameObject gameplaySelectionPanel;
    [SerializeField] private GameObject gameSettingCanvas;
    [SerializeField] private GameObject gameHistoryPanel;
    [SerializeField] private TextMeshProUGUI gameHistoryTMP;

    public void CreateGamePlay()
    {
        gameplaySelectionPanel.SetActive(true);
        AudioManager.Instance.PlayButtonClickSound();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenGameSetting()
    {
        gameSettingCanvas.SetActive(true);

        AudioManager.Instance.PlayButtonClickSound();
    }

    public void OpenGameHistory()
    {
        string history = GameHistoryManager.GetGameHistory();
        gameHistoryPanel.SetActive(true);
        gameHistoryTMP.text = history;

        AudioManager.Instance.PlayButtonClickSound();
    }

    public void CloseGameHistory()
    {
        gameHistoryPanel.SetActive(false);

        AudioManager.Instance.PlayButtonClickSound();
    }

    [Button("清空历史")] // Test case
    public void ClearHistory()
    {
        GameHistoryManager.ClearAllGameHistory();
    }

}
