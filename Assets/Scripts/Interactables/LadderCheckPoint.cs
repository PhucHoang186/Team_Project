using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Interactable
{

    public class LadderCheckPoint : MonoBehaviour
    {
        public Transform startPos;
        public Transform endPos;
        private Action<LadderCheckPoint, PlayerController> onEnterCheckPoint;
        private Action onExitCheckPoint;

        public void InitAction(Action<LadderCheckPoint, PlayerController> onEnterCheckPoint, Action onExitCheckPoint)
        {
            this.onEnterCheckPoint = onEnterCheckPoint;
            this.onExitCheckPoint = onExitCheckPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                onEnterCheckPoint?.Invoke(this, player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                onExitCheckPoint?.Invoke();
            }
        }

    }
}
