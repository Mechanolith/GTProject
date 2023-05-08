using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stackTitle;
    [SerializeField] private GameObject testButton;
    [SerializeField] private GameObject resetButton;

    private void Awake()
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
