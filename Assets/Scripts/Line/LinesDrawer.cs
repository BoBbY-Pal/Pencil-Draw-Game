using System.Collections.Generic;
using Enums;
using UnityEngine;

public class LinesDrawer : MonoBehaviour 
{

	public GameObject linePrefab;

	[Space ( 30f )]
	public Gradient lineColor;
	
	public float lineWidth;
	
	
	private Line _currentLine;
	private ObjectPooler<Line> _objectPooler;
	
	[SerializeField] private Canvas _canvas;
	[SerializeField] public int lineNodeSpacing;

	private List<Line> _lines = new List<Line>();
	
	void Start ( ) 
	{
		_objectPooler = new ObjectPooler<Line>(linePrefab, this.transform);
	}

	private void OnDisable()
	{
		// _objectPooler.ClearPool();
	}

	public void CreatePattern(Vector2 startPoint, List<Direction> directions)
	{
		_currentLine = PrepareLineDraw(startPoint);
		DrawLineFromDirections( startPoint, directions);
	}

	public Line PrepareLineDraw(Vector2 startPoint)
	{
		Line line = BeginDraw();
		line.transform.SetParent(_canvas.transform);
		line.transform.position = startPoint;
		line.transform.localScale = Vector3.one;
		_lines.Add(line);
		return line;
	}

	void DrawLineFromDirections(Vector2 startPoint, List<Direction> directions)
	{
		if (_currentLine == null)
			return;
		
		_currentLine.AddPoint(startPoint); // Add the starting point
		

		Vector2 currentPoint = startPoint;

		foreach (var direction in directions)
		{
			switch (direction)
			{
				case Direction.LEFT:
					currentPoint += Vector2.left *  lineNodeSpacing;
					break;
				case Direction.RIGHT:
					currentPoint += Vector2.right *  lineNodeSpacing;
					break;
				case Direction.UP:
					currentPoint += Vector2.up *   lineNodeSpacing;
					break;
				case Direction.DOWN:
					currentPoint += Vector2.down *  lineNodeSpacing;
					break;
			}

			_currentLine.AddPoint(currentPoint); // Add the new point based on the direction
		}
	}
	
	// Begin Draw ----------------------------------------------
	Line BeginDraw ( ) {
		// currentLine = Instantiate ( linePrefab, this.transform ).GetComponent <Line> ( );
		Line pooledLine = _objectPooler.GetPooledObject();
		if (pooledLine == null)
		{
			Debug.Log("no line available in the pool.");
			return null; // If no line available in the pool.
		}
		
		//Set line properties
		pooledLine.SetLineWidth ( lineWidth );
		return pooledLine;
	}

	public void ResetLines()
	{
		foreach (var line in _lines)
		{
			line.ResetLine();
		}
	}
}
