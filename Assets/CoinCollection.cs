using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    int coin = 0;

    [SerializeField] TMP_Text coinstext;
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sphere"))
        {
            Destroy(other.gameObject);
            coin += 10;
            coinstext.text = "Coins : " + coin;
            
        }
        if (other.gameObject.CompareTag("Cube"))
        {
            Destroy(other.gameObject);
            coin += 30;
            coinstext.text = "Coins : " + coin;
        }
    }
}
