using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRARTest : MonoBehaviour
{
    public QRCodeDecodeController4ARfoundation e_qrController;

    public Text UiText;

    public GameObject resetBtn;

    public GameObject scanLineObj;

    private void Start()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.onQRScanFinished += new QRCodeDecodeController4ARfoundation.QRScanFinished(this.qrScanFinished);
        }
    }

    private void Update()
    {
    }

    private void qrScanFinished(string dataText)
    {

        this.UiText.text = dataText;
        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(true);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(false);
        }

    }

    public void Reset()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.Reset();
        }
        if (this.UiText != null)
        {
            this.UiText.text = string.Empty;
        }
        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(true);
        }
    }

    public void Play()
    {
        Reset();
        if (this.e_qrController != null)
        {
            this.e_qrController.StartWork();
        }
    }

    public void Stop()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }

        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(false);
        }
    }

    public void GotoNextScene(string scenename)
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }
        //Application.LoadLevel(scenename);
        SceneManager.LoadScene(scenename);
    }




}
