using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager _instance;



    [SerializeField] private GameObject startCanvas;

    private enum ControlScheme
    {
        StartScreen = 0,
    }

    private ControlScheme currentControlScheme = ControlScheme.StartScreen;

    
    void Start()
    {
        _instance = this;


    }

    private void Update()
    {
        switch (this.currentControlScheme)
        {
            case ControlScheme.StartScreen:
#if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
#elif UNITY_ANDROID
                if (Input.touchCount > 0)
#endif
                    this.startGame();
                break;

        }
    }


    private void startGame()
    {
        startCanvas.SetActive(false);

        LevelCreator._instance.CreateLevel(5);
    }

}
