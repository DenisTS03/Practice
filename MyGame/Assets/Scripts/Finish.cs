using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject gameWinCanvas;

    private bool _isActivated1;
    private bool _isActivated2;
    private bool _isActivated3;

    public void Activate(int number) {
        switch(number)
        {
            case 1:
                _isActivated1 = true;
                break;
            case 2:
                _isActivated2 = true;
                break;
            case 3:
                _isActivated3 = true;
                break;
        }
        
    }
    public void FinishLevel() {
        if(_isActivated1 && _isActivated2 && _isActivated3) {
            gameWinCanvas.SetActive(true);
        }
    }
}
