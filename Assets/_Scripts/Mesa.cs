using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class Mesa : MonoBehaviour
{
    public Dictionary<ItemSO, int> comanda = new Dictionary<ItemSO, int>();

    public List<string> itemName = new List<string>();
    public List<int> itemQuant = new List<int>();

    public void AddItem(ItemSO _item)
    {
        if (!comanda.ContainsKey(_item))
        {
            comanda.Add(_item, 1);
            return;
        }

        comanda[_item]++;

        if (comanda[_item] == 0)
        {
            comanda.Remove(_item);
        }
    }
    public void RemoveItem(ItemSO _item)
    {
        if (!comanda.ContainsKey(_item))
        {
            comanda.Add(_item, -1);
            return;
        }

        comanda[_item]--;

        if (comanda[_item] == 0)
        {
            comanda.Remove(_item);
        }
    }
    public int GetQuantity(ItemSO _item)
    {
        if (!comanda.ContainsKey(_item))
        {
            return 0;
        }

        return comanda[_item];
    }

    public void GetBill()
    {
        string bill = gameObject.name + ": \n";
        float total = 0;
        itemName = new List<string>();
        itemQuant = new List<int>();

        foreach (KeyValuePair<ItemSO, int> item in comanda)
        {
            bill += item.Key.itemName + "......" + item.Value + " ...... " + item.Key.price + "\n";
            total += item.Key.price * item.Value;

            itemName.Add(item.Key.name);
            itemQuant.Add(item.Value);
        }

        bill += total.ToString("0.00") + "€\n\n";

        bill += JsonUtility.ToJson(this);

        print(bill);


        //----------------------------------------------------------------------
        //string filePath = Application.dataPath + "/_Scripts/document.json";

        //using(StreamReader stream = new StreamReader(filePath))
        //{
        //    MesaData nueva = JsonUtility.FromJson<MesaData>(stream.ReadToEnd());

        //    itemName = nueva.itemName;
        //    itemQuant = nueva.itemQuant;

        //    print(JsonUtility.ToJson(nueva));
        //}
    }
}
