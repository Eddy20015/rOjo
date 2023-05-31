using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsText : MonoBehaviour
{
    [SerializeField] List<GameObject> text;
    [SerializeField] Transform destination;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject blackScreen;
    int numOfTexts, currentIndex = 0;
    float fadeSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Cinematics();
        numOfTexts = text.Count;
        StartCoroutine(FadeAway());
        StartCoroutine(MoveTextAcrossScreen(text[currentIndex]));
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer spriteRenderer = blackScreen.GetComponent<SpriteRenderer>();
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeSpeed;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeAway()
    {
        SpriteRenderer spriteRenderer = blackScreen.GetComponent<SpriteRenderer>();
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fade to transparent

        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeSpeed;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveTextAcrossScreen(GameObject o)
    {
        if (currentIndex == 0) yield return new WaitForSeconds(fadeSpeed);

        while (o.transform.localPosition != destination.localPosition)
        {
            o.transform.localPosition = Vector3.MoveTowards(o.transform.localPosition, destination.localPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        currentIndex++;

        if (currentIndex < numOfTexts)
            StartCoroutine(MoveTextAcrossScreen(text[currentIndex]));
        else
        {
            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(fadeSpeed + 1f);
            GameStateManager.MainMenu();
        }
    }
}
