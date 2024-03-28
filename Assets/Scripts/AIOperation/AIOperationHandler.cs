using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOperationHandler : MonoBehaviour
{
    public static AIOperationHandler Instance;

    public GridObject[,] gridObjectArray;
    public int boardSize;

    public Occupant assumedWinner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AIMove()
    {
        // Update data
        gridObjectArray = GridSystem.Instance.gridObjectArray;
        boardSize = GridSystem.Instance.GetBoardSize();
        assumedWinner = Occupant.None;

        GridPosition targetGridPosition;
        if (GlobalParameters.Instance.Level == AILevel.Easy)
        {
            targetGridPosition = SimpleAIMove();
        }
        else
        {
            targetGridPosition = HardAIMove();
        }

        StartCoroutine(AIPlaceChess(targetGridPosition));
    }

    IEnumerator AIPlaceChess(GridPosition gridPosition)
    {
        yield return new WaitForSeconds(2f);
        ChessPlacingSystem.Instance.PlaceChess(gridPosition, Occupant.AI);
    }

    // Simple AI move
    public GridPosition SimpleAIMove()
    {
        List<GridPosition> availablePositions = GetAvailablePositions();
        if (availablePositions.Count > 0)
        {
            return availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
        }

        return new GridPosition { x = -1, z = -1 };
    }

    // Hard AI move
    public GridPosition HardAIMove()
    {
        GridPosition move = new GridPosition { x = -1, z = -1 };

        // 检查AI是否可以赢得比赛
        move = FindWinningMove(Occupant.AI);
        if (IsValidMove(move)) return move;

        // 阻止玩家赢得比赛
        move = FindWinningMove(Occupant.Player);
        if (IsValidMove(move)) return move;

        // 占据中心
        if (!gridObjectArray[1, 1].GetOccupiedStatus())
        {
            return new GridPosition { x = 1, z = 1 };
        }

        // 尝试占据角落
        move = TakeAnyCorner();
        if (IsValidMove(move)) return move;

        return SimpleAIMove();
    }

    // Is valid move
    public bool IsValidMove(GridPosition gridPosition)
    {
        if (gridPosition.x == -1 && gridPosition.z == -1)
        {
            return false;
        }

        if (!GridSystem.Instance.GetGridObject(gridPosition).GetOccupiedStatus())
        {
            return true;
        }

        return false;
    }

    private GridPosition FindWinningMove(Occupant player)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                GridPosition pos = new GridPosition { x = x, z = z };
                if (TestForWinningMove(pos, player)) return pos;
            }
        }
        return new GridPosition { x = -1, z = -1 };
    }

    private bool TestForWinningMove(GridPosition pos, Occupant player)
    {
        if (gridObjectArray[pos.x, pos.z].GetOccupiedStatus()) return false;

        // 模拟这一步
        gridObjectArray[pos.x, pos.z].SetOccupant(player); // 假设SetOccupant是设置占据者的方法
        gridObjectArray[pos.x, pos.z].SetOccupiedStatus(true);
        bool wins = CheckWin(player); // 假设CheckWin检查给定玩家是否赢得了比赛

        gridObjectArray[pos.x, pos.z].SetOccupant(Occupant.None); // 撤销模拟
        gridObjectArray[pos.x, pos.z].SetOccupiedStatus(false);

        return wins;
    }

    private GridPosition TakeAnyCorner()
    {
        int[,] corners = { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
        for (int i = 0; i < corners.GetLength(0); i++) // GetLength(0)获取行数
        {
            int x = corners[i, 0];
            int z = corners[i, 1];
            if (!gridObjectArray[x, z].GetOccupiedStatus())
            {
                return new GridPosition { x = x, z = z };
            }
        }
        return new GridPosition { x = -1, z = -1 }; // 表示没有找到可用的角落
    }

    private List<GridPosition> GetAvailablePositions()
    {
        List<GridPosition> availablePositions = new List<GridPosition>();
        for (int x = 0; x < boardSize; x++)
        {
            for (int z = 0; z < boardSize; z++)
            {
                if (!gridObjectArray[x, z].GetOccupiedStatus())
                {
                    availablePositions.Add(gridObjectArray[x, z].GetGridPosition());
                }
            }
        }
        return availablePositions;
    }

    // Check win
    public bool CheckWin(Occupant occupant)
    {
        // Check row
        for (int row = 0; row < boardSize; row++)
        {
            if (CheckLine(0, row, 1, 0))
            {
                if (assumedWinner == occupant)
                {
                    return true;
                }
            }
        }

        // Check column
        for (int col = 0; col < boardSize; col++)
        {
            if (CheckLine(col, 0, 0, 1))
            {
                if (assumedWinner == occupant)
                {
                    return true;
                }
            }
        }

        // Check diagonal
        if (CheckLine(0, 0, 1, 1) || CheckLine(0, boardSize - 1, 1, -1))
        {
            if (assumedWinner == occupant)
            {
                return true;
            }
        }

        return false;
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

        assumedWinner = firstOccupant;
        return true;
    }

}
