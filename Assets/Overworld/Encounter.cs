using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Encounter")]
internal class Encounter : ScriptableObject, IWeighted
{
    /*
     * 
     * Weight: How frequently this encounter shows up compared to others. Not sure how to do this yet
     * 
     * Enemies[]: enemies present. Need basically no info on creatures since that'll be handled by the battle, which isn't implemented yet.
     * 
     * Loot: Post battle rewards, with weighted chances of being dropped. This might be tied to the monsters I think, so just pull the info from creatures
     * 
     * Exp: Also pulled from monsters
     * 
     * 
     * 
     */

    /// <summary>
    /// Chance of this Encounter being hit compared to other Encounters in the same area.
    /// </summary>
    public int WeightValue;
    public Enemy[] Enemies;

    public int Weight { get => WeightValue; set => WeightValue = value; }

    public int GetExperience()
    {
        return Enemies.Sum(x => x.Experience);
    }

    public Item[] GetLoot()
    {
        //TODO: Get loot dropped for each enemy, based on the loot weight
        return null;
    }
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy: ScriptableObject
{
    public string Name;
    public int Experience;
    public Drop[] Drops;
}

[CreateAssetMenu(fileName = "Drop", menuName = "Drop")]
public  class Drop: ScriptableObject
{
    // items
    // drop rate
}

// TODO: Haven't created inventory system yet, so this is just a stub for encounters
public class Item
{

}

public interface IWeighted
{
    int Weight { get; set; }
} 