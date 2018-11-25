using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour {

    [Header("Jogador de cima")]
    public Transform _BG_top;
    public static Transform BG_top;

    public Transform _Medio_top;
    public static Transform Medio_top;

    public Transform _Perto_top;
    public static Transform Perto_top;


    [Header("Jogador de baixo")]
    public Transform _BG_bot;
    public static Transform BG_bot;

    public Transform _Medio_bot;
    public static Transform Medio_bot;

    public Transform _Perto_bot;
    public static Transform Perto_bot;

    public void Start()
    {
        BG_top = _BG_top;
        BG_bot = _BG_bot;
        Medio_bot = _Medio_bot;
        Medio_top = _Medio_top;
        Perto_bot = _Perto_bot;
        Perto_top = _Perto_top;
    }

}
