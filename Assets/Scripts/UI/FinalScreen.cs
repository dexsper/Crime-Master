using UnityEngine;
using Zenject;

public class FinalScreen : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _losePanel;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private IInput _input;

    public void RestartLevel()
    {
        _successPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);

        _levelManager.ChangeLevel(_levelManager.CurrentLevel);

        _input.Enabled = true;

    }
    public void NextLevel()
    {
        _successPanel.gameObject.SetActive(false);
        _input.Enabled = true;
    }

    public void ShowLose()
    {
        _losePanel.gameObject.SetActive(true);
    }

    public void ShowSuccess()
    {
        _successPanel.gameObject.SetActive(true);
    }
}
