using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float p1Health = 100f;
    public float p2Health = 100f;

    [SerializeField] public Image P1HealthBar;
    [SerializeField] public Image P2HealthBar;

    [SerializeField] public bool isJoining;

    // Start is called before the first frame update
    void Start()
    {
        isJoining = false;
    }

    // Update is called once per frame
    void Update()
    {
        P1HealthBar.fillAmount = p1Health * 0.01f;
        P2HealthBar.fillAmount = p2Health * 0.01f;
    }
}
