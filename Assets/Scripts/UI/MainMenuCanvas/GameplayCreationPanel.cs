using UnityEngine;
using UnityEngine.UI;

public class GameplayCreationPanel : MonoBehaviour
{
    public Color selectedColor;

    [SerializeField] private Image simpleAIButton;
    [SerializeField] private Image hardAIButton;

    [SerializeField] private Button startGameplayButton;

    // Default
    private void Start()
    {
        SelectSimpleAI();
        startGameplayButton.onClick.AddListener(() =>
        {
            TicSceneManager.Instance.EnterGameplayScene();
            AudioManager.Instance.PlayButtonClickSound();
        });
    }

    // Callback for simple AI selection
    public void SelectSimpleAI()
    {
        GlobalParameters.Instance.SetAILevel(AILevel.Easy);

        simpleAIButton.color = selectedColor;
        hardAIButton.color = Color.white;

        AudioManager.Instance.PlayButtonClickSound();
    }

    // Callback for hard AI selection
    public void SelectHardAI()
    {
        GlobalParameters.Instance.SetAILevel(AILevel.Hard);

        hardAIButton.color = selectedColor;
        simpleAIButton.color = Color.white;

        AudioManager.Instance.PlayButtonClickSound();
    }

    // Callback for cancel button
    public void CloseCreationPanel()
    {
        gameObject.SetActive(false);

        AudioManager.Instance.PlayButtonClickSound();
    }


}
