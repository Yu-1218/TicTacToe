using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AILevel
{
    Easy,
    Hard
}


public class GlobalParameters : MonoBehaviour
{
    // Singleton
    public static GlobalParameters Instance;

    public AILevel Level;

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

    public void SetAILevel(AILevel expectedLevel)
    {
        Level = expectedLevel;
        Debug.Log(Level);
    }

}
