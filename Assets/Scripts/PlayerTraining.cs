using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerTraining : MonoBehaviour
{
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private RectTransform _cursorArrow;
    [SerializeField] private CityMarkers _cityMarkers;

    [Inject]
    private LevelManager _levelManager;
    [Inject]
    private CameraController _cameraController;
    [Inject]
    private LevelStart _levelStart;
    private List<Level> _completedLevels = new List<Level>();
    private Camera _camera;

    private bool _cityCursorShowed = false;
    private bool _levelCursorShowed = false;

    [Inject]
    private IInput _input;

    private void Awake()
    {
        _cityMarkers.OnMarkersUpdated += OnMarkersUpdated;
        _levelStart.OnShow.AddListener(OnStartShow);
        _cameraController.OnPlayerShow.AddListener(OnPlayerShow);

        _camera = Camera.main;
    }

    private void Start()
    {
        _cursor.gameObject.SetActive(false);
        _cursorArrow.gameObject.SetActive(false);
    }

    private void OnPlayerShow()
    {
        if (!_levelCursorShowed)
        {
            StartCoroutine(ShowPlayerMove());
        }
    }

    private void OnStartShow()
    {
        if (!_cityCursorShowed)
        {
            StopAllCoroutines();

            _cursor.gameObject.SetActive(false);

            _cityCursorShowed = true;
        }
    }

    private void OnMarkersUpdated()
    {
        if (_completedLevels.Count == 0 && !_cityCursorShowed)
        {
            StartCoroutine(ShowMarkerTap());
        }
    }

    private IEnumerator ShowPlayerMove()
    {
        _cursor.gameObject.SetActive(true);
        _cursorArrow.gameObject.SetActive(true);

        _cursor.anchoredPosition = new Vector2(0, -400f);
        _cursorArrow.anchoredPosition = new Vector2(0, -300);

        StartCoroutine(CursorHorizontalMove(160f));

        while (_input.Horizontal == 0f)
        {
            yield return new WaitForSeconds(.1f);
        }

        _cursor.gameObject.SetActive(false);
        _cursorArrow.gameObject.SetActive(false);

        StopAllCoroutines();

        _levelCursorShowed = true;
    }

    private IEnumerator CursorHorizontalMove(float range)
    {
        float animationLength = 0.5f;

        _cursor.DOAnchorPosX(-range, animationLength).SetEase(Ease.Linear);
        yield return new WaitForSeconds(animationLength);
        _cursor.DOAnchorPosX(range, animationLength).SetEase(Ease.Linear);
        yield return new WaitForSeconds(animationLength);

        yield return CursorHorizontalMove(range);
    }

    private IEnumerator ShowMarkerTap()
    {
        var first = _cityMarkers.ActiveMarkers[0];
        _cursor.gameObject.SetActive(true);

        _cursor.transform.position = _camera.WorldToScreenPoint(first.transform.position);

        yield return CursorSizeAnimation();
    }

    private IEnumerator CursorSizeAnimation()
    {
        float animationLength = 0.3f;
        yield return new WaitForSeconds(1.2f);
        _cursor.transform.DOScale(Vector3.one * .5f, animationLength).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(animationLength);
        _cursor.transform.DOScale(Vector3.one, animationLength).SetEase(Ease.InOutBack);
        yield return CursorSizeAnimation();
    }

    private void CompleteLevel()
    {
        _completedLevels.Add(_levelManager.CurrentLevel);
    }

}
