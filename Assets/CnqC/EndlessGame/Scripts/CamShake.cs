using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class CamShake : MonoBehaviour
{
    public static CamShake ins;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    bool m_canShake;

    Vector3 originalPos;

    public void Awake()
    {
        ins = this;
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (!m_canShake) return;

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            m_canShake = false;
        }
    }

    public void ShakeTrigger(float _dur, float _amount, float _decreaseFactor = 1.0f)
    {
        m_canShake = true;
        shakeDuration = _dur;
        shakeAmount = _amount;
        decreaseFactor = _decreaseFactor;
    }

    public void ShakeTrigger(bool isTrigger = true)
    {
        m_canShake = isTrigger;
        camTransform.localPosition = originalPos;
    }
}
