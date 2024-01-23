using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int CoinAmount { get; private set; }
    public int GemAmount { get; private set; }
    public List<GameObject> Items { get; private set; } = new List<GameObject>();

    public void AddCoins(int amount) => CoinAmount += amount;
    public void AddGems(int amount) => GemAmount += amount;
    public void AddItem(GameObject item) => Items.Add(item);

}
