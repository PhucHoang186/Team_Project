using System.Collections;
using System.Collections.Generic;
using AI;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public class HandleEnemyInput : HandleEntityInput
    {
        // [SerializeField]  AIEnemy aIEnemy;
        [SerializeField] EnemyAI enemyAI;

        public override void GetInput()
        {
            // move
            EntityInput.moveVec = GetMovementInput();
            // attack
            EntityInput.isInstantAttackPressed = GetInstantAttackInput();
        }


        protected override Vector3 GetMovementInput()
        {
            return enemyAI.GetEnemyAIInput().moveVec;
        }
    }
}
