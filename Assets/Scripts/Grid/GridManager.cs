using System;
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
        PrepareGrid();
    }

    public void PrepareGrid()
    {
        currentLevel = GameManager.Instance.currentLevel;
        gridA = new GameObject[currentLevel.gridWidth, currentLevel.gridHeight];
        gridB = new GameObject[currentLevel.gridWidth, currentLevel.gridHeight];
        GenerateGrid(currentLevel, showCaseGrid, gridA);
        linesDrawer.CreatePattern(gridA[(int) currentLevel.firstNodeIndex.x, (int) currentLevel.firstNodeIndex.y].transform.position, currentLevel.patternPath);
        
        GenerateGrid(currentLevel, drawGrid, gridB);
        GameManager.Instance.pencilController.PlacePencil(gridB[(int) currentLevel.firstNodeIndex.x, (int) currentLevel.firstNodeIndex.y].transform.position);
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

    public void ResetGrid()
    {
        foreach (var node in gridA)
        {
            Destroy(node);
        }
        foreach (var node in gridB)
        {
            Destroy(node);
        }
    }

}