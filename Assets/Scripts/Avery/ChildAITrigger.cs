using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAITrigger : MonoBehaviour
{
    [SerializeField] GameObject child;
    [SerializeField] AnimationClip childAnimation;
    float animLength;
    Animator childAnimator;

    private void Start()
    {
        childAnimator = child.GetComponent<Animator>();
        animLength = childAnimation.length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer") && child.activeInHierarchy)
        {
            childAnimator.Play(childAnimation.name);
            StartCoroutine("DisableAfterSeconds");
        }    
    }

    IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(animLength);
        child.SetActive(false);
    }
}
