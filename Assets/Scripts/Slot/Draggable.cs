using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    private Transform originalParent;
    private Vector3 startPosition;
    [FormerlySerializedAs("Direction")] [SerializeField] public Direction direction;

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
            //if the object below draggable object has the specific component that marks it as a slot.
            DirectionSlot slot = hit.collider.gameObject.GetComponent<DirectionSlot>();
            if(slot != null) 
            { 
                // Handle the case where a direction slot is hit
                
                // If the component exists, then proceed to extract the direction info from the dropped object and enqueue it.
                // directions.Enqueue(draggableComponent .GetComponent<Draggable>().Direction);
                slot.direction = direction;
                slot.draggable = this;
                // re-parent the dropped object to the slot for visual feedback.
                transform.SetParent( slot.transform);
            }
        }
        else
        {
            // Play destroy particle then change it's position to initial position..
            // transform.position = startPosition;
            // transform.SetParent(originalParent);
            
            Destroy(gameObject,2);
        }
        Instantiate(gameObject, startPosition, transform.localRotation, originalParent);
    }
    
}