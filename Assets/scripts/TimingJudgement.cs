using Unity.VisualScripting;
using UnityEngine;

public enum JudgmentAnswer
{
    Ryo,
    Ka,
    Huka,
    TooFar,
}

public class TimingJudgement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null!; // 最初のリクエストにあったInputManager
    [SerializeField] public float borderline_ryou_ka = 0.2f;
    [SerializeField] public float borderline_ka_huka = 0.5f;
    [SerializeField] public float borderline_huka_null = 1f;
    [SerializeField] public ComboCounter comboCounter = null!;

 

    private void OnEnable()
    {
        // InputManagerのイベントに登録
        if (inputManager != null)
        {
            inputManager.OnDon += HandleDon;
            inputManager.OnKa += HandleKa;
        }
    }

    private void OnDisable()
    {
        // 登録解除
        if (inputManager != null)
        {
            inputManager.OnDon -= HandleDon;
            inputManager.OnKa -= HandleKa;
        }
    }

    // ドンが押された時の入り口
    private void HandleDon() => ProcessJudgement("Don");

    // カが押された時の入り口
    private void HandleKa() => ProcessJudgement("Ka");

    // 共通の判定ロジック
    private void ProcessJudgement(string noteTag)
    {
        GameObject nearestNote = GetNearestNote(noteTag);
        if (nearestNote == null) return;

        JudgmentAnswer result = Judge(nearestNote);
        Debug.Log($"{noteTag} Result: {result}");

        // 判定が「遠すぎ」以外ならノーツを消す
        if (result != JudgmentAnswer.TooFar)
        {
            Destroy(nearestNote);
        }

        // コンボ更新
        if (result == JudgmentAnswer.Ka || result == JudgmentAnswer.Ryo)
        {
            comboCounter.AddCombo(1);
        }
        else if (result == JudgmentAnswer.Huka)
        {
            comboCounter.ResetCombo();
        }
    }

    GameObject GetNearestNote(string str)
    {
        var notes = GameObject.FindGameObjectsWithTag(str);
        if (notes.Length == 0) return null;

        GameObject nearestNote = null;
        float minDistance = float.MaxValue;

        foreach (var note in notes)
        {
            float distance = CalcDistance(note);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNote = note;
            }
        }
        return nearestNote;
    }

    float CalcDistance(GameObject note)
    {
        return Vector3.Distance(transform.position, note.transform.position);
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