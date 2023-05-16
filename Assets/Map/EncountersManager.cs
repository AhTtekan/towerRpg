using System.Linq;
using UnityEngine;

public class EncountersManager : MonoBehaviour// : SingletonBase<EncountersManager>
{

#pragma warning disable 0649
    [SerializeField]
    InputReader _reader;
#pragma warning restore 0649

    [SerializeField]
    float minTimeBetweenEncounters;
    [SerializeField]
    float maxTimeBetweenEncounters;

    float timeLeftUntilEncounter;
    
    bool hasEncounters
    {
        get
        {
            return minTimeBetweenEncounters != 0 && maxTimeBetweenEncounters != 0 && encounters != null && encounters.Any();
        }
    }

    Encounter[] encounters;

    private EncountersManager() { }

    public void UpdateEncounterTimes(float minTime, float maxTime)
    {
        minTimeBetweenEncounters = minTime;
        maxTimeBetweenEncounters = maxTime;

        if (timeLeftUntilEncounter > maxTimeBetweenEncounters || timeLeftUntilEncounter == 0)
            RollNextEncounterTime();

    }

    internal void UpdateEncounters(Encounter[] newEncounters)
    {
        encounters = newEncounters;
    }

    private void RollNextEncounterTime()
    {
        if(hasEncounters)
            timeLeftUntilEncounter = UnityEngine.Random.Range(minTimeBetweenEncounters, maxTimeBetweenEncounters);
    }

    private void TickEncounterTime(Vector3 dir)
    {
        if (!hasEncounters)
            return;

        timeLeftUntilEncounter -= Time.deltaTime;
        if(timeLeftUntilEncounter <= 0)
        {
            StartEncounter();
        }
    }

    private void StartEncounter()
    {
        RollNextEncounterTime();

        MapLocationManager.Instance.SetLocation();

        // Choose Encounter based on weight
        Encounter encounter = (Encounter)MathUtility.GetRandomWeightedObject(encounters);
        
        Debug.Log(string.Format("Battle Started! Encounter chosen: Enemies: {0}", string.Join(",",encounter.Enemies.Select(x => x.Name))));

        // TODO: Implement battle scene (This is currently in a different Unity project being developed separately. Port in it later.)
        // SceneManager.LoadScene((int)ScenesEnum.BattleScene);
    }

    protected void Awake()
    {
        _reader.OnMovement += TickEncounterTime;
    }

    protected void OnDestroy()
    {
        _reader.OnMovement -= TickEncounterTime;
    }

    private void Start()
    {
        // encounters = new Encounter[] { new Encounter() };
        RollNextEncounterTime();
    }
}

