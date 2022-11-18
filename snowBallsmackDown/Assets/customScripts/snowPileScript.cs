using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowPileScript : MonoBehaviour
{
    public float timeToActivate = 4f;
    playerController playerCTRL; // reference to player controller script 
    [SerializeField] SpriteRenderer spr;// refernce to our sprite renderer
    [SerializeField] bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        playerCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // maybe replace with triggerstay so it seems like ur making picking up snow balls 
    {
        if (collision.gameObject.tag == "Player" && isActive)
        {
            if (playerCTRL.pIndex == 2) playerCTRL.playerTwoSBalls = 5;

            spr.enabled = false;
            isActive = false;
            StartCoroutine(ActiveClock());
            Debug.Log("Refil P2 snow");
        }

        if (collision.gameObject.tag == "PlayerTwo" && isActive)
        {
           if(playerCTRL.pIndex == 1) playerCTRL.playerOneSBalls = 5;
           
            spr.enabled = false;
            isActive = false;
            StartCoroutine(ActiveClock()); 

        }
    
    }

    IEnumerator ActiveClock()
    {
        yield return new WaitForSeconds(timeToActivate);
        spr.enabled = true;
        isActive = true;
    }
}
