using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;
    public Vector2 offset = Vector2.zero;
    public int cost = 100;
    //private Rigidbody2D rb;
    private Collider2D colider;
    public Color buildedColor = Color.white;
    public Color unbuildedColor;
    public Color cantBuildColor;
    public bool placeOnGround = false;

    [HideInInspector] public GameObject tagConnection;
    [HideInInspector] public SpriteRenderer[] renderComponent;
    [HideInInspector] public bool builded = false;
    private Coroutine courutineBreak;

    public void ChangeColor(Color color)
    {
        foreach(SpriteRenderer renderer in renderComponent)
        {
            renderer.color = color;
        }
    }

    public virtual void OnBuild() 
    {
        colider.enabled = true;
        ChangeColor(buildedColor);
        MatterManager.player1matter -= cost;
        builded = true;
        tagConnection = GameObject.Find("Player_1_Base");
        transform.gameObject.tag = tagConnection.tag ;
    }

    public void Destroy()
    {
        Vector2[] output = BuildManager.DrawGridSection((Vector3Int)size, transform.position);

        foreach (Vector2 sell in output)
        {
            switch (BuildManager.grid[(int)sell.x, (int)sell.y])
            {
                case SellType.GroundFilled:
                    BuildManager.grid[(int)sell.x, (int)sell.y] = SellType.GroundBroken;
                    break;
                case SellType.AirFilled:
                    BuildManager.grid[(int)sell.x, (int)sell.y] = SellType.GroundBroken;
                    break;
            }
        }
    }
    public virtual void OnUnbuild() {
        if(!builded) return;
        MatterManager.player1matter += cost / 2;

        Vector2[] output = BuildManager.DrawGridSection((Vector3Int)size, transform.position);

        foreach (Vector2 sell in output)
        {
            switch (BuildManager.grid[(int)sell.x, (int)sell.y])
            {
                case SellType.GroundFilled:
                    BuildManager.grid[(int)sell.x, (int)sell.y] = SellType.GroundEmpty;
                    break;
                case SellType.AirFilled:
                    BuildManager.grid[(int)sell.x, (int)sell.y] = SellType.AirEmpty;
                    break;
            }
        }
    }
    private void Awake()
    {
        renderComponent = GetComponentsInChildren<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();
        ChangeColor(unbuildedColor);
    }
    public void StartBreak()
    {
        courutineBreak = StartCoroutine(Break());
    }
    public void StopBreak()
    {
        if (courutineBreak == null)
            return;
        StopCoroutine(courutineBreak);
    }
    IEnumerator Break()
    {
        yield return new WaitForSeconds(.9f);

        OnUnbuild();

        Destroy(this.gameObject);
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector3 localOffset = Vector3.zero;
        if (size.x % 2 == 0)
            localOffset.x += .5f;
        if (size.y % 2 == 0)
            localOffset.y += .5f;

        Vector3 position = new(transform.position.x + offset.x + localOffset.x, transform.position.y + offset.y + localOffset.y);
        Gizmos.DrawWireCube(position, BuildManager.DrawGridSection(new(size.x, size.y)));
    }
}