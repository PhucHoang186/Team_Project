using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entity
{
    public enum MovementState
    {
        None,
        Idle,
        Run,
        Jump,
        Stun,
    }

    public class HandleEntityMovement : MonoBehaviour
    {
        public static Action<bool, GameObject> ON_TOGGLE_MOVEMENT;
        [Header("Moving")]
        [SerializeField] protected float moveSpeed;
        [Header("Rotating")]
        [SerializeField] protected float rotationSpeed;
        [Header("Jumping")]
        [SerializeField] protected float gravityMultiplier;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float groundCheckRange = 0.61f;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected float delayCheckGround;
        protected float gravity = Physics.gravity.y;
        protected float velocity;
        protected float currentDelayGroundCheck;
        protected bool isGround;
        protected bool isMoveable;
        protected MovementState currentMovementState;
        protected Vector3 groundPos;

        void Start()
        {
            groundPos = transform.position;
            isMoveable = true;
            ON_TOGGLE_MOVEMENT += OnToggleMovement;
        }

        void OnDestroy()
        {
            ON_TOGGLE_MOVEMENT -= OnToggleMovement;
        }

        [Button]
        public void ToggleMoveOff()
        {
            ON_TOGGLE_MOVEMENT?.Invoke(false, gameObject);
        }

        [Button]
        public void ToggleMoveOn()
        {
            ON_TOGGLE_MOVEMENT?.Invoke(true, gameObject);
        }


        public void HandleMovement(EntityInput entityInput)
        {
            if (CheckNoneMoveableState(entityInput))
            {
                OnChangeMoveState(MovementState.None);
                return;
            }

            DetectGround();
            SetGravity();
            HandleMoveAndRotate(entityInput);
            HandleJump(entityInput);
        }

        protected void DetectGround()
        {
            if (currentDelayGroundCheck > 0)
            {
                currentDelayGroundCheck -= Time.deltaTime;
                return;
            }

            bool checkGround = Physics.CheckSphere(transform.position, groundCheckRange, groundLayer, QueryTriggerInteraction.Ignore);
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 100f, groundLayer, QueryTriggerInteraction.Ignore))
                groundPos = hit.point;

            if (isGround != checkGround)
            {
                isGround = checkGround;
                if (checkGround)
                    Landed();
            }
        }

        protected void SetGravity()
        {
            if (isGround)
                return;
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
        }

        protected void HandleJump(EntityInput entityInput)
        {
            if (entityInput.isJump && isGround)
                StartJump();
        }

        protected void Move(Vector3 moveDir)
        {
            if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, moveDir, 1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                transform.position = Vector3.Lerp(transform.position, transform.position + moveDir * moveSpeed, Time.deltaTime);
        }

        protected void Rotate(Vector3 moveDir)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        protected void HandleMoveAndRotate(EntityInput entityInput)
        {
            Vector3 moveDir = entityInput.moveVec;
            if (moveDir != Vector3.zero)
            {
                Move(moveDir);
                Rotate(moveDir);
                if (isGround)
                    OnChangeMoveState(MovementState.Run);
            }
            else
            {
                if (isGround)
                    OnChangeMoveState(MovementState.Idle);
            }
        }

        protected bool CheckNoneMoveableState(EntityInput entityInput)
        {
            return !entityInput.FinishAttack || !isMoveable;
        }


        protected void StartJump()
        {
            velocity = jumpForce;
            isGround = false;
            currentDelayGroundCheck = delayCheckGround;
            OnChangeMoveState(MovementState.Jump);
        }

        protected void Landed()
        {
            HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Jump_End, gameObject, 0.1f);
            velocity = 0f;
            transform.position = groundPos;
        }

        private void OnToggleMovement(bool isActive, GameObject gameObject)
        {
            if (this.gameObject != gameObject)
                return;

            isMoveable = isActive;
        }

        public void OnChangeMoveState(MovementState newState)
        {
            if (currentMovementState == newState)
                return;
            currentMovementState = newState;
            switch (newState)
            {
                case MovementState.Idle:
                    HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Idle, gameObject, 0.2f);
                    break;
                case MovementState.Run:
                    HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Run, gameObject, 0.2f);
                    break;
                case MovementState.Jump:
                    HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Jump_Start, gameObject, 0.2f);
                    break;
            }
        }
    }
}
