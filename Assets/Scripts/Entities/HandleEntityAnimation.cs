using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{

    public enum AnimationName
    {
        Idle,
        Run,
        Attack,
        Jump_Start,
        Jump_End,
        Hit,
        Destroy,
    }

    public class HandleEntityAnimation : MonoBehaviour
    {
        public static Func<AnimationName, GameObject, float, float> ON_PLAY_ANIM;
        protected Animator anim;

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            ON_PLAY_ANIM += OnPlayAnim;
        }

        void OnDestroy()
        {
            ON_PLAY_ANIM -= OnPlayAnim;
        }

        protected float OnPlayAnim(AnimationName animName, GameObject entity, float transitionTime)
        {
            if (entity != gameObject)
                return 0f;

            anim.CrossFadeInFixedTime(animName.ToString(), transitionTime);
            return anim.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}
