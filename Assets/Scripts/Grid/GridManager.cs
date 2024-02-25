using ManagersAndControllers;
using UnityEngine;



public class GridManager : MonoBehaviour
{
    public GameObject drawGrid;
    public GameObject showCaseGrid;

    public LinesDrawer linesDrawer;
    public GameObject[,] gridA;
    public GameObject[,] gridB;
    private Level currentLevel;
    private void Start()
    {
        currentLevel = GameManager.Instance.currentLevel;
        gridA = new GameObject[currentLevel.gridWidth, currentLevel.gridHeight];
        gridB = new GameObject[currentLevel.gridWidth, currentLevel.gridHeight];
        GenerateGrid(GameManager.Instance.currentLevel, showCaseGrid, gridA);
        linesDrawer.CreatePattern(gridA[(int) currentLevel.firstNodeIndex.x, (int) currentLevel.firstNodeIndex.y].transform.position, GameManager.Instance.currentLevel.patternPath);
        
        GenerateGrid(GameManager.Instance.currentLevel, drawGrid, gridB);
        GameManager.Instance.pencilController.InitPencil(gridB[(int) currentLevel.firstNodeIndex.x, (int) currentLevel.firstNodeIndex.y].transform.position, currentLevel.nodeSpacing);
        // linesDrawer.CreatePattern(gridB[(int) currentLevel.firstNodeIndex.x, (int) currentLevel.firstNodeIndex.y].transform.position, GameManager.Instance.levelSettings.Levels[1].patternPath);

    }

    public void GenerateGrid(Level level, GameObject gridParent, GameObject[,] grid)
    {
        Vector3 startPosition = gridParent.transform.position; // Start position is the parent's position

        float nodeSpacing = level.nodeSpacing; // Spacing between each node

        for (int width = 0; width < level.gridWidth; width++)
        {
            for (int height = 0; height < level.gridHeight; height++)
            {
                // Calculate the position for each node considering node spacing
                Vector3 nodePosition = new Vector3(
                    startPosition.x + (width * nodeSpacing),
                    startPosition.y + (height * nodeSpacing),
                    startPosition.z // Assuming a 2D grid, Z position remains constant
                );
                // Instantiate the node at calculated position
                GameObject node = Instantiate(level.dotPrefab, nodePosition, Quaternion.identity, gridParent.transform);
                Debug.Log("Node Positoin: " + nodePosition, node);
                node.name = $"Node: ({width},{height})";
                grid[width, height] = node;
            }
        }
    }



}