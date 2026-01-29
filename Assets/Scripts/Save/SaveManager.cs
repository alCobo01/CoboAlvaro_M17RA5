using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Save settings")]
    [SerializeField] private GameObject characterToSave;
    [SerializeField] private string saveFileName = "saveData.json";
    [SerializeField] private List<ItemData> allItems;
    
    public static SaveManager Instance { get; private set; }
    private string _saveLocation;

    private void Awake()
    {
        Instance = this;
        _saveLocation = Path.Combine(Application.persistentDataPath, saveFileName);
    }

    public void SaveGame()
    {
        var equipmentManager = characterToSave.GetComponent<EquipmentManager>();
        var saveData = new SaveData()
        {
            playerPosition = characterToSave.transform.position,
            equippedItemNames = equipmentManager != null ? equipmentManager.GetEquippedItemNames() : new List<string>()
        };

        var json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(_saveLocation, json);
    }

    public void LoadGame()
    {
        if (File.Exists(_saveLocation))
        {
            var saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            characterToSave.transform.position = saveData.playerPosition;

            var equipmentManager = characterToSave.GetComponent<EquipmentManager>();
            foreach (var itemName in saveData.equippedItemNames)
            {
                var item = allItems.Find(i => i.itemName == itemName);
                if (item != null)
                    equipmentManager.EquipItem(item);

            }
        }
    }
}
