using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadListener : MonoBehaviour
{
    public LevelChanger levelChanger;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelChanger.ResetLevel();
            Time.timeScale = 1;
        }
    }
}
