using System;
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
    [SerializeField] private Vector3 thingyStartPosition;

    [SerializeField] private GameObject startCanvas;



    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject failCanvas;



    [SerializeField] private Transform thingy;

    [SerializeField] private Camera camera;
    [SerializeField] private float cameraXPos;

    [SerializeField] private int PhaseCount;
    [SerializeField] private int PlatformCount;


    private enum ControlScheme
    {
        StartScreen = 0,
        InGame = 1,
        InCheckpoint = 2,
        Fail = 3,
        InFinish = 4,
        Finished = 5,
    }

    private ControlScheme currentControlScheme;

    public void ThingyGetIntoCheckPoint(GameObject checkpoint)
    {
        this.currentControlScheme = ControlScheme.InCheckpoint;

        StartCoroutine(this.WaitForCheckpoint(checkpoint));
    }

    public void ThingyGetIntoFinish(GameObject checkpoint)
    {
        this.currentControlScheme = ControlScheme.InFinish;

        StartCoroutine(this.WaitForFinish(checkpoint));
    }

    private IEnumerator WaitForCheckpoint(GameObject checkpoint)
    {
        yield return new WaitForSeconds(5);
        PhasePlatformController controller = checkpoint.transform.parent.GetComponent<PhasePlatformController>();

        if (controller.IsPhasePassed)
        {
            checkpoint.transform.parent.GetComponent<PlatformDetails>().StartAnim();
            yield return new WaitForSeconds(1);
            this.currentControlScheme = ControlScheme.InGame;
        }
        else
        {
            failCanvas.SetActive(true);
            this.currentControlScheme = ControlScheme.Fail;
        }

    }

    private IEnumerator WaitForFinish(GameObject checkpoint)
    {
        yield return new WaitForSeconds(5);
        PhasePlatformController controller = checkpoint.transform.parent.GetComponent<PhasePlatformController>();

        if (controller.IsPhasePassed)
        {
            this.nextLevel();
            checkpoint.transform.parent.GetComponent<PlatformDetails>().StartAnim();
            yield return new WaitForSeconds(1);
            this.currentControlScheme = ControlScheme.InGame;
        }
        else
        {
            failCanvas.SetActive(true);
            this.currentControlScheme = ControlScheme.Fail;
        }

    }


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

            case ControlScheme.Fail:
#if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
#elif UNITY_ANDROID
                if (Input.touchCount > 0)
#endif
                {
                    this.restartGame();

                    this.currentControlScheme = ControlScheme.InGame;
                }
                break;

            default:
                break;
        }
    }

    private void startGame()
    {
        startCanvas.SetActive(false);
        inGameCanvas.SetActive(true);

        LevelCreator._instance.CreateLevel(this.PhaseCount,this.PlatformCount);
    }

    private void nextLevel()
    {
        LevelCreator._instance.CreateLevel(this.PhaseCount, this.PlatformCount);
    }

    private void restartGame()
    {
        CurrentPlatforms._instance.Restart();
        
        LevelCreator._instance.CreateStart();
        LevelCreator._instance.CreateLevel(this.PhaseCount, this.PlatformCount);
        
        this.thingy.position = this.thingyStartPosition;
        ScoreManager._instance.Restart();

        this.failCanvas.SetActive(false);
    }


}
