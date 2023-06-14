using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Chaser3D : EndingUI
{
    [Header("General Control Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private float speedOffset;
    private Player3DMovement pMove;

    [Header("Chaser Video")]
    [SerializeField] private VideoClip handsVid;
    [SerializeField] private GameObject redScreenImage;

    [Header("Eye Flash")]
    [SerializeField] private GameObject eyeFlashScreen;
    [SerializeField] private Material paleChroma;
    private Color matStartCol;

    [Header("Audio")]
    private Vector3 moveDirection;

    private Rigidbody rb;
    private float speed;
    private bool moving = true;
    private float drag;

    private float startDist;

    public AK.Wwise.Event ChaseMusic;
    public AK.Wwise.Event playDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        pMove = player.GetComponent<Player3DMovement>();
        speed = pMove.getSpeed();
        drag = pMove.getDrag();
        moving = true;

        startDist = DistanceFromPlayer();
        eyeFlashScreen.SetActive(true);
        matStartCol = paleChroma.GetColor("_TintColor");

        ChaseMusic.Post(gameObject);
    }

    void Update()
    {
        moveDirection = pMove.getMoveDirection();
        //moveDirection = transform.forward;
        rb.drag = drag;

        if(moving)
        {
            SetVidTransparency();
        }
    }

    private void SetVidTransparency()
    {
        float currDist = DistanceFromPlayer();
        Color color = paleChroma.GetColor("_TintColor");
        color.a = 1 - Mathf.Clamp01(currDist / startDist);
        paleChroma.SetColor("_TintColor", color);
    }

    private void FixedUpdate()
    {
        if(moving)
            rb.AddForce(moveDirection.normalized * (speed - speedOffset), ForceMode.Acceleration);
    }

    // Getting cause by chaser, CHANGE AS NEEDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            Halt();
            pMove.enabled = false;
            StartCoroutine(PlayHands());
        }

        /*print("trigger on " + other.name);
        Player3DMovement p = other.GetComponentInParent<Player3DMovement>();
        if(p != null) {
            p.enabled = false; // Stop them from moving
            text.SetActive(true);
            gameObject.SetActive(false);
        }*/
    }

    protected override void ToggleUI(bool on = false)
    {
        redScreenImage.SetActive(false);
        base.ToggleUI(on);
    }

    public void Halt()
    {
        moving = false;
    }

    private IEnumerator PlayHands()
    {
        playDeath.Post(gameObject);

        vPlayer.targetTexture.Release();
        GameStateManager.Cinematics();
        vPlayer.clip = handsVid;
        vPlayer.isLooping = false;
        vPlayer.Prepare();
        vPlayer.Play();
        yield return new WaitUntil(() => vPlayer.isPrepared);
        yield return new WaitForEndOfFrame();
        redScreenImage.SetActive(true);

        yield return new WaitForSeconds((float)handsVid.length);

        redScreenImage.SetActive(false);
        vPlayer.Stop();
        PlayMenu();
    }

    public float DistanceFromPlayer()
    {
        float currDistance = Mathf.Abs(transform.position.z - player.transform.position.z);

        AkSoundEngine.SetRTPCValue("MonsterDistance", currDistance);

        return currDistance;
    }

    private void OnDestroy()
    {
        paleChroma.SetColor("_TintColor", matStartCol);
    }
}
