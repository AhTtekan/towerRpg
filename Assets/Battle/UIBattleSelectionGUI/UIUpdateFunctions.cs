using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpdateFunctions : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    RectTransform content;
    [SerializeField]
    RectTransform scrollRect;
#pragma warning restore 0649


    public void UpdateHeightFromChildrenHeight()
    {
        var childrenTotalHeight = 0f;
        foreach (RectTransform child in content)
        {
            childrenTotalHeight += child.sizeDelta.y;
        }
        childrenTotalHeight = Mathf.Clamp(childrenTotalHeight, 120, float.MaxValue);
        if (childrenTotalHeight != Mathf.Abs(content.rect.y))
            content.sizeDelta = new Vector2(content.sizeDelta.x, childrenTotalHeight - 120);
    }
    

    public void Scroll()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null || selected.transform.parent != content.transform)
        {
            return;
        }

        RectTransform selectedRectTransform = selected.GetComponent<RectTransform>();
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

        float scrollViewMinY = content.anchoredPosition.y;
        float scrollViewMaxY = content.anchoredPosition.y + scrollRect.rect.height;

        if (selectedPositionY > scrollViewMaxY)
        {
            float newY = selectedPositionY - scrollRect.rect.height;
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
        }
        else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
        {
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y));
        }
    }
}
