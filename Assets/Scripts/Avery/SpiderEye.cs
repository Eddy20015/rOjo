using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpiderEye : MonoBehaviour
{
    [SerializeField] Spider spider;
    [SerializeField] float exposeRate = 0.3f;
    bool isSeeingPlayer = false;
    GameObject Player;
    SpriteRenderer rend;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Dancer");
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSeeingPlayer)
        {
            Player.GetComponent<PlayerHealth>().IncreaseMeter(exposeRate);
        }

        if (isSeeingPlayer != spider.GetSeeing())
        {
            spider.SetSeeing(isSeeingPlayer);
            rend.enabled = isSeeingPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            StopCoroutine("WaitToDeactivateBeam");
            rend.enabled = true;
            StopCoroutine(PlayerRecovers());
            isSeeingPlayer = true;
            spider.SetMoving(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            StartCoroutine(WaitToDeactivateBeam(3f));
            spider.SetMoving(true);
            isSeeingPlayer = false;
            StartCoroutine(PlayerRecovers());
        }
    }

    public IEnumerator PlayerRecovers()
    {
        yield return new WaitForSeconds(Player.GetComponent<PlayerHealth>().GetExposureDecreaseDelay());
        Player.GetComponent<PlayerHealth>().ChangeStartDecreasingVar(true);
    }

    public IEnumerator WaitToDeactivateBeam(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        rend.enabled = false;
    }
}
