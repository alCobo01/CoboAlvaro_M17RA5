using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SaveManager _saveManager;

    private void Start()
    {
        _saveManager = SaveManager.Instance;
        _saveManager.LoadGame();
    }
}
