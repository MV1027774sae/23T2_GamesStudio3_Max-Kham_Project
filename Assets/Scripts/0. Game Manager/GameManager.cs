using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerStatManager playerStatManager;

    void Start()
    {
        //SaveLoadManager.LoadGame(playerStatManager);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //    SaveLoadManager.SaveGame(playerStatManager);

        //if (Input.GetKeyDown(KeyCode.L))
        //    SaveLoadManager.LoadGame(playerStatManager);
    }

    private void OnDestroy()
    {
        //SaveLoadManager.SaveGame(playerStatManager);
        //PlayerPrefs.Save();
    }
}
