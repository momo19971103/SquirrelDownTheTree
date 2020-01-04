using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    private AudioSource audioSource;


    Dictionary<eType, AudioClip> _clipPool = new Dictionary<eType, AudioClip>();
    public enum eType
    {
        INTHEAIR,
        DEAD,
        LANDING,
        MOVE,
        SQUAT
    }

    public void Play(eType type)
    {

        audioSource.PlayOneShot(_clipPool[type]);

    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _clipPool[eType.INTHEAIR] = Resources.Load<AudioClip>(@"Audios\power_up_21");
        _clipPool[eType.DEAD] = Resources.Load<AudioClip>(@"Audios\game_over_30");
        _clipPool[eType.LANDING] = Resources.Load<AudioClip>(@"Audios\jump_01");
        _clipPool[eType.MOVE] = Resources.Load<AudioClip>(@"Audios\jump_10");
        _clipPool[eType.SQUAT] = Resources.Load<AudioClip>(@"Audios\hit_16");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
