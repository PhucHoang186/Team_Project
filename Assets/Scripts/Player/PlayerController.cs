using System.Collections;
using System.Collections.Generic;
using Controller;
using Interactable;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(HandleEntityInput), typeof(HandleEntityMovement))]
    public class PlayerController : EntityController
    {
        [SerializeField] protected HandleEntityAttack handleAttack;

        void Start()
        {
            CameraController.Instance.SetCurrentCam(CamType.PlayerCam, follow: transform);
            handleAttack.Init(handleInput.EntityInput);
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                this.enabled = false;
                Destroy(this);
            }

        }

        void Update()
        {
            handleMovement.HandleMovement(handleInput.EntityInput);
            handleAttack.HandleAttack(handleInput.EntityInput);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Ladder>(out var ladder))
            {

            }
        }
    }
}
