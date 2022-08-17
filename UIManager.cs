using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button startButton;

    private UnityAction action;

    void Start()
    {
        action = () => OnStartClick();
    }
    public void OnStartClick()
    {
        SceneManager.LoadScene("Stage");
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }

}
