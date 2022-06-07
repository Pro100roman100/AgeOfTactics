using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int prefabIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        BuildManager.StartBuilding(prefabIndex);
    }
}
