using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerConnectionObject : NetworkBehaviour {

	void Start () {

        if(!isLocalPlayer){return;}

        CmdSpawnMyUnit();
	}


    public GameObject PlayerUnitPrefab;

    /////////////////////////////////////// SYNCVARS 
    // SyncVars son variables que si cambian su valor en el SERVER, actualiza a los CLIENTES
    [SyncVar (hook = "OnPlayerNameChanged")]
    public string PlayerName = "Anonymous";



	void Update () {

        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))  {

            CmdSpawnMyUnit();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {

            string n = "Player" + Random.Range(1, 100);
            Debug.Log("Enviando una petición al servidor para cambiar el nombre a: " +n);
            CmdChangePlayerName(n);
        }
		
	}
    //ATENCIÓN: Si usas un hook en un SyncVar, el valor local NO se actualiza automaticamente.
    void OnPlayerNameChanged(string newName){
        Debug.Log("OnPlayerNameChanged: OldName: "+PlayerName+ ", NewName: " +newName);

        PlayerName = newName;

        gameObject.name = "PlayerConnectionObject [" + newName + "]";
    }

    /////////////////////////////////////// COMMANDS 
    // Commands son funciones especiales que SOLO se ejecutan en el SERVER
    [Command] void CmdSpawnMyUnit(){

        GameObject go = Instantiate(PlayerUnitPrefab);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command] void CmdChangePlayerName(string n) {
        
        Debug.Log("CmdChangePlayerName: " + n);
        PlayerName = n;

        //RpcChangePlayerName(PlayerName);
    }

    /////////////////////////////////////// RPC
    // Rpc son funciones especiales que SOLO se ejecutan en el CLIENT, ojo, GENERA LAG. Usar SYNCVAR.

    /*[ClientRpc] void RpcChangePlayerName(string n){
        Debug.Log("RpcChangePlayerName: Preguntamos por el cambio de nombre del PlayerConnectionObject particular: " +n);
        PlayerName = n;
    }*/


}
