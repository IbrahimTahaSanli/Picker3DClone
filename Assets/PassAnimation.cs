using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAnimation : AnimAbstractClass
{
    private Coroutine animCoroutine;

    [SerializeField] Vector3 EndAnimVector;

    public override void PlayAnim()
    {
        this.animCoroutine = StartCoroutine(this.openAnim());

    }

    private IEnumerator openAnim()
    {
        Vector3 firstVector = this.transform.rotation.eulerAngles;

        for (float current = 0.0f; current < 1.0f; current += Time.deltaTime)
        {
            this.transform.eulerAngles = Vector3.Lerp(firstVector, EndAnimVector, current);
            yield return new WaitForEndOfFrame();
        }

        this.animCoroutine = null;

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


}
