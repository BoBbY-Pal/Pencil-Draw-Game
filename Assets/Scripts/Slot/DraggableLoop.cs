using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableLoop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject loopingPrefab;
    public Canvas canvas;
    private Transform originalParent;
    private Vector3 startPosition;
    [SerializeField] public Direction direction;

    private void Start()
    {
        startPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData) 
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.GetComponent<RectTransform>(), 
            eventData.position, 
            canvas.worldCamera, 
            out Vector3 globalMousePos);

        transform.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        int layerMask = 1 << LayerMask.NameToLayer("Draggable");
        layerMask = ~layerMask; // Invert the mask to ignore the specified layer
        
        RaycastHit2D hit = Physics2D.Raycast(eventData.pointerDrag.gameObject.transform.position, Vector2.down, 1, layerMask);

        if (hit.collider != null) 
        {
            Debug.Log("Got hit something.");
            if(hit.collider.gameObject.CompareTag("DroppableArea"))
            {
                Debug.Log("Droppable area found");
                Instantiate(loopingPrefab, hit.collider.gameObject.transform);
            }
        }
        
        Instantiate(gameObject, startPosition, transform.localRotation, originalParent);
        Destroy(gameObject);
    }
    
}