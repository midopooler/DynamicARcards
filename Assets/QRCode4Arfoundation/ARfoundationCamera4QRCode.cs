using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARfoundationCamera4QRCode : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events.")]
    ARCameraManager m_CameraManager;
    

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set { m_CameraManager = value; }
    }

   // [SerializeField]
   // RawImage m_RawImage;

    Texture2D m_Texture;
    public bool isPlaying = false;

    void OnEnable()
    {
      
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived += OnCameraFrameReceived;
        }
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived -= OnCameraFrameReceived;
        }
    }

    unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        // Attempt to get the latest camera image. If this method succeeds,
        // it acquires a native resource that must be disposed (see below).
        XRCameraImage image;
        if (!cameraManager.TryGetLatestImage(out image))
        {
            return;
        }

        var format = TextureFormat.RGBA32;

        if (m_Texture == null || m_Texture.width != image.width || m_Texture.height != image.height)
        {
            m_Texture = new Texture2D(image.width, image.height, format, false);
        }
        
        var conversionParams = new XRCameraImageConversionParams(image, format);
        
        var rawTextureData = m_Texture.GetRawTextureData<byte>();
        try
        {
            image.Convert(conversionParams, new IntPtr(rawTextureData.GetUnsafePtr()), rawTextureData.Length);
        }
        finally
        {
            image.Dispose();
        }
        
        m_Texture.Apply();
        isPlaying = true;
    }

   public int Width()
    {
        if (!!this.m_Texture)
        {
            return this.m_Texture.width;
        }
        return 0;
    }

   public int Height()
    {
        if (!!this.m_Texture)
        {
            return this.m_Texture.height;
        }
        return 0;
    }

    public void StartWork()
    {
        
    }

    public void StopWork()
    {
        
    }

    public Color[] GetPixels(int x, int y, int w, int h)
    {
        if (!!m_Texture)
        {
           return m_Texture.GetPixels(x, y, w, h);
        }
        return null;
    }
}