using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressOToIncreaseEXP : MonoBehaviour
{
    [SerializeField] private Text expText;

    private int expINT;

    // Start is called before the first frame update
    private void Start()
    {
        expINT = int.Parse(expText.text);
    }

    // Update is called once per frame
    private void Update()
    {
        expINT = int.Parse(expText.text);

        if (Input.GetKeyDown(KeyCode.O))
        {
            expINT = expINT + 5;
            expText.text = expINT.ToString();
        }
    }
}
