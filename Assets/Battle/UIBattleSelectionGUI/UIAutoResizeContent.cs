using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIAutoResizeContent : MonoBehaviour
{
    private float childrenTotalHeight;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void UpdateHeightFromChildrenHeight()
    {
        childrenTotalHeight = 0;
        foreach(RectTransform child in transform)
        {
            childrenTotalHeight += child.sizeDelta.y;
        }
        childrenTotalHeight = Mathf.Clamp(childrenTotalHeight, 120, float.MaxValue);
        if (childrenTotalHeight != Mathf.Abs(rectTransform.rect.y))
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, childrenTotalHeight - 120);
    }
}
