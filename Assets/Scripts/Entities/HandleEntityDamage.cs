using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class HandleEntityDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] float stunTime;
        [SerializeField] float knockBackTime;
        [SerializeField] float knockBackForce;
        [SerializeField] float maxHealth;
        [SerializeField] GameObject hitVfx;
        protected float currentDelayTime;
        protected float totalDelayTime;
        protected float currentHealth;
        protected bool isKnockback;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            HandleEntityAnimation.ON_PLAY_ANIM?.Invoke(AnimationName.Hit, gameObject, 0.1f);
            PlayHitVfx();
            CheckHealth(amount);
        }

        protected void PlayHitVfx()
        {
            hitVfx.SetActive(false);
            hitVfx.SetActive(true);
        }

        protected void CheckHealth(float amount)
        {
            currentHealth -= amount;

            HandleEntityMovement.ON_TOGGLE_MOVEMENT?.Invoke(false, gameObject);
            if (currentHealth > 0)
            {
                Knockback();
            }
            else
            {
                Destroy();
            }
        }

        protected void Destroy()
        {
            HandleEntityAnimation.ON_PLAY_ANIM?.Invoke(AnimationName.Destroy, gameObject, 0.1f);
        }

        protected void Knockback()
        {
            currentDelayTime = 0f;
            isKnockback = true;
            totalDelayTime = stunTime + knockBackTime;
        }

        void Update()
        {
            if (!isKnockback)
                return;

            if (currentDelayTime >= totalDelayTime)
            {
                isKnockback = false;
                // after knockback finish, wait for a little more before continue to move
                HandleEntityMovement.ON_TOGGLE_MOVEMENT?.Invoke(true, gameObject);
            }
            else
            {
                if (currentDelayTime < knockBackTime)
                    transform.position = transform.position - (transform.forward * knockBackForce * Time.deltaTime);

                currentDelayTime += Time.deltaTime;
            }
        }
    }
}

public interface IDamageable
{
    public void TakeDamage(float amount);
}
