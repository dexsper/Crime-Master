using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX Instance;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _moneyTake;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMoneyTake()
    {
        _audioSource.PlayOneShot(_moneyTake);
    }
}
