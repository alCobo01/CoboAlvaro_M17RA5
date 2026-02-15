using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Save settings")]
    [SerializeField] private string saveFileName = "saveData.json";
    [SerializeField] private List<ItemData> allItems;
    
    public static SaveManager Instance { get; private set; }
    public SaveData CurrentSaveData { get; private set; }
    public bool ShouldLoadSave { get; set; }
    
    private string _saveLocation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _saveLocation = Path.Combine(Application.persistentDataPath, saveFileName);
    }

    public bool HasSaveData()
    {
        return File.Exists(_saveLocation);
    }

    public void SaveGame(SaveData saveData)
    {
        var json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(_saveLocation, json);
    }
    
    public void LoadGameFromDisk()
    {
        if (File.Exists(_saveLocation))
        {
            CurrentSaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
        }
    }

    public ItemData GetItemByName(string itemName)
    {
        return allItems.Find(i => i.itemName == itemName);
    }

    public bool TryGetLoadedPlayerPosition(out Vector3 position)
    {
        if (ShouldLoadSave && CurrentSaveData != null)
        {
            position = CurrentSaveData.playerPosition;
            ShouldLoadSave = false;
            return true;
        }

        position = default;
        return false;
    }
}
