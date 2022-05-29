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
        Quaternion firstVector = Quaternion.Euler( this.transform.localRotation.eulerAngles);
        Quaternion endAnimVector = Quaternion.Euler(this.EndAnimVector);

        for (float current = 0.0f; current < 1.0f; current += Time.deltaTime)
        {
            this.transform.localRotation = Quaternion.Lerp(firstVector, endAnimVector, current);
            yield return new WaitForEndOfFrame();
        }

        this.animCoroutine = null;

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


}
