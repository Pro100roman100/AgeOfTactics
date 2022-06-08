using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager builder = null;
    public static Building[,] building;
    public static bool isBuilding;
    public static Vector3 mousePosition;

    [Header("Grid")]
    [SerializeField] private Vector2 gridOffset;
    [SerializeField] private float sellSize = 1f;
    [SerializeField] private Vector2Int buildingArea = new(5, 10);
    [Header("Prefabs")]
    [SerializeField] private GameObject[] prefabs;
    [Header("Masks")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask UIMask;
    [Header("GUI")]
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    private Building buildingObject = null;
    private RaycastHit2D hit;
    private RaycastHit2D oldHit;

    private void Awake()
    {
        if (builder == null)
            builder = this;
        building = new Building[buildingArea.x, buildingArea.y];
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isBuilding)
        {
            if (Input.GetMouseButtonUp(0))
                EndBuilding();
            else
                Building();
        }

        //Destroying building
        if(!isBuilding)
            DestroyBuilding();
    }
    public static Vector3 FitToGrid(Vector3 original)
    {
        Vector3 output;

        output = new(
            Mathf.Floor(original.x / builder.sellSize) * builder.sellSize + builder.gridOffset.x,
            Mathf.Floor(original.y / builder.sellSize) * builder.sellSize + builder.gridOffset.y,
            0);

        return output;
    }
    public static void StartBuilding(int prefabIndex)
    {
        builder.buildingObject = Instantiate(builder.prefabs[prefabIndex], FitToGrid(mousePosition), Quaternion.identity).GetComponent<Building>();
        isBuilding = true;
    }
    private void Building()
    {
        buildingObject.transform.position = FitToGrid(mousePosition);
    }
    private void EndBuilding()
    {
        List<RaycastResult> results = new();

        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        raycaster.Raycast(pointerEventData, results);

        foreach(RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<BuildButton>(out BuildButton button))
            {
                Destroy(buildingObject);
                isBuilding = false;
                return;
            }
        }

        buildingObject.OnBuild();
        buildingObject = null;
        isBuilding = false;
    }
    private bool DestroyBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mask);

        if (hit.collider == null && oldHit.collider == null)
            return false;
        else
        {
            if((hit.collider == null || hit.collider != oldHit.collider) && oldHit.collider != null)
            {
                if (oldHit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StopBreak();
            }else if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StartBreak();
            }else if (Input.GetMouseButtonUp(0))
            {
                if (hit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StopBreak();
            }

            oldHit = hit;

            return true;
        }
    }
}
