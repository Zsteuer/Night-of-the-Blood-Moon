using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioSource : MonoBehaviour
{
    private AudioSource audio;
    private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = audio.clip;
        StartCoroutine(playSound());
    }

   IEnumerator playSound()
    {
        audio.PlayOneShot(clip);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
