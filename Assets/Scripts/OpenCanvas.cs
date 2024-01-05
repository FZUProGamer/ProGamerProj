using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCanvas : MonoBehaviour
{
    public GameObject canvas;
    public bool canOpen = true;

    private void OnEnable()
    {
        EventHander.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHander.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canOpen = gameState != GameState.Pause;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            canvas.SetActive(!canvas.activeInHierarchy);
        }
    }
}
