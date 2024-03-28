using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySetting : MonoBehaviour
{
    [SerializeField] private GameObject gameplaySelectionPanel;

    // Volume
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectsSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(HandleBGMSliderValueChanged);
        bgmSlider.value = AudioManager.Instance.GetBGMVolume();

        effectsSlider.onValueChanged.AddListener(HandleEffectsSliderValueChanged);
        effectsSlider.value = AudioManager.Instance.GetEffectsVolume();
    }

    private void HandleBGMSliderValueChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetBGMVolume(value);
        }
    }

    private void HandleEffectsSliderValueChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetEffectsVolume(value);
        }
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlayButtonClickSound();

        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayButtonClickSound();

        TicSceneManager.Instance.EnterMainMenuScene();
    }

    public void OpenGameSetting()
    {
        AudioManager.Instance.PlayButtonClickSound();
        gameplaySelectionPanel.SetActive(true);
    }

    public void CloseGameSetting()
    {
        AudioManager.Instance.PlayButtonClickSound();
        gameplaySelectionPanel.SetActive(false);
    }

}
