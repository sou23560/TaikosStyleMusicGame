using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnDon;
    public event Action OnKa;
    public event Action OnPause;
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
        actions["Pause"].performed += Pause; 
    }

    private void Pause(InputAction.CallbackContext _)
    {
        OnPause?.Invoke();
    }

    private void OnDisable()
    {
        var actions = playerInput.actions;
        actions["Don"].performed -= Don;
        actions["Ka"].performed -= Ka;
        actions["Pause"].performed -= Pause;
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

