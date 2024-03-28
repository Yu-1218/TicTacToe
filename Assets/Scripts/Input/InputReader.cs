using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public static InputReader Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    // Mouse Click Event
    public event Action MouseClickEvent;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInputActions = new PlayerInputActions();
        DontDestroyOnLoad(gameObject);

        // Callback binding
        playerInputActions.Player.SetCallbacks(this);
        playerInputActions.Player.Enable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseClickEvent?.Invoke();
        }
    }
}
