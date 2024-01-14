using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class HandleMeleeAttack : MonoBehaviour
    {
        [SerializeField] Vector3 castSize;
        [SerializeField] float castDistance;
        [SerializeField] LayerMask attackLayer;
        protected float damage;
        protected Action onAttackHit;
        protected RaycastHit[] hits = new RaycastHit[10];

        public void Init(Action onAttackHit, float damage)
        {
            this.onAttackHit = onAttackHit;
            this.damage = damage;
        }

        // TO-FIX: this is a cheap way to handle attack check
        public void CheckAttack()
        {
            Physics.BoxCastNonAlloc(transform.position, castSize * 0.5f, transform.forward, hits, Quaternion.identity, castDistance, attackLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider == null)
                    continue;

                if (hits[i].collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(damage);
                    onAttackHit?.Invoke();
                }
            }
        }
    }
}
