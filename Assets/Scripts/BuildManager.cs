using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SellType
{
    GroundEmpty,
    GroundFilled,
    AirEmpty,
    AirFilled
}

public class BuildManager : MonoBehaviour
{
    public static BuildManager builder = null;
    public static Building[,] building;
    private SellType[,] grid;
    public static bool isBuilding;
    public static Vector3 mousePosition;

    [Header("Grid")]
    [SerializeField] private Vector2 buildingAreaOffset;
    [SerializeField] private Vector2 gridOffset;
    private readonly float sellSize = 1f;
    [SerializeField] private Vector2Int buildingArea = new(5, 10);
    [Header("Prefabs")]
    public Building[] prefabs;
    [Header("Masks")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask UIMask;

    private Building buildingObject = null;
    private RaycastHit2D hit;
    private RaycastHit2D oldHit;
    private bool canBuild;

    private void Awake()
    {
        RecalculateBuilder();
        building = new Building[buildingArea.x, buildingArea.y];
        grid = new SellType[buildingArea.x, buildingArea.y];

        for (int i = 0; i < buildingArea.x; i++)
        {
            grid[i, 0] = SellType.GroundEmpty;
        }
        for (int i = 0; i < buildingArea.x; i++)
        {
            for (int j = 1; j < buildingArea.y; j++)
            {
                grid[i, j] = SellType.AirEmpty;
            }
        }
    }
    public static void RecalculateBuilder()
    {
        if (builder == null)
        {
            builder = FindObjectOfType<BuildManager>();
        }
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
        //RecalculateBuilder();
        Vector3 output;

        output = new(
            Mathf.Floor(original.x / builder.sellSize) * builder.sellSize + builder.gridOffset.x, 
            Mathf.Floor(original.y / builder.sellSize) * builder.sellSize + builder.gridOffset.y
            );

        return output;
    }
    public static Vector3 DrawGridSection(Vector3Int size)
    {
        Vector3 result;

        Vector3 leftTopPoint = new(0.5f + size.x / 2, -0.5f - size.y / 2);
        Vector3 rightBottomPoint = new(-0.5f - size.x / 2, 0.5f + size.y / 2);

        if (size.x % 2 == 0)
            leftTopPoint.x -= 1;
        if (size.y % 2 == 0)
            rightBottomPoint.y -= 1;

        result = new(leftTopPoint.x - rightBottomPoint.x, leftTopPoint.y - rightBottomPoint.y);

        return result;
    }
    public static Vector2Int[] DrawGridSection(Vector3Int size, bool getPosOnGrid)
    {
        Vector2Int[] result = new Vector2Int[size.x*size.y];

        Vector3 leftTopPoint = new(0.5f + size.x / 2, -0.5f - size.y / 2);
        Vector3 rightBottomPoint = new(-0.5f - size.x / 2, 0.5f + size.y / 2);



        return result;
    }
    public static void StartBuilding(int prefabIndex)
    {
        builder.buildingObject = Instantiate(
            builder.prefabs[prefabIndex].gameObject, 
            FitToGrid(mousePosition), 
            Quaternion.identity
            ).GetComponent<Building>();
        isBuilding = true;
    }
    private void Building()
    {
        buildingObject.transform.position = FitToGrid(mousePosition);
        if (buildingObject.cost > MatterManager.matter)
        {
            buildingObject.renderComponent.color = buildingObject.cantBuildColor;
            canBuild = false;
        }
        else
        {
            buildingObject.renderComponent.color = buildingObject.unbuildedColor;
            canBuild = true;
        }
    }

    [Header("GUI")]
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    private PointerEventData pointerEventData;

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
            if(result.gameObject.CompareTag("UI"))
            {
                Destroy(buildingObject.gameObject);
                buildingObject = null;
                isBuilding = false;
                return;
            }
        }
        if (!canBuild)
        {
            Destroy(buildingObject.gameObject);
            buildingObject = null;
            isBuilding = false;
            return;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Vector3 pos = Vector3.up * buildingArea.y/2 + (Vector3)buildingAreaOffset + (Vector3)gridOffset;
        if (buildingArea.x % 2 == 0)
            pos.x += .5f;

        Gizmos.DrawWireCube(pos, DrawGridSection((Vector3Int) buildingArea));
    }
}
