using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> PrimaryFireEvent;
    public event Action<Vector2> MoveEvent;
    private Controls controls;
    public Vector2 AimPosition { get; private set; }


    private void OnEnable() {
        if (controls == null) {
            controls = new Controls();
            controls.Player.SetCallbacks(this); // Set the callback functions to this script
        }

        controls.Player.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        if (context.performed){
            // ? is a null check operator, if PrimaryFireEvent is not null, then invoke it with the argument true, otherwise do nothing
            PrimaryFireEvent?.Invoke(true);
        } else {
            PrimaryFireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }
}
