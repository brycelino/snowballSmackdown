using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallScript : MonoBehaviour
{
    SpriteRenderer spr; // referecne to our sprite
    Rigidbody2D rb; // reference to our rigid body
    GameManager gameManager; // reference to our gamemanager

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spr = GetComponent<SpriteRenderer>();// our reference of the sprite render will get the componnent of the renderer 
        rb = GetComponent<Rigidbody2D>(); // our reference of the rigid body will get the component of the rigid body 
        Destroy(gameObject, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0) spr.flipX = false; // if moving to the right dont flip 
        else spr.flipX = true; // else flip to the opposite direction
    }

    //maybe make the usder gain more snowballs as long as they stay on the pile 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") Destroy(gameObject);
        

        if (collision.gameObject.tag == "Player")
        {
            PlayerController pCTRL = collision.GetComponent<PlayerController>();
            if (pCTRL.pIndex == 1) gameManager.p1Health -= 10;
           
            Debug.Log("Hit ConnectP1");
            Destroy(gameObject);
            if (pCTRL.pIndex == 1 && gameManager.p1Health == 0)
            {
                Destroy(pCTRL.gameObject);
            }
        }

        if (collision.gameObject.tag == "PlayerTwo")
        {
            PlayerController pCTRL = collision.GetComponent<PlayerController>();
            
            if (pCTRL.pIndex == 2) gameManager.p2Health -= 10;
            Debug.Log("Hit ConnectP2");
            Destroy(gameObject);
            if(pCTRL.pIndex == 2 && gameManager.p2Health == 0)
            {
                Destroy(pCTRL.gameObject);
            }
        }
    }
}
