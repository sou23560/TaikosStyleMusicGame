using UnityEngine;

public class TaikoSound : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private AudioSource DonAudio;
    [SerializeField] private AudioSource KaAudio;


    void OnEnable()
    {
        if (inputManager != null)
        {
            inputManager.OnDon += PlayDon;
            inputManager.OnKa += PlayKa;
        }
    }

    void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.OnDon -= PlayDon;
            inputManager.OnKa -= PlayKa;
        }
    }

    void PlayDon()
    {
        Debug.Log("Don!");
        DonAudio.Play();
    }

    void PlayKa()
    {
        Debug.Log("Ka!");
        KaAudio.Play();
    }
}
