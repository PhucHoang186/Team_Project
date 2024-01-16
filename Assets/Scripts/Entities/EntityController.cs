using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Entity
{
    public abstract class EntityController : NetworkBehaviour
    {
        [SerializeField] protected HandleEntityInput handleInput;
        [SerializeField] protected HandleEntityMovement handleMovement;
    }
}
