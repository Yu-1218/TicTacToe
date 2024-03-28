using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Singleton
    public static GridSystem Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;

    public float cellSize;

    public GridObject[,] gridObjectArray;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one MapGridSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        InitMapGridSystem(width, height, cellSize);
        //CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void InitMapGridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0f, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.Clamp(Mathf.RoundToInt(worldPosition.x / cellSize), 0, width - 1),
            Mathf.Clamp(Mathf.RoundToInt(worldPosition.z / cellSize), 0, height - 1)
        );
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
#if UNITY_EDITOR
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform =
                    Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity, transform);

                debugTransform.GetComponent<GridDebugObject>().SetGridObject(GetGridObject(gridPosition));
            }
        }
#endif
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public int GetBoardSize()
    {
        return width;
    }
}
