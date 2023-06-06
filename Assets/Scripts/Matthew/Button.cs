using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private bool two_button;

    [SerializeField]
    private Button other_button;

    public bool collision;
    
    [SerializeField]
    private bool timed;
    
    [SerializeField]
    private int time;

    [SerializeField]
    private Sprite button_pressed;

    [SerializeField]
    private Sprite button_unpressed;

    [SerializeField]
    private SpriteRenderer button_sprite;

    
    private void ButtonPress(Sprite sprite)
    {
        button_sprite.sprite = sprite;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (two_button)
        {
            if (other.gameObject.CompareTag("Dancer"))
            {
                collision = true;
                ButtonPress(button_pressed);

                if (collision && other_button.collision)
                {
                    door.SetActive(false);
                    StartCoroutine("Timer");
                }
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Dancer"))
            {
                ButtonPress(button_pressed);
                door.SetActive(false);
                StartCoroutine("Timer");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dancer"))
        {
            ButtonPress(button_unpressed);
        }
    }

    IEnumerator Timer()
    {
        if (time > 0)
        {
            yield return new WaitForSeconds(time);

            door.SetActive(true);

            collision = false;
        }
    }
}
