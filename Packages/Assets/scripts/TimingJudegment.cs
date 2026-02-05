using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
#nullable enable
enum JudgmentAnswer
{
    Ryo,
    Ka,
    Huka,
    TooFar,
}


public class TimingJudegment : MonoBehaviour
{
    [SerializeField] private float borderline_ryou_ka = 0.2f;
    [SerializeField] private float borderline_ka_huka = 0.5f;
    [SerializeField] private float borderline_huka_null = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject? nearestNote = GetNearestNote();
            if (nearestNote == null) { return; }
            float nearestDistance = CalcDistance(nearestNote);
           JudgmentAnswer result = Judge(nearestNote);
            Debug.Log(result.ToString());
            if(result != JudgmentAnswer.TooFar)
            {
                Destroy(nearestNote);
            }
            if(result == JudgmentAnswer.Ka || result ==JudgmentAnswer.Ryo)
            {
                ComboCounter combocounter;
                GameObject obj = GameObject.Find("ComboCounter");
                combocounter = obj.GetComponent<ComboCounter>();
                combocounter.combo ++;
            }
        }
    }
    GameObject? GetNearestNote()
    {
        var notes = GameObject.FindGameObjectsWithTag("notes");
        if (notes.Length == 0)
        {
            return null;
        }
        float nearestDistance = CalcDistance(notes[0]);
        GameObject nearestNote = notes[0];
        foreach (var note in notes)
        {
            var noteDistance = CalcDistance(note);
            if (nearestDistance > noteDistance)
            {
                nearestDistance = noteDistance;
                nearestNote = note; 
            }
        }
        return nearestNote;
    }
    float CalcDistance(GameObject note)
    {
        float distance = Vector3.Distance(transform.position, note.transform.position);
        return distance;
    }

    JudgmentAnswer Judge(GameObject note)
    {
        float distance = CalcDistance(note);
        if (distance < borderline_ryou_ka) return JudgmentAnswer.Ryo;
        if (distance < borderline_ka_huka) return JudgmentAnswer.Ka;
        if (distance < borderline_huka_null) return JudgmentAnswer.Huka;
        return JudgmentAnswer.TooFar;
    }
     

}

        //if (distance < borderline_ryou_ka)
        //{
        //    Debug.Log("ryou");
        //}
        //if (distance < borderline_ka_huka)
        //{
        //    Debug.Log("ka");
        //}
        //if(distance < borderline_huka_null)

