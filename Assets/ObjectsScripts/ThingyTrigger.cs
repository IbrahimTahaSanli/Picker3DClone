using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case ("Phase"):
                MainManager._instance.ThingyGetIntoCheckPoint(other.gameObject);
                break;
            case ("Finish"):
                MainManager._instance.ThingyGetIntoFinish(other.gameObject);
                break;

            case ("NextLevel"):
                other.gameObject.SetActive(false);
                CurrentPlatforms._instance.DestroyLevel(1);
                break;
            default:
                Debug.Log(other.tag);
                break;
        }
    }
}
