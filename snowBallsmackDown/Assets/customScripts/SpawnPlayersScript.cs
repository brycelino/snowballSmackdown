using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayersScript : MonoBehaviourPunCallbacks
{
    public GameObject player1Prefab;// reference to game object
    public Transform p1Spawn;
   
    GameManager gm;

    public float SpawnTime = 1f;
    float timer;
    bool HasPlayerSpawned = false;
    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnTime)
        {
            if (!HasPlayerSpawned)
            {
                PhotonNetwork.Instantiate(player1Prefab.name, p1Spawn.position, Quaternion.identity);
                HasPlayerSpawned = true;
            }

            timer = 0;
        }
        
        
      
    }

    
}
