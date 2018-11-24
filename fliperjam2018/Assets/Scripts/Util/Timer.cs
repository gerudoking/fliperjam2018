using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    //Um contador pode ser crescente ou decrescente
    public enum TYPE {
        CRESCENTE,
        DECRESCENTE
    }

    private TYPE tipo;
    private float maxValor;
    private float valor;

    public Timer(TYPE tipo, float maxValor) {
        this.tipo = tipo;
        this.maxValor = maxValor;

        Reset();
    }

    public bool Finished() {
        if (tipo == TYPE.CRESCENTE) {
            return (valor >= maxValor);
        }
        else {
            return (valor <= 0);
        }
    }

    public void Update() {
        if (tipo == TYPE.CRESCENTE) {
            valor += Time.deltaTime;
        }
        else {
            valor -= Time.deltaTime;
        }
    }

    public void Reset() {
        if (tipo == TYPE.CRESCENTE) {
            valor = 0;
        }
        else {
            valor = maxValor;
        }
    }

    public float GetTime() {
        return valor;
    }

    public float GetRatio() {
        return valor / maxValor;
    }
}
