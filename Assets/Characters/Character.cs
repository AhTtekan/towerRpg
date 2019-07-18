using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
[Serializable]
public class Character
{
    public string Name;// { get; set; }
    public int PortraitId;// { get; set; }
    public int HP_Current;// { get; set; }
    public int HP_Max;// { get { return hp_max; } set { hp_max = value; } }

    public APCore APCore { get; set; } = new APCore();

    public SpeedCore SpeedCore { get; set; } = new SpeedCore();
}