using System.Collections;
using System.Collections.Generic;
using Enums;
using ManagersAndControllers;
using UnityEngine;

public class PencilController : MonoBehaviour
{
    public GameObject pencil;
    float pencilNodeSpacing;
    float lineNodeSpacing;
    public LinesDrawer linesDrawer;
    private Line currentLine;

    public void PlacePencil(Vector2 pos)
    {
        InitPencil(pos);
    }
    
    private void InitPencil(Vector2 pos)
    {
        pencil.transform.position = pos;
        pencil.SetActive(true);
        pencilNodeSpacing = GameManager.Instance.currentLevel.nodeSpacing;
        lineNodeSpacing = linesDrawer.lineNodeSpacing;
    }
    
    public void MovePencil(Queue<Direction> directions) 
    {
        StartCoroutine(FollowDirections(directions));
    }

    IEnumerator FollowDirections(Queue<Direction> directions)
    {
        currentLine = linesDrawer.PrepareLineDraw(pencil.transform.position);
        
        Vector2 currentPencilPos = pencil.transform.position; // Assuming 'pencil' is your pencil GameObject
        Vector2 currentLinePos = currentPencilPos;
        currentLine.AddPoint(currentLinePos);
        
        while (directions.Count > 0) 
        {
            Direction dir = directions.Dequeue();
            switch (dir)
            {
                case Direction.LEFT:
                    currentPencilPos += Vector2.left * pencilNodeSpacing;
                    currentLinePos += Vector2.left * (lineNodeSpacing );
                    break;
                case Direction.RIGHT:
                    currentPencilPos += Vector2.right * pencilNodeSpacing;
                    currentLinePos += Vector2.right * (lineNodeSpacing );
                    break;
                case Direction.UP:
                    currentPencilPos += Vector2.up * pencilNodeSpacing;
                    currentLinePos += Vector2.up * (lineNodeSpacing );
                    break;
                case Direction.DOWN:
                    currentPencilPos += Vector2.down * pencilNodeSpacing;
                    currentLinePos += Vector2.down * (lineNodeSpacing );
                    break;
            }

            // Move the pencil to the new position
            StartCoroutine(MovePencilToPosition( currentPencilPos, currentLinePos, 1f)); // Move over 1 second
            
            // Wait for the pencil to reach the next node before continuing
            yield return new WaitForSeconds(1); // Adjust this time as needed
        }
        // After completing the movement, check if the pattern is correct
    }

    IEnumerator MovePencilToPosition(Vector2 pencilTargetPosition, Vector2 lineTargetPosition, float duration)
    {
        Vector2 startPosition = pencil.transform.position;
        Vector2 startLinePoint = currentLine.GetLastPoint(); // returns the last point added to the line
        float time = 0;

        while (time < duration)
        {
            // Lerp the pencil's position
            Vector2 nextPencilPosition = Vector2.Lerp(startPosition, pencilTargetPosition, time / duration);
            pencil.transform.position = nextPencilPosition;

            // Lerp the line's next point to be slightly behind the pencil, giving the effect of drawing
            Vector2 nextLinePoint = Vector2.Lerp(startLinePoint, lineTargetPosition, time / duration);
            currentLine.AddPoint( nextLinePoint); // Update the last point of the line to follow the pencil
            

            time += Time.deltaTime;
            yield return null;
        }

        pencil.transform.position = pencilTargetPosition; // Ensure the pencil reaches the final target position
    }
}