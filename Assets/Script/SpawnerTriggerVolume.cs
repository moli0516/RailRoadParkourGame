using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SpawnerTriggerVolume : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController player))
        {
            OnEnter.Invoke(player.gameObject);
        }     
    }

}
