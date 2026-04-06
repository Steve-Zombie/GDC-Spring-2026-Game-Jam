using UnityEngine;

public class MusicManaging : MonoBehaviour
{

    [SerializeField] private AudioClip musicBackground;
    private AudioSource audioSources;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsByType<MusicManaging>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        audioSources = GetComponent<AudioSource>();
        audioSources.clip = musicBackground;
        audioSources.loop = true;
        audioSources.Play();
    }
}
