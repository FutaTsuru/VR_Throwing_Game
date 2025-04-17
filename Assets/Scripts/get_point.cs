using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class get_point : MonoBehaviour
{
    [SerializeField] int point;

    [SerializeField] AudioClip collision_sound;

    AudioSource effect_audio;

    GameObject Player;

    pos pos_scripts;

    // Start is called before the first frame update
    void Start()
    {
        effect_audio = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        pos_scripts = Player.GetComponent<pos>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    pos.total_score += point;
        
    //    effect_audio.PlayOneShot(collision_sound);
    //}

}
