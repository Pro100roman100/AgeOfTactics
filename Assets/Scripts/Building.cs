using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2 size;
    public Vector2 offset;
    [HideInInspector] public SpriteRenderer renderComponent;

    public void OnBuild() { return; }    public void OnDestroy() { return; }
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
