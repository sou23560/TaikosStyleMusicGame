using UnityEngine;

public class ChartLoader : MonoBehaviour
{
    [SerializeField]private TextAsset chartJSON;
    ChartData data;
    public ChartData LoadChart()
    {
        data = JsonUtility.FromJson<ChartData>(chartJSON.text);
        return data;
    }
}
