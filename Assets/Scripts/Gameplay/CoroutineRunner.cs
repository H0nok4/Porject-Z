using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CoroutineRunner : MonoSingleton<CoroutineRunner> {
    public Coroutine RunCoroutine(IEnumerator coroutine) {
        return StartCoroutine(coroutine);
    }

    public void StopCoroutine(Coroutine coroutine) {
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
    }
}