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
    public static SellType[,] grid;
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
    private Vector2 zeroSell;

    private void Awake()
    {
        RecalculateBuilder();
        grid = new SellType[buildingArea.x, buildingArea.y];

        for (int i = 0; i < buildingArea.x; i++)
        {
            grid[i, 0] = SellType.GroundEmpty;
        }
        for (int i = 0; i < buildingArea.x; i++)
        {
            string value = "";
            for (int j = 0; j < buildingArea.y; j++)
            {
                if (j == 0)
                    grid[i, j] = SellType.GroundEmpty;
                else
                    grid[i, j] = SellType.AirEmpty;

                value += ((int)grid[i, j]);
            }
        }

        if (buildingArea.x % 2 == 0)
            zeroSell = buildingAreaOffset - new Vector2(buildingArea.x / 2 - 1, -1);
        else
            zeroSell = buildingAreaOffset - new Vector2(buildingArea.x / 2, -1);
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
    /// <summary>
    /// return size of object
    /// </summary>
    /// <param name="size">size of object</param>
    /// <returns>returns size of object</returns>
    public static Vector3 DrawGridSection(Vector3Int size)
    {
        Vector3 result;

        result = size;

        return result;
    }
    /// <summary>
    /// return array of coordinates on grid
    /// </summary>
    /// <param name="size">size of object</param>
    /// <param name="position">position of start sell</param>
    /// <returns>array of coordinates on grid</returns>
    public static Vector2[] DrawGridSection(Vector3Int size, Vector3 position)
    {
        Vector2[] result = new Vector2[size.x * size.y];

        for (int i = 0; i < size.x * size.y; i++)
        {
            result[i] = position - (Vector3)builder.zeroSell;
        }

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
            buildingObject.ChangeColor(buildingObject.cantBuildColor);
            canBuild = false;
        }
        else
        {
            buildingObject.ChangeColor(buildingObject.unbuildedColor);
            canBuild = true;
        }

        foreach (Vector2 pos in DrawGridSection(Vector3Int.one, buildingObject.transform.position))
        {
            if (pos.x < 0 || pos.x > buildingArea.x - 1 || pos.y < 0 || pos.y > buildingArea.y - 1)
            {
                buildingObject.ChangeColor(buildingObject.cantBuildColor);
                canBuild = false;
            }else if (grid[(int)pos.x, (int)pos.y] == SellType.AirFilled || grid[(int)pos.x, (int)pos.y] == SellType.GroundFilled)
            {
                buildingObject.ChangeColor(buildingObject.cantBuildColor);
                canBuild = false;
            }else if (grid[(int)pos.x, (int)pos.y] != SellType.GroundEmpty && 
                !(grid[(int)pos.x, (int)pos.y-1] == SellType.GroundFilled || grid[(int)pos.x, (int)pos.y-1] == SellType.AirFilled))
            {
                buildingObject.ChangeColor(buildingObject.cantBuildColor);
                canBuild = false;
            }
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

        foreach (Vector2 pos in DrawGridSection(Vector3Int.one, buildingObject.transform.position))
        {
            switch (grid[(int)pos.x, (int)pos.y])
            {
                case SellType.AirEmpty:
                    grid[(int)pos.x, (int)pos.y] = SellType.AirFilled;
                    break;
                case SellType.GroundEmpty:
                    grid[(int)pos.x, (int)pos.y] = SellType.AirFilled;
                    break;
            }
        }

        buildingObject.OnBuild();
        buildingObject = null;
        isBuilding = false;
    }
    private void DestroyBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mask);

        if (hit.collider == null && oldHit.collider == null)
            return;
        else
        {
            if((hit.collider == null || hit.collider != oldHit.collider) && oldHit.collider != null)
            {
                if (oldHit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StopBreak();
            }else if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = DrawGridSection(Vector3Int.one, hit.transform.position)[0];
                Debug.Log(pos.y + ", " + buildingArea.y);
                if (pos.y != buildingArea.y-1)
                    if(grid[(int)pos.x, (int)pos.y + 1] == SellType.AirFilled)
                        return;

                if (hit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StartBreak();
            }else if (Input.GetMouseButtonUp(0))
            {
                if (hit.transform.TryGetComponent(out Building blockComponent)) blockComponent.StopBreak();
            }

            oldHit = hit;

            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Vector3 pos = Vector3.up * buildingArea.y/2 + (Vector3)buildingAreaOffset + (Vector3)gridOffset;
        if (buildingArea.x % 2 != 0)
            pos.x -= .5f;

        Gizmos.DrawWireCube(pos, DrawGridSection((Vector3Int) buildingArea));
    }
}
