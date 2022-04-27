using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class SkinData
{
    [SerializeField] private GameObject _skinPrefab;
    [SerializeField] private UI_Skin _imagePrefab;
    [Range(20, 500)]
    [SerializeField] private int _needMoney = 60;

    public GameObject SkinPrefab => _skinPrefab;
    public UI_Skin ImagePrefab => _imagePrefab;
    public int NeedMoney => _needMoney;
}

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private List<SkinData> _skins = new List<SkinData>();

    private int _current = -1;
    private float progress = 0;
    private GameObject _currentSkinObject;
    [Inject]
    private Robbery _robbery;

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

    public void AddProgress(float p)
    {
        progress += p;

        if (progress >= 1 && Next != null)
        {
            NextSkin();
            progress = 0;
        }

    }


    private void NextSkin()
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
