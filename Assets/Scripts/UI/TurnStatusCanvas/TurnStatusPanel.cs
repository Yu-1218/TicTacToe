using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStatusPanel : MonoBehaviour
{
    [SerializeField] private GameObject playerTurnTip;
    [SerializeField] private GameObject aiTurnTip;
    [SerializeField] private GameObject playerTurnReminderText;

    private void Start()
    {
        TurnManager.Instance.NextTurnEvent += ChangeStatusPanel;
        ChangeStatusPanel();
    }

    public void ChangeStatusPanel()
    {
        Turn currentTurn = TurnManager.Instance.GetCurrentTurn();

        if (currentTurn == Turn.Player)
        {
            playerTurnTip.SetActive(true);
            playerTurnReminderText.SetActive(true);
            aiTurnTip.SetActive(false);
        }
        else
        {
            playerTurnTip.SetActive(false);
            playerTurnReminderText.SetActive(false);
            aiTurnTip.SetActive(true);
        }
    }
}
