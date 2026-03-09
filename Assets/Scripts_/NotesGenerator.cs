using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField] GameObject donPrefab;
    [SerializeField] GameObject kaPrefab;
    [SerializeField] AudioSource music;
    [SerializeField] ChartLoader chartLoader;
    [SerializeField] float spawnLeadTime = 1.0f;

    private ChartData chart;

    private double songStartDspTime;
    private double songTime;
    private double pausedSongTime;

    private int noteIndex = 0;
    private float offset;

    private bool isPaused = false;

    void Start()
    {
        chart = chartLoader.LoadChart();
        offset = chart.offset;

        songStartDspTime = AudioSettings.dspTime + 1.0;
        music.PlayScheduled(songStartDspTime);
    }

    void Update()
    {
        if (chart == null) return;
        if (isPaused) return;

        songTime = AudioSettings.dspTime - songStartDspTime + offset;

        while (noteIndex < chart.notes.Count &&
               chart.notes[noteIndex].time <= songTime + spawnLeadTime)
        {
            SpawnNote(chart.notes[noteIndex]);
            noteIndex++;
        }
    }

    void SpawnNote(NoteData note)
    {
        GameObject prefab = note.type == 0 ? donPrefab : kaPrefab;
        Instantiate(prefab, transform.position, transform.rotation);
    }

    public void Pause()
    {
        if (isPaused) return;

        isPaused = true;

        // 現在の楽曲時間を保存
        pausedSongTime = AudioSettings.dspTime - songStartDspTime;
        Time.timeScale = 0f;
        music.Pause();
    }

    public void Resume()
    {
        if (!isPaused) return;

        isPaused = false;

        // DSP基準を再計算
        songStartDspTime = AudioSettings.dspTime - pausedSongTime;
        Time.timeScale = 1f;
        music.UnPause();
    }
}