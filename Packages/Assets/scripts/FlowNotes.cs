using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowNotes : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if (transform.position.magnitude > 30)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
      

    }
}
