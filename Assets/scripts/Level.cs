using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{

    public string nextLevel;

    public void MoveToNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}