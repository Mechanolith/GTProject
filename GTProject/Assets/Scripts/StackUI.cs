using UnityEngine;
using TMPro;

public class StackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stackTitle;
    [SerializeField] private GameObject testButton;
    [SerializeField] private GameObject resetButton;

    void Awake()
    {
        testButton.SetActive(true);
        resetButton.SetActive(false);
    }

    public void SetStackTitle(string _title)
    {
        stackTitle.text = _title;
    }

    public void SetTestButtons(bool _isTesting)
    {
        testButton.SetActive(!_isTesting);
        resetButton.SetActive(_isTesting);
    }
}
