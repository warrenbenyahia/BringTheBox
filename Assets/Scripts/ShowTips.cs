using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTips : MonoBehaviour
{
    public GameObject tipsNote;
    public GameObject tipsCanvas;

    // Update is called once per frame
    void Update()
    {
        if (ControlManager.IsTaped() && ControlManager.IsTouchingGameObject(tipsNote))
        {
            DisplayTips();
        }
    }

    private void DisplayTips()
    {
        tipsCanvas.SetActive(true);
    }

    public void HideTips()
    {
        tipsCanvas.SetActive(false);
    }
}
