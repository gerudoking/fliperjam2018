using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundsPlayer : MonoBehaviour {

    public static AudioClip jump_sound;
    public  AudioClip _jump_sound;

    public static AudioClip cast_sound;
    public AudioClip _cast_sound;

    public static AudioClip damage_sound;
    public AudioClip _damage_sound;

    public static AudioClip stun_sound;
    public AudioClip _stun_sound;

    public static AudioClip attack_sound;
    public AudioClip _attack_sound;

    private void Start()
    {
       jump_sound = _jump_sound;
       cast_sound = _cast_sound;
       damage_sound = _damage_sound;
       stun_sound = _stun_sound;
       attack_sound = _attack_sound;
}

}
