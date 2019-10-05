﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{

    public Canvas collectiblesPrompt;
    private bool isCollectible;

    private void Start()
    {
        collectiblesPrompt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        collectiblesPrompt.gameObject.SetActive(true);
        isCollectible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collectiblesPrompt.gameObject.SetActive(false);
        isCollectible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Collect") && isCollectible)
        {
            collectiblesPrompt.gameObject.SetActive(false);
            Debug.Log("DESTROYED Clipboard");
            Destroy(gameObject);
        }
    }
}
