using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAITrigger : MonoBehaviour
{
    [SerializeField] GameObject child;
    [SerializeField] Transform destination;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] bool fadesAway = true;
    [SerializeField] float fadeSpeed = 1f;
    bool moving = false;

    public void Update()
    {
        if (moving)
        {
            print("MOVING");
            child.transform.position = Vector3.MoveTowards(child.transform.position, destination.position, movementSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(child.transform.position, destination.position) < 0.01f)
            child.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer") && child.activeInHierarchy)
        {
            child.transform.Rotate(0, 180f, 0);
            moving = true;
            if (fadesAway)
                StartCoroutine(FadeAway());
            gameObject.GetComponent<Collider2D>().enabled = false;
        }    
    }

    IEnumerator FadeAway()
    {
        SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
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
}
