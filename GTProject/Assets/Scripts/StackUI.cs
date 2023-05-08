using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stackTitle;

    public void SetStackTitle(string _title)
    {
        stackTitle.text = _title;
    }
}
