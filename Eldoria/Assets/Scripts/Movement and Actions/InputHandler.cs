using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set;}
    public int NormalizedInputX { get; private set;}
    public int NormalizedInputY { get; private set;}
    public bool JumpInput { get; private set;}
    public bool JumpInputStop { get; private set;}
    public bool AttackInput { get; private set;}
    public bool SkillInput { get; private set;}
    public bool InteractInput { get; private set;}
    [SerializeField] private float inputHoldTime=.2f;
    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>().normalized;
        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) { AttackInput = true; }
        if (context.canceled) { AttackInput = false; }
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.started) { SkillInput = true; }
        if (context.canceled) { SkillInput = false; }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) {  InteractInput = true; }
        if (context.canceled) {  InteractInput = false; }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }


    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}

