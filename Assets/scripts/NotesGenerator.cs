using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField] public GameObject donPrefab;
    [SerializeField] public GameObject kaPrefab;
    [SerializeField] private AudioSource music ;
    [SerializeField] private ChartLoader chartLoader;

    [SerializeField] public float spawnLeadTime = 1.0f; // 何秒前に生成するか

    double songTime = 0;
    private ChartData chart;
    private double songStartDspTime;
    private int noteIndex = 0;
    float offset;

    public void Start()
    {
        chart = chartLoader.LoadChart();
        songStartDspTime = AudioSettings.dspTime + 1.0;
        music.PlayScheduled(songStartDspTime);
        songTime = AudioSettings.dspTime - songStartDspTime + offset;
        offset = chart.offset;
    }

    void Update()
    {
        if (chart == null) return;
        songTime = AudioSettings.dspTime - songStartDspTime + offset;

        while (noteIndex < chart.notes.Count && chart.notes[noteIndex].time <= songTime + spawnLeadTime)
        {
            SpawnNote(chart.notes[noteIndex]);
            noteIndex++;
        }
    }

    void SpawnNote(NoteData note)
    {
        GameObject prefab = note.type == 0 ? donPrefab : kaPrefab;
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
    }

    //[SerializeField] private float firstWait = 2f;
    //[SerializeField] private float period = 3f;
    //[SerializeField] private GameObject notePrefab;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(FuncCoroutine(firstWait));
    //}

    //IEnumerator FuncCoroutine(float wait)
    //{
    //    yield return new WaitForSeconds(wait);
    //    while (true)
    //    {
    //        Instantiate(notePrefab,transform.position,transform.rotation);
    //        yield return new WaitForSeconds(period);
    //    }

    //}
}
