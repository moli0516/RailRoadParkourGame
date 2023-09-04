using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TriggerVolume : MonoBehaviour
{

    private GameController gameManager;

    public UnityEvent<GameObject> OnEnter;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController player))
        {
            OnEnter.Invoke(player.gameObject);
            gameManager.OnFail.Invoke();
        }     
    }

}
