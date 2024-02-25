using System;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererUi : MonoBehaviour
{
    [SerializeField] private RectTransform m_myTransform;
    [SerializeField] private Image m_image;

    public Transform pos1;
    public Transform pos2;
    private void Start()
    {
        CreateLine(pos1.position,pos2.position, Color.green);
    }

    public void CreateLine(Vector3 positionOne, Vector3 positionTwo, Color color)
    {
        m_image.color = color;

        Vector2 point1 = new Vector2(positionTwo.x, positionTwo.y);
        Vector2 point2 = new Vector2(positionOne.x, positionOne.y);
        Vector2 midpoint = (point1 + point2) / 2f;

        m_myTransform.position = midpoint;

        Vector2 dir = point1 - point2;
        m_myTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        m_myTransform.localScale = new Vector3(dir.magnitude, 1f, 1f);
    }
}