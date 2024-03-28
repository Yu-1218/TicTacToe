using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChessSelectionPanel : MonoBehaviour
{
    [SerializeField] private Button circleButton;
    [SerializeField] private Button crossButton;

    // Root of chess selection UI
    [SerializeField] private GameObject chessSelection;
    [SerializeField] private GameObject turnGeneration;

    // Randomple decided first turn
    private Turn firstTurn;

    // For card animation
    public RectTransform cardFrontFirst;   // Player is the first
    public RectTransform cardFrontLast;    // Player is not the first
    private RectTransform cardFront;
    public RectTransform cardBack;
    private bool isFrontVisible = false;
    public float flipSpeed = 0.2f;

    // Reminder Text
    [SerializeField] private TextMeshProUGUI firstTurnReminderText;

    private void Start()
    {
        circleButton.onClick.AddListener(() =>
        {
            ChessPlacingSystem.Instance.SetPlayerChessType(ChessType.Circle);
            AudioManager.Instance.PlayButtonClickSound();
            DecideFirstTurn();
        }
        );

        crossButton.onClick.AddListener(() =>
        {
            ChessPlacingSystem.Instance.SetPlayerChessType(ChessType.Cross);
            AudioManager.Instance.PlayButtonClickSound();
            DecideFirstTurn();
        }
        );
    }

    private void DecideFirstTurn()
    {
        chessSelection.SetActive(false);
        turnGeneration.SetActive(true);

        int result = Random.Range(0, 2);
        if (result == 0)
        {
            firstTurn = Turn.Player;
        }
        else
        {
            firstTurn = Turn.AI;
        }

        Invoke(nameof(FlipCard), 2);
    }

    public void FlipCard()
    {
        // Set correct card front
        if (firstTurn == Turn.Player)
        {
            cardFront = cardFrontFirst;
        }
        else
        {
            cardFront = cardFrontLast;
        }

        StartCoroutine(FlipAnimation());
    }

    IEnumerator FlipAnimation()
    {
        RectTransform flippingCard = isFrontVisible ? cardFront : cardBack;
        RectTransform appearingCard = isFrontVisible ? cardBack : cardFront;

        appearingCard.gameObject.SetActive(false);

        for (float scale = 1f; scale > 0; scale -= Time.deltaTime / flipSpeed)
        {
            flippingCard.localScale = new Vector3(scale, 1, 1);
            yield return null;
        }

        flippingCard.gameObject.SetActive(false);
        appearingCard.gameObject.SetActive(true);

        for (float scale = 0; scale < 1; scale += Time.deltaTime / flipSpeed)
        {
            appearingCard.localScale = new Vector3(scale, 1, 1);
            yield return null;
        }

        appearingCard.localScale = new Vector3(1, 1, 1);
        isFrontVisible = !isFrontVisible;

        yield return new WaitForSeconds(0.5f);

        // Set reminder text
        firstTurnReminderText.gameObject.SetActive(true);
        if (firstTurn == Turn.Player)
        {
            firstTurnReminderText.text = "您获得先手";
        }
        else
        {
            firstTurnReminderText.text = "AI获得先手";
        }

        yield return new WaitForSeconds(1.5f);
        StartGame();
    }

    private void StartGame()
    {
        TurnManager.Instance.StartGame(firstTurn);
        gameObject.SetActive(false);
    }


    [Button]
    public void TestFlipCard()
    {
        DecideFirstTurn();
    }

}
