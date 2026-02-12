using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject player;
    private SaveManager _saveManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _saveManager = SaveManager.Instance;
        if (_saveManager.ShouldLoadSave && _saveManager.CurrentSaveData != null)
        {
            ApplySaveData(_saveManager.CurrentSaveData);
            _saveManager.ShouldLoadSave = false;
        }
    }

    public void SavePlayerProgress()
    {
        var equipmentManager = player.GetComponent<EquipmentManager>();
        var saveData = new SaveData()
        {
            playerPosition = player.transform.position,
            playerRotation = player.transform.rotation,
            equippedItemNames = equipmentManager.GetEquippedItemNames()
        };

        _saveManager.SaveGame(saveData);
    }

    private void ApplySaveData(SaveData data)
    {
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // Disable to allow manual transform update
            player.transform.position = data.playerPosition;
            player.transform.rotation = data.playerRotation;
            controller.enabled = true; // Re-enable
        }
        else
        {
            player.transform.position = data.playerPosition;
            player.transform.rotation = data.playerRotation;
        }

        var equipmentManager = player.GetComponent<EquipmentManager>();
        foreach (var itemName in data.equippedItemNames)
        {
            var item = _saveManager.GetItemByName(itemName);
            if (item != null)
                equipmentManager.EquipItem(item);
        }
        
    }
}
