using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene((int)ScenesEnum.TestMap);
        }
    }
}
