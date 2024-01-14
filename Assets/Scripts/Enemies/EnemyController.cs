using AI;
using UnityEngine;

namespace Entity
{

    public class EnemyController : EntityController
    {
        void Update()
        {
            handleMovement.HandleMovement(handleInput.EntityInput);
        }
    }
}
