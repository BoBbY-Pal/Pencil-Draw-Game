using System.Collections.Generic;
using Enums;
using UnityEngine;

public class LinesDrawer : MonoBehaviour 
{

	public GameObject linePrefab;
	
	// int cantDrawOverLayerIndex;

	[Space ( 30f )]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;
	private float _timeOfLineDraw;
	public float lineDrawLimit;
	
	private Line _currentLine;
	private ObjectPooler<Line> _objectPooler;
	Camera _cam;
	[SerializeField] private Canvas _canvas;
	[SerializeField] private int nodeSpacing;

	void Start ( ) 
	{
		_cam = Camera.main;
		_objectPooler = new ObjectPooler<Line>(linePrefab, this.transform);
	}

	private void OnDisable()
	{
		// _objectPooler.ClearPool();
	}

	// void Update ( ) {
	// 	if ( Input.GetMouseButtonDown ( 0 ) )
	// 		BeginDraw ( );
	// 	
	// 	if ( _currentLine != null )
	// 		Draw ( );
	// 	
	// 	if ( Input.GetMouseButtonUp ( 0 ) )
	// 		EndDraw ( );
	// }

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
					currentPoint += Vector2.left *  nodeSpacing;
					break;
				case Direction.RIGHT:
					currentPoint += Vector2.right *  nodeSpacing;
					break;
				case Direction.UP:
					currentPoint += Vector2.up *   nodeSpacing;
					break;
				case Direction.DOWN:
					currentPoint += Vector2.down *  nodeSpacing;
					break;
			}

			_currentLine.AddPoint(currentPoint); // Add the new point based on the direction
		}
	}
	
	public void DrawLineFromDirection(Line line, Direction direction)
	{
		Vector2 currentPoint = Vector2.zero;
	
			switch (direction)
			{
				case Direction.LEFT:
					currentPoint += Vector2.left *  nodeSpacing;
					break;
				case Direction.RIGHT:
					currentPoint += Vector2.right *  nodeSpacing;
					break;
				case Direction.UP:
					currentPoint += Vector2.up *   nodeSpacing;
					break;
				case Direction.DOWN:
					currentPoint += Vector2.down *  nodeSpacing;
					break;
			}

			line.AddPoint(currentPoint); // Add the new point based on the direction
	
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
	// Draw ----------------------------------------------------
	void Draw ( ) {
		Vector2 mousePosition = _cam.ScreenToWorldPoint ( Input.mousePosition );

		// //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
		// RaycastHit2D hit = Physics2D.CircleCast ( mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer );
		_timeOfLineDraw += Time.deltaTime;
		if ( _timeOfLineDraw >= lineDrawLimit )
			EndDraw ( );
		else
			_currentLine.AddPoint ( mousePosition );
	}
	// End Draw ------------------------------------------------
	void EndDraw ( ) {
		if ( _currentLine != null ) 
		{
			if ( _currentLine.pointsCount < 2 ) 
			{
				//If line has one point
				StartCoroutine(_currentLine.ResetLine(0));
			} 
			else
			{
				//Activate Physics on the line
				// Set it to 'true' if you want line to fall down after drawing it.
				_currentLine.UsePhysics ( false );
				StartCoroutine(_currentLine.ResetLine(5));
				_currentLine = null;
			}

			_timeOfLineDraw = 0;
		}
	}
}
