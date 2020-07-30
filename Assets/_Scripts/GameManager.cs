using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    public GameObject mesaPan;
    public GameObject macroPan;
    public GameObject microPan;
    public string numMesa;
    public Text infoText;
    public Transform content;

    public GameObject pf;
    public Sprite sprite;

    public Mesa selected;
    public List<Mesa> mesas = new List<Mesa>();
    public List<GameObject> macros = new List<GameObject>();
    public List<ItemSO> items = new List<ItemSO>();


    private void Awake() {
        mesaPan.SetActive(true);
        macroPan.SetActive(false);
        microPan.SetActive(false);
    }

    public void CreateBill() {
        macroPan.SetActive(true);

        numMesa = EventSystem.current.currentSelectedGameObject.name;
        selected = EventSystem.current.currentSelectedGameObject.GetComponent<Mesa>();
        infoText.text = numMesa;

        //TODO: crear factura
    }

    public void PrintBill()
    {
        selected.GetBill();
    }

    public void SpawnItems(int i){

        microPan.SetActive(true);
        macroPan.SetActive(false);
        mesaPan.SetActive(false);

        foreach(var m in items){
            if(m.id == i){
                Item getItem = Instantiate(pf, content).GetComponent<Item>();


                //BORRAR LUEGO
                getItem.gameManager = this;


                getItem.Load(m);
                ////pf.GetComponent<Item>();
                //getItem.template = m;
                //getItem.nameText.text = m.name;
                //getItem.priceText.text = m.price + " €";
                //getItem.image.sprite = m.photo;
                //Instantiate(pf, content);

            }
        }
    }


    public void Done(){
        
        mesaPan.SetActive(true);
        macroPan.SetActive(true);
        microPan.SetActive(false);
    }

}



