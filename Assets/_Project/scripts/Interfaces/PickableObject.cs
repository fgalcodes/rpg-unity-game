using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{

    public GameObject objectMap;
    public GameObject objectPlayer;
    public GameObject objectUI;

    private void Start()
    {
        objectMap.SetActive(true);
        objectPlayer.SetActive(false);
        objectUI.SetActive(false);
    }
    public void ActivateObject()
    {
        objectMap.SetActive(false);
        objectPlayer.SetActive(true);
        objectUI.SetActive(true);
    }

}
