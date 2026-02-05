using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class NoteTiming
{
    public float time;   // 秒単位のタイミング
    public string type;  // don / ka / etc.
}

public class ChartConductor : MonoBehaviour
{
    public AudioSource audioSource;
    public TextAsset chartFile; // JSONデータを直接Inspectorに入れてもOK
    public float startDelay = 1.0f; // 再生前のカウントダウン

    public delegate void NoteEvent(NoteTiming note);
    public static event NoteEvent OnNoteTriggered;

    private List<NoteTiming> notes = new List<NoteTiming>();
    private int noteIndex = 0;
    private bool isPlaying = false;

    void Start()
    {
        // JSONデータ読み込み
        if (chartFile != null)
        {
            string json = chartFile.text;
            notes = new List<NoteTiming>(JsonHelper.FromJson<NoteTiming>(json));
        }

        StartCoroutine(StartMusicAfterDelay());
    }

    IEnumerator StartMusicAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        audioSource.Play();
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying) return;
        if (!audioSource.isPlaying) return;
        if (noteIndex >= notes.Count) return;

        float currentTime = audioSource.time;

        // 次のノーツをチェックして、再生時間を超えたら発火
        while (noteIndex < notes.Count && currentTime >= notes[noteIndex].time)
        {
            OnNoteTriggered?.Invoke(notes[noteIndex]);
            noteIndex++;
        }
    }
}

// JSON配列読み取り用
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T> { public T[] Items; }
}