using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadSceneAsync("MainScene");
    public void ExitGame() => Application.Quit();
}
