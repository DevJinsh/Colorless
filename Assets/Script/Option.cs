using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 0f;
        Player.Paused = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene("인트로");
    }
    public void BGMManager()
    {

    }
    public void SfxManager()
    {

    }
}
