using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationManager : SingletonBase<MapLocationManager>
{

    Dictionary<int, Vector3> characterLocations = new Dictionary<int, Vector3>();

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneLoad;
    }

    internal void SetLocation()
    {
        SetLocation(SceneManager.GetActiveScene().buildIndex);
    }

    internal void SetLocation(int buildIndex)
    {
        SetLocation(buildIndex, GameObject.FindObjectOfType<CharacterMovement>().transform.position);
    }

    internal void SetLocation(Vector3 position)
    {
        SetLocation(SceneManager.GetActiveScene().buildIndex, position);
    }

    internal void SetLocation(int buildIndex, Vector3 position)
    {
        if (characterLocations.ContainsKey(buildIndex))
            characterLocations[buildIndex] = position;
        else
            characterLocations.Add(buildIndex, position);
    }

    void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (characterLocations.ContainsKey(scene.buildIndex))
        {
            GameObject.FindObjectOfType<CharacterMovement>().transform.position = characterLocations[scene.buildIndex];
        }
    }
    
}
