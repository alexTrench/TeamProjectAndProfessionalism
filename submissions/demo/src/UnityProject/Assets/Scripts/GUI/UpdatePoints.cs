using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePoints : MonoBehaviour
{
    [SerializeField] private Text pointsText = null;
    [SerializeField] private Text totalPoints = null;

    private int currentPoints;
    private int updatedPoints;

    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = "0";
        currentPoints   = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updatedPoints = int.Parse(totalPoints.text);
        if (updatedPoints != currentPoints)
        {
            currentPoints = updatedPoints;
            pointsText.text = updatedPoints.ToString();
        }
    }
}
