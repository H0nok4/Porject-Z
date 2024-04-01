using System.Collections;
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