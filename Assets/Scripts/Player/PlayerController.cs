using System.Collections;
using System.Collections.Generic;
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
            handleAttack.Init(handleInput.EntityInput);
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
