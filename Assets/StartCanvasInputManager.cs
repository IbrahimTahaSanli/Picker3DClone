using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvasInputManager : MonoBehaviour
{

    public delegate void GameStarted();
    [SerializeField] GameStarted _gameStarted;


    // Update is called once per frame
    void Update()
    {

    }
}
