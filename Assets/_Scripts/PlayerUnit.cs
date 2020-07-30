using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    Vector3 velocity;

    // La posición que creemos es la mas coreccta para este player.
    // NOTA: si tenemos authority, este será exactamente el mismo que transform.position;
    Vector3 bestGuessPosition;

    //este valor se actualiza constantemente para saber nuestra latencia con el SERVER
    float ourLatency;
    float latencySmoothingFactor = 10f;

	// Update is called once per frame
	void Update () {

        // El código pasa por aquí para TODAS las versiones del objeto, incluso si no es una copia autorizada
        // Predecimos donde el objeto debería estar basado en la última actualización de velocidad.
        if (!hasAuthority ){

            bestGuessPosition = bestGuessPosition + (velocity * Time.deltaTime);

            //En vez de teleportarnos a nuestra bestGuessPosition, podemos desplazarnos suavemente.
            transform.position = Vector3.Lerp(transform.position, bestGuessPosition, Time.deltaTime * latencySmoothingFactor);
            return;
        }

        // Si llegamos aquí, tenemos autorización para este objeto.
        transform.Translate(velocity * Time.deltaTime);
		
        if(Input.GetKeyDown(KeyCode.Space)){
            
            this.transform.Translate(0,1,0);
        }

        if(Input.GetKeyDown(KeyCode.Delete)){
            Destroy(gameObject);
        }

        if (/*Some input*/ true){
            //Player pregunta para cambiar nuestra direccion/velocidad

            velocity = new Vector3(1, 0, 0);

            //accelerate = 
            //turnRate = 

            CmdUpdateVelocity(velocity, transform.position);
        }
	}


    [Command] void CmdUpdateVelocity(Vector3 v, Vector3 p){
        //Estoy en el SERVER
        transform.position = p;
        velocity = v;

        // Si sabemos cual es nuestra latencia actual, podríamos hacer algo así:
        // transform.position = p + (v * thisPlayersLatencyToServer));

       // RcpUpdateVelocity(velocity, transform.position);
    }
    /*
    [ClientRpc]
    void RcpUpdateVelocity(Vector3 v, Vector3 p) {
        //Estoy en el CLIENT


        if(hasAuthority){
            //Tengo mejor latencia que el server
            return;
        }

        // Si sabemos cual es nuestra latencia actual, podríamos hacer algo así:
        // transform.position = p + (v * ourLatency));
        //transform.position = p;

        velocity = v;
        bestGuessPosition = p + (velocity * (ourLatency));
    }
    */
}
