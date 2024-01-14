using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class HandleEntityInput : MonoBehaviour
    {
        public EntityInput EntityInput { get; set; }

        void Awake()
        {
            EntityInput = new();
            EntityInput.FinishAttack = true;
        }

        void Update()
        {
            GetInput();
        }

        public virtual void GetInput()
        {
            // move
            EntityInput.moveVec = GetMovementInput();
            // rotate
            EntityInput.lookRotation = GetRotationInput();
            // attack
            EntityInput.isInstantAttackPressed = GetInstantAttackInput();
            EntityInput.isCastingAttackPressed = GetCastingAttackInput();
            EntityInput.isCastingAttackReleased = GetCastingAttackReleaseInput();
            EntityInput.isBlockPressed = GetBlockingInput();
            EntityInput.isHoldingCombatInput = GetHoldingAttackInput();
            // lock target
            EntityInput.isLockTarget = Input.GetKeyDown(KeyCode.LeftShift);
            EntityInput.isJump = GetJumpInput();
        }

        protected virtual Vector3 GetMovementInput()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        }

        protected virtual Vector3 GetRotationInput()
        {
            return new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
        }

        protected virtual bool GetInstantAttackInput()
        {
            return Input.GetMouseButtonDown(0);
        }

        protected virtual bool GetCastingAttackInput()
        {
            return Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R);
        }

        protected virtual bool GetCastingAttackReleaseInput()
        {
            return Input.GetMouseButtonUp(1);
        }

        protected virtual bool GetHoldingAttackInput()
        {
            return Input.GetMouseButton(1);
        }

        protected virtual bool GetBlockingInput()
        {
            return Input.GetKey(KeyCode.Q);
        }

        protected virtual bool GetJumpInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    [Serializable]
    public class EntityInput
    {
        public Vector3 lookRotation;
        public Vector3 moveVec;
        public bool isJump;
        public bool isInstantAttackPressed;
        public bool isCastingAttackPressed;
        public bool isCastingAttackReleased;
        public bool isBlockPressed;
        public bool isLockTarget;
        public bool isHoldingCombatInput;
        public bool FinishAttack;
        public bool StartAttack => isInstantAttackPressed || isCastingAttackPressed || isCastingAttackReleased;
    }
}


