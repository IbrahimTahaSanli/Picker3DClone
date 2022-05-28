using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainManager._instance.ThingyGetIntoCheckPoint(other.gameObject);
    }
}
