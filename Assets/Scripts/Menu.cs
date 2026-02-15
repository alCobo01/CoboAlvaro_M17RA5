using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start() => continueButton.SetActive(SaveManager.Instance.HasSaveData());
    
    public void StartNewGame()
    {
        SaveManager.Instance.ShouldLoadSave = false;
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void ContinueGame()
    {
        SaveManager.Instance.ShouldLoadSave = true;
        SaveManager.Instance.LoadGameFromDisk();
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void ExitGame() => Application.Quit();
}
