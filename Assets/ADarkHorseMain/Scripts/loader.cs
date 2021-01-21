using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loader : MonoBehaviour
{
   
    public void Register()
    {
        Application.OpenURL("https://www.thedarkhorse.io/signup");
    }
    public void OpenDarkhorse()
    {
        Application.OpenURL("https://www.thedarkhorse.io/");
    }
    public void LoadLogin()
    {
        SceneManager.LoadScene("login");
    }
    public void LoadTableTop()
    {
        SceneManager.LoadScene("FeatheredPlanes");
    }
    public void LoadPlayAR()
    {
        SceneManager.LoadScene("ImageTracking");
    }
    public void LoadNavigationScene()
    {
        SceneManager.LoadScene("NavigationScene");
    }
    public void LoadintegratedScene()
    {
        SceneManager.LoadScene("QRCode4ARFoundationCamera");
    }
}
