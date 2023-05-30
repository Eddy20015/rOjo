using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsText : MonoBehaviour
{
    [SerializeField] List<GameObject> text;
    [SerializeField] Transform destination;
    [SerializeField] float moveSpeed = 2f;
    int numOfTexts, currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Cinematics();
        numOfTexts = text.Count;
        StartCoroutine(MoveTextAcrossScreen(text[currentIndex]));
    }

    IEnumerator MoveTextAcrossScreen(GameObject o)
    {
        while (o.transform.localPosition != destination.localPosition)
        {
            o.transform.localPosition = Vector3.MoveTowards(o.transform.localPosition, destination.localPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        currentIndex++;

        if (currentIndex < numOfTexts)
            StartCoroutine(MoveTextAcrossScreen(text[currentIndex]));
        else
            GameStateManager.MainMenu();
    }
}
