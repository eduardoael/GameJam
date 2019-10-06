using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;

public class Countdown : MonoBehaviour
{
    public int timeLeft = 60;
    public TMP_Text countdown;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoseTime");
        //Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = ("" + timeLeft);
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (timeLeft <= 0) { gameController.GameOver(); }
        }
       
    }

}
