using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CoinCollection : MonoBehaviour
{
    private GameController gameManager;
    private TextMeshProUGUI coinText;

    public UnityEvent OnCollectd;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameController>();
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController player))
        {
            OnCollectd.Invoke();
            gameManager.Coin++;
            coinText.text = gameManager.Coin.ToString();
            Destroy(gameObject);
        }
    }
}
