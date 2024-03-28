using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameHistoryManager : MonoBehaviour
{
    public static GameHistoryManager Instance;

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

    private const string GameHistoryKey = "GameHistory";
    private const int MaxHistoryCount = 10;

    // Write a game record
    public static void WriteGameRecord(string result)
    {
        // Beijing Time
        DateTime now = DateTime.UtcNow.AddHours(8);
        string formattedTime = now.ToString("yyyy年M月d日HH时mm分");
        string record = $"{formattedTime} {result}";

        string history = PlayerPrefs.GetString(GameHistoryKey, "");
        var records = new List<string>(history.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

        if (records.Count >= MaxHistoryCount)
        {
            records.RemoveAt(0); // remove earliest record
        }

        // Add new record
        records.Add(record);
        history = string.Join("\n", records);
        PlayerPrefs.SetString(GameHistoryKey, history);
        PlayerPrefs.Save();
    }

    // Get Current Record
    public static string GetGameHistory()
    {
        return PlayerPrefs.GetString(GameHistoryKey, "");
    }

    // Clear all records
    public static void ClearAllGameHistory()
    {
        PlayerPrefs.DeleteKey(GameHistoryKey);
        PlayerPrefs.Save();
    }
}
