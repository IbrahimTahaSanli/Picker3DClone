using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetails : MonoBehaviour
{
    public uint ballCount { get; private set; } = 0;

    private Vector2 platformScale;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private Transform Env;

    [SerializeField] private MeshFilter PlatformMeshFilter;

    [SerializeField] private int inc = 3;

    [SerializeField] private AnimAbstractClass platformAnim;

    public enum PlatformStyle
    {
        Empty = 0,
        Sin = 1,
        Cos = 2,

       
        StartPlatform = 96,
        PhasePlatform = 97,
        FinishPlatform = 98,
        ShouldntBeRandomize = 99

    }

    [SerializeField] public PlatformStyle style;

    private void Awake()
    {


        //Const subtractions for padding to sides
        this.platformScale = new Vector2(this.PlatformMeshFilter.mesh.bounds.extents.x * 2 - 1, this.PlatformMeshFilter.mesh.bounds.extents.y * 2 - 2 );
        this.style = style == PlatformStyle.ShouldntBeRandomize? PlatformStyle.ShouldntBeRandomize:(PlatformStyle)Random.Range(0, 3);

        switch (this.style)
        {
            case PlatformStyle.Empty:
                break;
            case PlatformStyle.Sin:
                for (int i = 0; i < this.platformScale.x; i+=this.inc)
                    CreateEnvItem(EnvItems.BasicBall, i, Mathf.Sin(i * 5 / this.platformScale.y) * this.platformScale.y / 2);
                this.ballCount = (uint)(this.platformScale.x / this.inc + 1);
                break;
            case PlatformStyle.Cos:
                for (int i = 0; i < this.platformScale.x; i +=this.inc)
                    CreateEnvItem(EnvItems.BasicBall, i, Mathf.Cos(i * 5 / this.platformScale.y) * this.platformScale.y / 2);
                this.ballCount = (uint)(this.platformScale.x / this.inc + 1);
                break;

            default:
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
        retObj.transform.localPosition = new Vector3(x, 3, z);
        return retObj;
    }

    public void StartPlatform()
    {

    }
}
