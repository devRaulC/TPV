using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="TPV/item")]
public class ItemSO : ScriptableObject {
    public int id;
    public string itemName;
    public float price;
    public Sprite photo;

}
