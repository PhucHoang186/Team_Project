using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Interactable
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] LadderCheckPoint bottomCheckPoint;
        [SerializeField] LadderCheckPoint topCheckPoint;
        [SerializeField] float moveSpeed;
        private LadderCheckPoint currentCheckPoint;
        private PlayerController player;
        private bool startClimb;
        private bool isGoUp;

        void Start()
        {
            bottomCheckPoint.InitAction(OnEnterCheckPoint, OnExitCheckPoint);
            topCheckPoint.InitAction(OnEnterCheckPoint, OnExitCheckPoint);
        }

        public void OnEnterCheckPoint(LadderCheckPoint ladderCheckPoint, PlayerController player)
        {
            this.player = player;
            currentCheckPoint = ladderCheckPoint;
            isGoUp = bottomCheckPoint == ladderCheckPoint;
        }

        public void OnExitCheckPoint()
        {
            currentCheckPoint = null;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && currentCheckPoint != null)
            {
                HandleEntityMovement.ON_TOGGLE_MOVEMENT?.Invoke(false, player.gameObject);
                startClimb = true;
                int offsetDir = isGoUp ? 1 : -1;
                player.transform.position = currentCheckPoint.startPos.position + Vector3.up * 0.5f * offsetDir;
                if (!isGoUp)
                    player.transform.eulerAngles = new Vector3(0f, -transform.eulerAngles.y, 0f);
            }

            if (!startClimb)
                return;
            MovePlayer();
        }

        private void MovePlayer()
        {
            Vector3 moveVec = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + moveVec * moveSpeed, Time.deltaTime);
            if (player.transform.position.y >= topCheckPoint.startPos.position.y)
            {
                FinishClimb(topCheckPoint.endPos.position);
            }
            else if (player.transform.position.y <= bottomCheckPoint.startPos.position.y)
            {
                FinishClimb(bottomCheckPoint.endPos.position);
            }

        }

        private void FinishClimb(Vector3 finishPos)
        {
            player.transform.position = finishPos;
            startClimb = false;
            HandleEntityMovement.ON_TOGGLE_MOVEMENT?.Invoke(true, player.gameObject);
            player = null;
        }
    }
}
