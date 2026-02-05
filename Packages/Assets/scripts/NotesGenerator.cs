using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField] private float period = 3f;
    [SerializeField] private GameObject notePrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FuncCoroutine());
    }

    IEnumerator FuncCoroutine()
    {
        while (true)
        {
            Instantiate(notePrefab,transform.position,transform.rotation);
            yield return new WaitForSeconds(period);
        }

    }
}
