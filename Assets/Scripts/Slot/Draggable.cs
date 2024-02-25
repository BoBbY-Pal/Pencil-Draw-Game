using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Canvas canvas;
    private Transform originalParent;
    private Vector3 startPosition;
    [FormerlySerializedAs("Direction")] [SerializeField] public Direction direction;
   
    
    public void OnBeginDrag(PointerEventData eventData) 
    {
        startPosition = transform.position;
        originalParent = transform.parent;
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
        // transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        int layerMask = 1 << LayerMask.NameToLayer("Draggable");
        layerMask = ~layerMask; // Invert the mask to ignore the specified layer
        
        RaycastHit2D hit = Physics2D.Raycast(eventData.pointerDrag.gameObject.transform.position, Vector2.down, 1, layerMask);

        if (hit.collider != null) 
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name); // Log the name of the GameObject that was hit
            
            //if the object below draggable object has the specific component that marks it as a slot.
            DirectionSlot slot = hit.collider.gameObject.GetComponent<DirectionSlot>();
            if(slot != null) 
            { 
                // Handle the case where a direction slot is hit
                Debug.Log("Direction slot hit: " + hit.collider.gameObject.name);
                
                // If the component exists, then proceed to extract the direction info from the dropped object and enqueue it.
                // directions.Enqueue(draggableComponent .GetComponent<Draggable>().Direction);
                slot.direction = direction;
                slot.draggable = this;
                // re-parent the dropped object to the slot for visual feedback.
                transform.SetParent( slot.transform);
            }
            
            // Instantiate(gameObject, startPosition, transform.localRotation, originalParent);
        }
        else
        {
            // Play destroy particle then change it's position to initial position..
            // transform.position = startPosition;
            // transform.SetParent(originalParent);
            
            // Destroy(gameObject,2);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("On pointer UP found a object: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject != null &&
            eventData.pointerCurrentRaycast.gameObject.GetComponent<DirectionSlot>())
        {
            // The pointer was released over a DirectionSlot, handle the drop here
        }
    }
}