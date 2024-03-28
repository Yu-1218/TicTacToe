using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Turn
{
    Empty,
    Player,
    AI
}

public class TurnManager : MonoBehaviour
{
    // Singleton
    public static TurnManager Instance;

    public event Action NextTurnEvent;


    public Turn currentTurn;
    public TurnStatusPanel turnStatusPanel;
    private int totalRounds = 0;
    private bool gameStarted = false;
    private bool playerActed = false;
    private bool aiActed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame(Turn firstTurn)
    {
        currentTurn = firstTurn;
        gameStarted = true;
        totalRounds = 1;

        // Set Action flag
        if (firstTurn == Turn.Player)
        {
            playerActed = true;
            aiActed = false;
        }
        else
        {
            playerActed = false;
            aiActed = true;
            AIOperationHandler.Instance.AIMove();
        }

        turnStatusPanel.gameObject.SetActive(true);
    }

    public void NextTurn()
    {
        if (!gameStarted)
        {
            Debug.LogError("Game not started yet");
            return;
        }

        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.AI;
            playerActed = true;

            // AI move
            AIOperationHandler.Instance.AIMove();
        }
        else
        {
            currentTurn = Turn.Player;
            aiActed = true;
        }

        NextTurnEvent?.Invoke();

        if (playerActed && aiActed)
        {
            totalRounds++;
            Debug.Log("Total Round" + totalRounds);
            playerActed = false;
            aiActed = false;
        }
    }

    public Turn GetCurrentTurn()
    {
        if (!gameStarted)
        {
            return Turn.Empty;
        }

        return currentTurn;
    }

    public int GetTotalRounds()
    {
        return totalRounds;
    }


    // Test in Editor
    [BoxGroup("快捷开始")]
    [Button]
    public void QuickStart(Turn firstTurn)
    {
        StartGame(firstTurn);
    }
}

