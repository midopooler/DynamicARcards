using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class video : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var vp = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        vp.url = "https://darkhorse-static-data.s3-us-west-2.amazonaws.com/g4bwu8kgo1cjx1_videoplayback.mp4";
       // Debug.Log(LoadAPItoCard.VideoURL);

        vp.isLooping = true;
        vp.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        vp.targetMaterialRenderer = GetComponent<Renderer>();
        vp.targetMaterialProperty = "_MainTex";

        vp.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
