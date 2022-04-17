using UnityEngine;
using UnityEngine.Events;

public abstract class Panel : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnShow;
    public UnityEvent OnHide;
}