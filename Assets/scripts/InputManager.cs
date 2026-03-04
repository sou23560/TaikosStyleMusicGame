using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnDon;
    public event Action OnKa;

    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        var actions = playerInput.actions;
        actions["Don"].performed += Don;
        actions["Ka"].performed += Ka;
    }
    private void OnDisable()
    {
        var actions = playerInput.actions;
        actions["Don"].performed -= Don;
        actions["Ka"].performed -= Ka;
    }

    private void Don(InputAction.CallbackContext _)
    {
        OnDon?.Invoke();
    }

    private void Ka(InputAction.CallbackContext _)
    {  
        OnKa?.Invoke(); 
    }
}

