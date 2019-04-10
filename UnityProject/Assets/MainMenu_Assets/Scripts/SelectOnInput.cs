using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    //public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.MenuUp() || InputManager.MenuDown() && buttonSelected == false && selectedObject != null)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}