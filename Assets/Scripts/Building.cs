using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2 size = Vector2.one;
    public Vector2 offset = Vector2.zero;
    public int cost = 100;
    //private Rigidbody2D rb;
    private Collider2D colider;
    public Color buildedColor = Color.white;
    public Color unbuildedColor;
    public Color cantBuildColor;

    [HideInInspector] public SpriteRenderer renderComponent;
    [HideInInspector] public bool builded = false;
    public virtual void OnBuild() 
    {
        colider.enabled = true;
        renderComponent.color = buildedColor;
        MatterManager.matter -= cost;
        builded = true;
    }
    public virtual void OnUnbuild() {
        if(!builded) return;
        MatterManager.matter += cost / 2;
    }
    private void Awake()
    {
        renderComponent = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();
        renderComponent.color = unbuildedColor;
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
        yield return new WaitForSeconds(.9f);

        OnUnbuild();
        Destroy(this.gameObject);
    }
}
