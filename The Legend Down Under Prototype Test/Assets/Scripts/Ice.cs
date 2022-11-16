using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public PlayerMovement player;
    public int interpolationFrameCount = 100;
    private int elapsedFrames;
    private bool onIce = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onIce)
        {
            player.transform.position += player.change * 0.1f;
            /*
            //float velx = Mathf.Max(player.velocity.x - Time.deltaTime * player.speed, 0);
            //float vely = Mathf.Max(player.velocity.y - Time.deltaTime * player.speed, 0);
            player.velocity = Vector2.Lerp(player.velocity, Vector2.zero, Time.deltaTime * player.speed);
            elapsedFrames++;
            if (player.velocity == Vector2.zero)
            {
                elapsedFrames = 0;
            }
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                player.change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * player.speed;
                player.change += new Vector3(player.velocity.x, player.velocity.y);
                player.transform.position += player.change * 0.001f;
            }
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                player.change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * player.speed;
                player.change -= new Vector3(player.velocity.x, player.velocity.y);
                player.transform.position -= player.change * 0.001f;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onIce = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onIce = false;
    }
}
