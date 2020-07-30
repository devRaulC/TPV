using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public ItemSO template;
    public Image image;
    public float price;
    public Text priceText;
    public int quantityInt;
    public Text quantityText;
    public Text nameText;


    //BORRAR LUEGO
    public GameManager gameManager;

    //private void Awake()
    //{

    //    quantityInt = 0;
    //    quantityText.text = quantityInt.ToString();

    //}

    //private void Start()
    //{

    //    quantityText.text = quantityInt.ToString();

    //}

    public void Load(ItemSO _item){

        template = _item;
        nameText.text = _item.itemName;
        priceText.text = _item.price.ToString("0.00") + " €";
        image.sprite = _item.photo;

        UpdateQuantity();
    }
    public void UpdateQuantity()
    {
        quantityInt = gameManager.selected.GetQuantity(template);
        quantityText.text = quantityInt.ToString();


        //Debug.Log(nameText.text + " :" + quantityInt);
    }

    public void AddItem(){

        gameManager.selected.AddItem(template);
        UpdateQuantity();
    }
    public void QuitItem(){
        
        gameManager.selected.RemoveItem(template);
        UpdateQuantity();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
