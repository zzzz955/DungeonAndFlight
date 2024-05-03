using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    [SerializeField]
    public bool isUpgrade = false;

    void Start()
    {
        // Animator 컴포넌트를 가져옵니다.
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 특정 조건에 따라 다른 애니메이션을 재생합니다.
        if (isUpgrade) 
        {
            PlayAnimation("Run");
        } else {
            PlayAnimation("Walk");
        }
        // 여러 가지 다른 조건에 따라 원하는 애니메이션을 재생할 수 있습니다.
    }

    public void PlayAnimation(string animationName)
    {
        // Animator Controller에서 animationName에 해당하는 애니메이션을 재생합니다.
        animator.Play(animationName);
    }

    public void movemotionUpgrade() {
        isUpgrade = true;
    }
}
