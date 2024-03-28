using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TicSceneManager : MonoBehaviour
{
    public static TicSceneManager Instance;

    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;

    [SerializeField] private GameObject loadingCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterGameplayScene()
    {
        StartCoroutine(FadeInGamePlayScene());
    }

    public void EnterMainMenuScene()
    {
        StartCoroutine(FadeInMainMenuScene());
    }

    IEnumerator FadeInGamePlayScene()
    {
        // Fade in
        loadingCanvas.SetActive(true);
        float currentTime = 0f;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            loadingCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, currentTime / fadeInDuration);
            yield return null;
        }

        loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;

        // Load Scene
        SceneManager.LoadSceneAsync(1);
        AudioManager.Instance.SwitchGameplayBGM();

        // Fade out
        currentTime = 0f;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            loadingCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, currentTime / fadeOutDuration);
            yield return null;
        }

        loadingCanvas.GetComponent<CanvasGroup>().alpha = 0;
        loadingCanvas.SetActive(false);
    }

    IEnumerator FadeInMainMenuScene()
    {
        // Fade in
        loadingCanvas.SetActive(true);
        float currentTime = 0f;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            loadingCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, currentTime / fadeInDuration);
            yield return null;
        }

        loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;

        // Load Scene
        SceneManager.LoadSceneAsync(0);
        AudioManager.Instance.SwitchMainMenuBGM();

        // Fade out
        currentTime = 0f;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            loadingCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, currentTime / fadeOutDuration);
            yield return null;
        }

        loadingCanvas.GetComponent<CanvasGroup>().alpha = 0;
        loadingCanvas.SetActive(false);
    }
}
