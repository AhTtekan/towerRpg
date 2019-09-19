using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EncounterSetter : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    float minTimeBetweenEncounters;
    [SerializeField]
    float maxTimeBetweenEncounters;
    [SerializeField]
    Encounter[] encounters;
#pragma warning restore 0649
    EncountersManager encountersManager;


    private void Awake()
    {
        encountersManager = GameObject.FindObjectOfType<EncountersManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        encountersManager.UpdateEncounters(encounters);
        encountersManager.UpdateEncounterTimes(minTimeBetweenEncounters, maxTimeBetweenEncounters);

    }

    private void Start()
    {
        encounters = new Encounter[] { new Encounter() };
    }
}
