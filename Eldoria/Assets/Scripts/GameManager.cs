using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get { return playerData; }
        private set { playerData = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>();
        }
    }

}

