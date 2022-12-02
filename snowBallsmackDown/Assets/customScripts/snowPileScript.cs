using System.Collections;
using UnityEngine;

public class SnowPileScript : MonoBehaviour
{
    public float timeToActivate = 4f;
    PlayerController playerCTRL; // reference to player controller script 
    [SerializeField] SpriteRenderer spr;// refernce to our sprite renderer
    [SerializeField] bool isActive;

    // Start is called before the first frame update
    void Awake()
    {
        //playerCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spr = GetComponent<SpriteRenderer>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) // maybe replace with triggerstay so it seems like ur making picking up snow balls 
    {
        if (collision.gameObject.tag == "Player" && isActive)
        {
           PlayerController pCTRL = collision.GetComponent<PlayerController>();
            pCTRL.playerOneSBalls = 5;
            Debug.Log("Refil p1 snow");

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
