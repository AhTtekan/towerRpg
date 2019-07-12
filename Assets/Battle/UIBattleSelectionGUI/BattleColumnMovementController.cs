using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleColumnMovementController
{
    Column[] Columns;
    int index;
       
    /// <summary>
    /// Constructor for BattleColumnMovementController
    /// </summary>
    /// <param name="canvasGroups">canvasGroups for each column stored in index order referring to the Content gameobject under Viewport</param>
    public BattleColumnMovementController(params CanvasGroup[] canvasGroups)
    {
        Columns = canvasGroups.Select(x => new Column { ContentBox = x }).ToArray();

    }

    public void Back()
    {
        Back(true);
    }

    public void Back(bool clearOldContent)
    {
        if (index == 0)
            return;
        index--;
        var column = Columns[index];
        if (column.LastSelectedButton != null)
        {
            if(clearOldContent)
                ClearColumn(index + 1);
            column.ContentBox.interactable = true;
            Columns[index + 1].ContentBox.interactable = false;
            column.LastSelectedButton.Select();
            column.LastSelectedButton.GetComponent<IUIAccentuate>().ResetAccent();
            column.LastSelectedButton = null;
        }
    }

    public void Forward()
    {
        if (index == Columns.Length)
            return;
        index++;
        
        Columns[index].ContentBox.interactable = true;
        var previousColumn = Columns[index - 1];
        previousColumn.ContentBox.interactable = false;

        Columns[index].ContentBox.GetComponent<UIAutoResizeContent>().UpdateHeightFromChildrenHeight();

        //Columns[index].ContentBox.transform.GetChild(0).GetComponent<Button>().Select(); //This doesn't seem to work consistently, using EvenSystem.SetSelectedGameObject call instead.
        if (EventSystem.current != null && Columns[index].ContentBox.transform.childCount > 0)
            EventSystem.current.SetSelectedGameObject(Columns[index].ContentBox.transform.GetChild(0).gameObject);
    }
        
    public void SetColumnLastSelected(int columnIndex, Button button)
    {
        Columns[columnIndex].LastSelectedButton = button;
    }

    private void ClearColumn(int columnIndex)
    {
        Columns[columnIndex].ContentBox.transform.DestroyAllChildren();
    }

    private struct Column
    {
        public CanvasGroup ContentBox;
        public Button LastSelectedButton;
    }
}
