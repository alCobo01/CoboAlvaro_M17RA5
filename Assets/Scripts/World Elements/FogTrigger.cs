using System;
using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem fog;
    [SerializeField] private float transitionDuration = 2f;

    private float _normalRate;

    private void Awake() => _normalRate = fog.emission.rateOverTime.constant;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StopAllCoroutines();
        StartCoroutine(ChangeFog(0f));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StopAllCoroutines();
        StartCoroutine(ChangeFog(_normalRate));
    }

    private IEnumerator ChangeFog(float target)
    {
        var emission = fog.emission;
        var start = emission.rateOverTime.constant;
        var elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            var newValue = Mathf.Lerp(start, target, elapsed / transitionDuration);
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(newValue);
            yield return null;
        }
        
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(target);
    }
}
