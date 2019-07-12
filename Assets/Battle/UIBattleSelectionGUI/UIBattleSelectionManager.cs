using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBattleSelectionManager : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField]
    InputReader _reader;
    [SerializeField]
    private CanvasGroup Column2Content;
    [SerializeField]
    private CanvasGroup Column1Content;
    [SerializeField]
    private Button GUIButtonPrefab;
#pragma warning restore 0649

    private BattleColumnMovementController _battleColumnController;

    public UnityEvent UIPopulateColumn;
    
    void Awake()
    {
        _battleColumnController = new BattleColumnMovementController(Column1Content, Column2Content, null);
        _reader.OnCancel += _battleColumnController.Back;
    }
    
    public void PopulateColumn2WithTest()
    {
        PopulateColumn2WithTestData();
        _battleColumnController.Forward();
    }

    public void SetColumn1LastSelected(Button button)
    {
        _battleColumnController.SetColumnLastSelected(0, button);
    }

    private void PopulateColumn2WithTestData()
    {
        Column2Content.transform.DestroyAllChildren();

        for (int i = 0; i < UnityEngine.Random.Range(4, 10); i++)
        {
            GenerateNewButton(i);
        }

        UIPopulateColumn.Invoke();
    }

    private void GenerateNewButton(int i)
    {
        RectTransform button = GameObject.Instantiate(GUIButtonPrefab).GetComponent<RectTransform>();
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger.triggers == null)
            trigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();
        EventTrigger.Entry entry = trigger.triggers.FirstOrDefault(x => x.eventID == EventTriggerType.Select);// new EventTrigger.Entry();
        if (entry == null)
        {
            entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Select
            };
            trigger.triggers.Add(entry);
        }
        button.SetParent(Column2Content.transform, false);
        entry.callback.AddListener((eventData) => { button.parent.parent.parent.GetComponent<UIUpdateFunctions>().Scroll(); });
        button.anchoredPosition = new Vector2(button.anchoredPosition.x, button.sizeDelta.y * -i);
    }
}
