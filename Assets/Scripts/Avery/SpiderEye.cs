using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpiderEye : MonoBehaviour
{
    [SerializeField] Spider spider;
    [SerializeField] float exposeRate = 0.1f;
    bool isSeeingPlayer = false;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Dancer");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSeeingPlayer)
        {
            Player.GetComponent<PlayerHealth>().IncreaseMeter(exposeRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            StopCoroutine(PlayerRecovers());
            isSeeingPlayer = true;
            spider.SetMoving(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
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
}
