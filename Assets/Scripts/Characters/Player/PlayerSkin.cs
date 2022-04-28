using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class SkinData
{
    [SerializeField] private GameObject _skinPrefab;
    [SerializeField] private UI_Skin _imagePrefab;
    [Range(1, 10)]
    [SerializeField] private int _needLevels = 2;

    public GameObject SkinPrefab => _skinPrefab;
    public UI_Skin ImagePrefab => _imagePrefab;
    public int NeedLevels => _needLevels;

    [HideInInspector] public int LevelsCount;

    public float Progress
    {
        get
        {
            return (float)LevelsCount / NeedLevels;
        }
    }
}

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private List<SkinData> _skins = new List<SkinData>();

    private int _current = -1;
    private GameObject _currentSkinObject;

    [Inject]
    private Robbery _robbery;

    private void Start()
    {
        _robbery.OnSuccess.AddListener(AddProgress);
    }

    public SkinData Next
    {
        get
        {
            int nextIndex = _current + 1;

            if (nextIndex < 0 || nextIndex >= _skins.Count)
                return null;

            return _skins[nextIndex];
        }
    }

    private void Awake()
    {
        _currentSkinObject = GetComponentInChildren<Animator>().gameObject;
    }

    public void AddProgress()
    {
        if (Next == null) return;

        Next.LevelsCount++; 
    }

    public void ChangeToNext()
    {
        if (_current < _skins.Count)
            _current++;

        ChangeSkin(_current);
    }

    private void ChangeSkin(int index)
    {
        if (index >= _skins.Count)
            return;


        Destroy(_currentSkinObject.gameObject);

        Instantiate(_skins[index].SkinPrefab, transform, false);
    }
}
