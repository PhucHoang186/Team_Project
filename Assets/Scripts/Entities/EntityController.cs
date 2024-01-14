using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public abstract class EntityController : MonoBehaviour
    {
        [SerializeField] protected HandleEntityInput handleInput;
        [SerializeField] protected HandleEntityMovement handleMovement;
    }
}
