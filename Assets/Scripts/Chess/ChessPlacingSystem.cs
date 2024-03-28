using Unity.Mathematics;
using UnityEngine;

public enum ChessType
{
    Cross,
    Circle
}

public class ChessPlacingSystem : MonoBehaviour
{
    // Singleton
    public static ChessPlacingSystem Instance;

    // Raycast LayerMask
    public static LayerMask mousePlaneLayerMask;

    [SerializeField] private GameObject crossChess;
    [SerializeField] private GameObject circleChess;

    private ChessType playerChessType;
    private ChessType aiChessType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than 1 " + nameof(ChessPlacingSystem));
        }

        mousePlaneLayerMask = LayerMask.GetMask("MousePlane");
    }

    // Set Player ChessType
    public void SetPlayerChessType(ChessType chessType)
    {
        playerChessType = chessType;
        if (playerChessType == ChessType.Cross)
        {
            aiChessType = ChessType.Circle;
        }
        else
        {
            aiChessType = ChessType.Cross;
        }
        Debug.Log("Player: " + playerChessType);
        Debug.Log("AI: " + aiChessType);
    }

    public void PlaceChess(GridPosition targetPosition, Occupant sender)
    {
        GridObject gridObject = GridSystem.Instance.GetGridObject(targetPosition);

        // Place the chess if this grid is empty
        if (gridObject != null && !gridObject.GetOccupiedStatus())
        {
            // Instantiate chess visual
            if (sender == Occupant.Player)  // Player
            {
                if (playerChessType == ChessType.Cross)
                {
                    Instantiate(crossChess, GridSystem.Instance.GetWorldPosition(targetPosition), Quaternion.identity);
                }
                else
                {
                    Instantiate(circleChess, GridSystem.Instance.GetWorldPosition(targetPosition), Quaternion.identity);
                }
            }
            else  // AI
            {
                if (aiChessType == ChessType.Cross)
                {
                    Instantiate(crossChess, GridSystem.Instance.GetWorldPosition(targetPosition), Quaternion.identity);
                }
                else
                {
                    Instantiate(circleChess, GridSystem.Instance.GetWorldPosition(targetPosition), Quaternion.identity);
                }
            }

            // Audio
            AudioManager.Instance.PlayButtonClickSound();

            // Update chees board status
            gridObject.SetOccupiedStatus(true);
            gridObject.SetOccupant(sender);

            // Check for win
            if (!BoardConditionChecker.Instance.CheckForWin())
            {
                TurnManager.Instance.NextTurn();
            }
            else
            {
                GameplayEnd.Instance.DisplayResult();
            }
        }
    }

}
