using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatformAnim : AnimAbstractClass
{
    private Coroutine animCoroutine;

    [SerializeField] Vector3 EndAnimVector;

    public override void PlayAnim()
    {
        this.animCoroutine = StartCoroutine(this.closeAnim());

    }

    private IEnumerator closeAnim()
    {
        Vector3 firstVector = this.transform.position;

        for (float current = 0.0f; current < 1.0f; current += Time.deltaTime)
        {
            this.transform.position = Vector3.Lerp(firstVector, EndAnimVector, current);
            yield return new WaitForEndOfFrame();
        }

        this.animCoroutine = null;

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
