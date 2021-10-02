using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public List<AudioSource> BGM = new List<AudioSource>();

    private int CurrentTrack = 0;

    private void Start()
    {
        StartCoroutine(NextAudio());
    }

    private IEnumerator NextAudio()
    {
        AudioSource track = BGM[CurrentTrack++ % BGM.Count];
        track.Play();

        yield return new WaitForSeconds(track.clip.length);

        StartCoroutine(NextAudio());
    }
}
