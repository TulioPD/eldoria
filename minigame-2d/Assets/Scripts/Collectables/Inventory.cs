using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int CoinAmount { get; private set; }
    public int GemAmount { get; private set; }
    public List<GameObject> Items { get; private set; } = new List<GameObject>();

    public void AddCoins(int amount) => CoinAmount += amount;
    public void AddGems(int amount)
    {
        GemAmount += amount;
        if (GemAmount == 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        coinText.text = "Gems remaining = "+(10-GemAmount).ToString();
    }
    public void AddItem(GameObject item) => Items.Add(item);

}
