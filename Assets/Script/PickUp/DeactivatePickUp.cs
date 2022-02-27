using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate",Random.Range(4f,9f));
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
