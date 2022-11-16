using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    [SerializeField]
    private AudioClip clipToChangeTo;
    [SerializeField]
    private AudioSource bgmObject;

    public void ChangeMusic()
    {
        this.bgmObject.Stop();
        this.bgmObject.clip = clipToChangeTo;
        this.bgmObject.Play();
    }

}
