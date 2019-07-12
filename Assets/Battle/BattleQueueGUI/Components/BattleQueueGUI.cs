using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleQueueGUI : MonoBehaviour
{

    private float height, width, incrementWidth;
    public GameObject background;
    public GameObject ItemBackground;
    public GameObject ComboBackground;
    public GameObject TextPrefab;
    private Transform canvas;
    private Transform Canvas
    {
        get
        {
            if (canvas == null)
                canvas = transform.Find("QueueGUICanvas");
            return canvas;
        }
    }
    private GameObject bg;

    List<QueueObj> queue;
    List<ComboObj> combos;
    public static BattleQueueGUI Initialize(GameObject prefab, int length, float width = 0, float height = 0)
    {
        BattleQueueGUI output = GameObject.Instantiate(prefab).GetComponent<BattleQueueGUI>();
        output.Initialize(length, width, height);
        return output;
    }

    public void Initialize(int length, float width = 0, float height = 0)
    {
        this.height = height;
        if (this.height == 0)
            this.height = 20f;
        this.width = width;
        if (this.width == 0)
            this.width = 400f;
        this.incrementWidth = this.width / length;
        SetBackground();
    }

    private void Start()
    {
    }

    private void SetBackground()
    {
        bg = GameObject.Instantiate(background);
        bg.transform.SetParent(Canvas.transform, false);
        bg.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height / 6f);
    }


    public void Add(int length, string primaryText, string secondaryText)
    {
        if (queue == null)
            queue = new List<QueueObj>();
        QueueObj obj = new QueueObj();
        obj.length = length;
        obj.primaryText = primaryText;
        obj.secondaryText = secondaryText;

        obj.instance = GameObject.Instantiate(ItemBackground);
        var rt = obj.instance.GetComponent<RectTransform>();
        obj.instance.transform.SetParent(Canvas, false);
        float xPos = (queue.Any() ? queue.Sum(x => x.length) : 0) * incrementWidth;
        float startPos = -bg.GetComponent<RectTransform>().sizeDelta.x / 2;
        rt.sizeDelta = new Vector2(length * incrementWidth, height);
        startPos += rt.sizeDelta.x / 2;
        rt.localPosition = new Vector3(xPos + startPos, 0f);
        obj.instance.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(length * incrementWidth * 10, height * 10);
        obj.instance.transform.Find("Text").GetComponent<Text>().text = primaryText;
        obj.instance.transform.Find("itemBackground").Find("Text").GetComponent<Text>().text = secondaryText;
        
        //if secondary text obj is smaller than main obj, set width equal (only occurs with length == 1)
        if(obj.instance.transform.Find("itemBackground").GetComponent<RectTransform>().sizeDelta.x > rt.sizeDelta.x)
        {
            obj.instance.transform.Find("itemBackground").GetComponent<RectTransform>().sizeDelta = new Vector2(rt.sizeDelta.x,
                obj.instance.transform.Find("itemBackground").GetComponent<RectTransform>().sizeDelta.y);
            obj.instance.transform.Find("itemBackground").Find("Text").GetComponent<RectTransform>().sizeDelta =
                obj.instance.transform.Find("itemBackground").GetComponent<RectTransform>().sizeDelta * 10;
        }

        queue.Add(obj);
    }

    public void AddCombo(int start, int length, string primaryText, string secondaryText)
    {
        if (queue == null || !queue.Any() || queue.Count < start + length)
            return;
        if (combos == null)
            combos = new List<ComboObj>();
        ComboObj obj = new ComboObj();
        obj.length = length;
        obj.start = start;

        obj.instance = GameObject.Instantiate(ComboBackground);
        obj.instance.transform.SetParent(Canvas, false);
        float xPos = queue[start].instance.GetComponent<RectTransform>().localPosition.x 
            - queue[start].instance.GetComponent<RectTransform>().sizeDelta.x / 2;
        float width = 0f;
        for(int i = start; i < length + start; i++)
        {
            width += queue[i].instance.GetComponent<RectTransform>().sizeDelta.x;
        }
        RectTransform inst = obj.instance.GetComponent<RectTransform>();
        inst.localPosition = new Vector3(xPos, inst.localPosition.y);
        inst.sizeDelta = new Vector2(width, inst.sizeDelta.y);
        obj.instance.transform.Find("Text").GetComponent<Text>().text = primaryText;
        obj.instance.transform.Find("comboBackground").Find("Text").GetComponent<Text>().text = secondaryText;

        combos.Add(obj);
    }
    private class QueueObj
    {
        internal int length;
        internal string primaryText;
        internal string secondaryText;
        internal GameObject instance;
    }

    private class ComboObj
    {
        //length and start refer to indexes and number of QueueObj in queue
        internal int length;
        internal int start;
        internal GameObject instance;
    }
    public void RemoveLast()
    {
        //if combo over most recent item, destroy that
        if (combos != null && combos.Any())
        {
            var c = combos.Last();
            if (c.start + c.length == queue.Count)
            {
                GameObject.Destroy(c.instance);
                combos.Remove(c);
                return;
            }
        }
        //else destroy most recently added GO in queue
        var q = queue.Last();
        GameObject.Destroy(q.instance.gameObject);
        queue.Remove(q);
    }

    public void Clear()
    {
        while (queue.Any())
            RemoveLast();
    }

    //TODO: Need overlay for combos

}
