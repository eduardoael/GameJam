using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            print("Level Finished!");
            gameController.TerminalReached();
    }
}
