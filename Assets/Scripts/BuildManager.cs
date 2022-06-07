using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager builder = null;

    private RaycastHit2D hit;
    private RaycastHit2D oldHit;

    private void Awake()
    {
        if (builder == null)
            builder = this;
    }

    private void Update()
    {
        //Destroying building
        DestroyBuilding();
    }

    private bool DestroyBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null && oldHit.collider == null)
            return false;
        else
        {
            if(hit.collider == null && oldHit.collider != null)
            {
                if (oldHit.transform.TryGetComponent(out Block blockComponent)) blockComponent.StopBreak();
            }else if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.TryGetComponent(out Block blockComponent)) blockComponent.StartBreak();
            }else if (Input.GetMouseButtonUp(0))
            {
                if (hit.transform.TryGetComponent(out Block blockComponent)) blockComponent.StopBreak();
            }

            oldHit = hit;

            return true;
        }
    }
}
