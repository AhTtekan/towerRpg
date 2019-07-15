using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    public string Name;// { get; set; }
    public Sprite Portrait;// { get; set; }
    public float AP_Current;// { get; set; }
    public int HP_Current;// { get; set; }
    public int HP_Max;// { get { return hp_max; } set { hp_max = value; } }
}