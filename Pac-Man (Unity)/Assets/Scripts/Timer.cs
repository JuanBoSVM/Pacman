using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public float time = 0.0f;
    public PacMan paco;
    public GameObject VictoryScreen;
    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time <=  0)
        {
            VictoryScreen.SetActive(true);
        }

        if (!paco.anim.GetBool("Alive"))
        {
            GameOverScreen.SetActive(true);
        }

        if (paco.anim.GetBool("Alive") && time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = "" + time.ToString("f0");
        }
    }
}
