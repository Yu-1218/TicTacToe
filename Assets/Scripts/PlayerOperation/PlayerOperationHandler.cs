using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOperationHandler : MonoBehaviour
{
    private void Start()
    {
        InputReader.Instance.MouseClickEvent += ClickToPlaceChess;
    }

    public void ClickToPlaceChess()
    {
        // Not Player Turn
        if (TurnManager.Instance.GetCurrentTurn() != Turn.Player)
        {
            return;
        }

        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ChessPlacingSystem.mousePlaneLayerMask))
        {
            Vector3 hitPosition = raycastHit.collider.gameObject.transform.position;

            // Place Chess
            ChessPlacingSystem.Instance.PlaceChess(GridSystem.Instance.GetGridPosition(hitPosition), Occupant.Player);
        }
    }

}
