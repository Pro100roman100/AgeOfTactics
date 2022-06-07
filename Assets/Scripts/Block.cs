using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void StartBreak()
    {
        Debug.Log("destroying");
        StartCoroutine(Break());
    }
    public void StopBreak()
    {
        Debug.Log("stop destroying");
        StopAllCoroutines();
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}
