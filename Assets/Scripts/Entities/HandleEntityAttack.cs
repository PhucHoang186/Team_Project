using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace Entity
{
    public class HandleEntityAttack : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float attackDelay;
        [SerializeField] protected HandleMeleeAttack meleeAttack;
        protected float currentDelayAttack;
        protected EntityInput entityInput;

        public void Init(EntityInput entityInput)
        {
            this.entityInput = entityInput;
            meleeAttack.Init(OnAttackHit, damage);
        }

        void Update()
        {
            if (currentDelayAttack > 0)
            {
                currentDelayAttack -= Time.deltaTime;
            }
            else
            {
                if (entityInput != null && !entityInput.FinishAttack)
                {
                    entityInput.FinishAttack = true;
                }
            }
        }

        public void HandleAttack(EntityInput entityInput)
        {

            if (entityInput.StartAttack && currentDelayAttack <= 0)
            {
                entityInput.FinishAttack = false;
                Attack();
            }
        }

        protected void Attack()
        {
            HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Attack, gameObject, 0.1f);
            currentDelayAttack = HandleEntityAnimation.ON_PLAY_ANIM(AnimationName.Attack, gameObject, 0.1f);
        }

        protected void OnAttackHit()
        {
            Debug.Log("Hit");
            CameraController.Instance.OnShakeCamera();
        }
    }
}
