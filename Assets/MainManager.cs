using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager _instance;

    public float inputPosInX { get; private set; }


    [SerializeField] private float thingyGeneralSpeed;
    [SerializeField] private float thingyMoveAreaWidth;
    [SerializeField] private float thingyXSpeed;

    [SerializeField] private GameObject startCanvas;
    [SerializeField] private Transform thingy;

    [SerializeField] private Camera camera;
    [SerializeField] private float cameraXPos;

    private enum ControlScheme
    {
        StartScreen = 0,
        InGame = 1,
    }

    private ControlScheme currentControlScheme;

    
    void Start()
    {
        _instance = this;

        this.currentControlScheme = ControlScheme.StartScreen;
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
                {
                    this.startGame();

                    this.currentControlScheme = ControlScheme.InGame;
                }
                break;

            case ControlScheme.InGame:
#if UNITY_EDITOR
                if (Input.GetMouseButton(0))
                    if(Input.mousePosition.x >= 0 &&  Input.mousePosition.x < Screen.width)
                        this.inputPosInX = -(Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2);
#elif UNITY_ANDROID
                if (Input.touchCount > 0)
                    if (Input.GetTouch(0).position.x >= 0 && Input.GetTouch(0).position.x < Screen.width)
                        this.inputPosInX = -(Input.GetTouch(0).position.x - Screen.width / 2) / (Screen.width / 2);
#endif
                //this.thingy.GetComponent<Rigidbody>().velocity = new Vector3( this.thingyXSpeed, 0, thingyMoveAreaWidth * this.inputPosInX);
                this.thingy.position = Vector3.Lerp(this.thingy.position, new Vector3(this.thingy.position.x + this.thingyXSpeed, this.thingy.position.y, thingyMoveAreaWidth * this.inputPosInX), Time.deltaTime * this.thingyGeneralSpeed);
                this.camera.transform.position = new Vector3(this.thingy.position.x + this.thingyXSpeed - this.cameraXPos, this.camera.transform.position.y, this.camera.transform.position.z);
                break;
        }
    }


    private void startGame()
    {
        startCanvas.SetActive(false);

        LevelCreator._instance.CreateLevel(3,5);
    }

}
