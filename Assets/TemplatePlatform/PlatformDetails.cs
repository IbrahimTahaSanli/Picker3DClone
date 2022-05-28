using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetails : MonoBehaviour
{
    public uint ballCount { get; private set; } = 0;

    private Vector2 platformScale;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private Transform Env;

    public enum PlatformStyle
    {
        Empty = 0,
        Sin = 1,
        Cos = 2,


    }

    public PlatformStyle style;

    private void Awake()
    {
        //Const subtractions for padding to sides
        this.platformScale = new Vector2(this.transform.localScale.x - 1, this.transform.localScale.z - 2 );
        this.style = (PlatformStyle)Random.Range(0, 3);
        Debug.Log(this.style);
        switch (this.style)
        {
            case PlatformStyle.Empty:
                break;
            case PlatformStyle.Sin:
                for (int i = 0; i < this.platformScale.x; i++)
                    CreateEnvItem(EnvItems.BasicBall, i, Mathf.Sin(i*5/this.platformScale.y) * this.platformScale.y / 2);
                break;
            case PlatformStyle.Cos:
                for (int i = 0; i < this.platformScale.x; i++)
                    CreateEnvItem(EnvItems.BasicBall, i, Mathf.Cos(i * 5 / this.platformScale.y) * this.platformScale.y / 2);
                break;

            default:
                Debug.Log("Random Out");
                break;
        }
    }

    public enum EnvItems{
        BasicBall = 0,
    }

    private GameObject CreateEnvItem(EnvItems item, float x, float z)
    {
        GameObject retObj;
        switch (item)
        {
            case (EnvItems.BasicBall):
                retObj = Instantiate(BallPrefab);
                break;
            default:
                return null;
        }

        retObj.transform.SetParent(Env);
        retObj.transform.localPosition = new Vector3(x / this.platformScale.x, 3, z / (this.platformScale.y + 5));
        return retObj;
    }

    public void StartPlatform()
    {

    }
}
