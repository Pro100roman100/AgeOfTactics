using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject blockPrefab;

    private GameObject buildingObject;
    private bool building = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnMouseDown");
        buildingObject = Instantiate(blockPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        buildingObject.transform.position = new(buildingObject.transform.position.x, buildingObject.transform.position.y, 0);
        building = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnMouseUp");
        building = false;
    }

    public void Update()
    {
        if (!building)
            return;
        buildingObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        buildingObject.transform.position = new Vector3(buildingObject.transform.position.x, buildingObject.transform.position.y, 0);
    }
}
