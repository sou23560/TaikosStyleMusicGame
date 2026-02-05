using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] public int combo = 0;
    int combobefore = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (combo != combobefore)
        {
            Debug.Log(combo);
            combobefore = combo;
        }
    }
}
