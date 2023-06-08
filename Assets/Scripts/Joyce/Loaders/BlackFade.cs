using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : SceneTransition
{
    [SerializeField] private Player2DMovement player;
    private void Awake()
    {
        player.enabled = false;
        screen.SetActive(true);
        Open();
    }

    public override void Open()
    {
        base.Open();
        StartCoroutine(FadeIn());
    }

    public override void Close()
    {
        base.Close();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(openLength);
        screen.SetActive(false);
        player.enabled = true;
    }
    private IEnumerator FadeOut()
    {
        screen.SetActive(true);
        yield return new WaitForSeconds(closeLength);
    }

}
