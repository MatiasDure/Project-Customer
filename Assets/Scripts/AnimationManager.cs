using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] AnimationClip[] animations;

        public enum Anim
        {
            Vet
        }

        public static AnimationManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        public void TriggerAnimation(Anim animationName, bool trigger)
        {
            for(int i = 0; i < animations.Length; i++)
            {
                if (animations[i].animation == animationName)
                {
                    animations[i].animator.SetBool(animations[i].parameterToTrigger,trigger);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

namespace Assets.Scripts
{
    [System.Serializable]
    public class AnimationClip
    {
        public Animator animator;
        public AnimationManager.Anim animation;
        public string parameterToTrigger;
    }
}