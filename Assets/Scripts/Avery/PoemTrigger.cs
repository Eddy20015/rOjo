using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoemTrigger : MonoBehaviour
{
    [SerializeField] TextMeshPro poemText;
    [SerializeField] float timeStaysVisible = 3f;
    [SerializeField] float visibilitySpeedChange = 1f;
    bool hasBeentriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        poemText.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer") && !hasBeentriggered)
        {
            StartCoroutine(FadeTextToFullAlpha(visibilitySpeedChange, poemText));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshPro i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        yield return new WaitForSeconds(timeStaysVisible);
        StartCoroutine(FadeTextToZeroAlpha(visibilitySpeedChange, poemText));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshPro i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
