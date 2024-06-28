using System;
using System.Collections;
using UnityEngine;

public class SampleSceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SampleSceneRoutine());
    }

    private IEnumerator SampleSceneRoutine()
    {
        var waitTime = new WaitForSeconds(1);
        while (true)
        {
            Debug.Log($"test {DateTime.Now}");
            Debug.LogError($"11");
            yield return waitTime;
        }
    }
}
