using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendu : MonoBehaviour
{

    [SerializeField]
    private string mot;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Lancement pendu. Le mot à trouver est "+mot+".");
    }
}
