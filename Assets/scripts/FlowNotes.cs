using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowNotes : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    ComboCounter comboCounter;
    GameObject target;
    float borderline;
    bool judgementable = false;
    float distance;
    Vector3 targetPos;

    void Start()
    {
        target = GameObject.Find("target");

        GameObject obj = GameObject.Find("ComboCounter");
        comboCounter = obj.GetComponent<ComboCounter>();

        TimingJudegment timingJudegment = target.GetComponent<TimingJudegment>();
        borderline = timingJudegment.borderline_huka_null;
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position , targetPos);
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if (distance < borderline && judgementable == false) {
            judgementable = true;
        }
        if (distance > borderline && judgementable == true)
        {
            comboCounter.ResetCombo();
            judgementable = false;
            Debug.Log("Miss");
        }
        if (transform.position.magnitude > 30)
        {
            Destroy(gameObject);
        }
    }
}
