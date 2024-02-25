using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PencilController : MonoBehaviour
{
    public GameObject pencil;
    float nodeSpacing;
    public LinesDrawer linesDrawer;
    private Line currentLine;
    public void InitPencil(Vector2 pos, float spacing)
    {
        pencil.transform.position = pos;
        pencil.SetActive(true);
        nodeSpacing = spacing;
    }
    
    public void MovePencil(Queue<Direction> directions) 
    {
        StartCoroutine(FollowDirections(directions));
    }

    IEnumerator FollowDirections(Queue<Direction> directions)
    {
        currentLine = linesDrawer.PrepareLineDraw(pencil.transform.position);
        Vector2 currentPoint = pencil.transform.position; // Assuming 'pencil' is your pencil GameObject
        currentLine.AddPoint(currentPoint);
        
        while (directions.Count > 0) 
        {
            Direction dir = directions.Dequeue();
            switch (dir)
            {
                case Direction.LEFT:
                    currentPoint += Vector2.left * nodeSpacing;
                    break;
                case Direction.RIGHT:
                    currentPoint += Vector2.right * nodeSpacing;
                    break;
                case Direction.UP:
                    currentPoint += Vector2.up * nodeSpacing;
                    break;
                case Direction.DOWN:
                    currentPoint += Vector2.down * nodeSpacing;
                    break;
            }

            // Move the pencil to the new position
            StartCoroutine(MovePencilToPosition( currentPoint, 1f)); // Move over 1 second
            
            
            // Wait for the pencil to reach the next node before continuing
            yield return new WaitForSeconds(1); // Adjust this time as needed
        }
        // After completing the movement, check if the pattern is correct
    }

    IEnumerator MovePencilToPosition( Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = pencil.transform.position;
        float time = 0;

        while (time < duration)
        {
            pencil.transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            currentLine.AddPoint( pencil.transform.position);
            time += Time.deltaTime;
            yield return null;
        }

        pencil.transform.position = targetPosition; // Ensure the pencil reaches the final target position
       
    }
}