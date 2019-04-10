using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressOToIncreaseEXP : MonoBehaviour
{
    [SerializeField] private Text expText = null;

    private int expFloat;

    // Start is called before the first frame update
    private void Start()
    {
        expFloat = int.Parse(expText.text);
    }

    // Update is called once per frame
    private void Update()
    {
        expFloat = int.Parse(expText.text);

        if (Input.GetKeyDown(KeyCode.O))
        {
            expFloat = expFloat + 5;
            expText.text = expFloat.ToString();
        }
    }
}
