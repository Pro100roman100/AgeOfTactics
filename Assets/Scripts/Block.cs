using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer renderComponent;

    private void Awake()
    {
        renderComponent = GetComponent<SpriteRenderer>();
    }

    public void StartBreak()
    {
        StartCoroutine(Break());
    }
    public void StopBreak()
    {
        StopAllCoroutines();
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(.5f);

        Destroy(this.gameObject);
    }
}
