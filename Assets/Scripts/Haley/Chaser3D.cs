using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Chaser3D : MonoBehaviour
{
    [Header("General Control Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private float speedOffset;

    // FOR TESTING, CHANGE AS NEEDED
    [Header("On Death Variables")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private VideoPlayer cutscenePlayer;
    [SerializeField] private GameObject cutsceneImage;
    private Player3DMovement pMove;

    private Vector3 moveDirection;

    private Rigidbody rb;
    private float speed;
    private float drag;

    private void Awake()
    {
        deathScreen.SetActive(false);
        cutsceneImage.SetActive(false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        pMove = player.GetComponent<Player3DMovement>();
        speed = pMove.getSpeed();
        drag = pMove.getDrag();
    }

    void Update()
    {
        moveDirection = pMove.getMoveDirection();
        //moveDirection = transform.forward;
        rb.drag = drag;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * (speed - speedOffset), ForceMode.Acceleration);
    }

    // Getting cause by chaser, CHANGE AS NEEDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            pMove.enabled = false;
            StartCoroutine(PlayCutscene());
        }

        /*print("trigger on " + other.name);
        Player3DMovement p = other.GetComponentInParent<Player3DMovement>();
        if(p != null) {
            p.enabled = false; // Stop them from moving
            text.SetActive(true);
            gameObject.SetActive(false);
        }*/
    }

    private IEnumerator PlayCutscene()
    {
        cutsceneImage.SetActive(true);
        cutscenePlayer.Play();
        yield return new WaitForSeconds((float)cutscenePlayer.clip.length);
        cutscenePlayer.Stop();
        GameStateManager.Pause();
        cutsceneImage.SetActive(false);
        deathScreen.SetActive(true);
    }
}
