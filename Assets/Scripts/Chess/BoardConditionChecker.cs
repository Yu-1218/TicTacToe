using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardConditionChecker : MonoBehaviour
{
    // Singleton
    public static BoardConditionChecker Instance;

    private GridObject[,] gridObjectArray;
    private int boardSize;

    private GridPosition[] winningLine;
    private Occupant winner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool CheckForWin()
    {
        gridObjectArray = GridSystem.Instance.gridObjectArray;
        boardSize = GridSystem.Instance.GetBoardSize();

        winningLine = null;
        winner = Occupant.None;

        // Check row
        for (int row = 0; row < boardSize; row++)
        {
            if (CheckLine(0, row, 1, 0))
                return true;
        }

        // Check column
        for (int col = 0; col < boardSize; col++)
        {
            if (CheckLine(col, 0, 0, 1))
                return true;
        }

        // Check diagonal
        if (CheckLine(0, 0, 1, 1) || CheckLine(0, boardSize - 1, 1, -1))
            return true;

        // Check draw
        if (CheckDraw())
        {
            return true;
        }

        return false;
    }

    private bool CheckDraw()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (!gridObjectArray[i, j].GetOccupiedStatus())
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Check line in specific direction
    private bool CheckLine(int startX, int startY, int stepX, int stepY)
    {
        Occupant firstOccupant = gridObjectArray[startX, startY].GetOccupant();
        if (firstOccupant == Occupant.None) return false;

        List<GridPosition> positions = new List<GridPosition>();

        for (int i = 0; i < boardSize; i++)
        {
            int x = startX + i * stepX;
            int y = startY + i * stepY;

            if (!gridObjectArray[x, y].GetOccupiedStatus() || gridObjectArray[x, y].GetOccupant() != firstOccupant)
                return false;

            positions.Add(new GridPosition(x, y));
        }
        winningLine = positions.ToArray();
        winner = firstOccupant;
        return true;
    }

    public GridPosition[] GetWinningLine()
    {
        return winningLine;
    }

    public Occupant GetWinner()
    {
        return winner;
    }

}
