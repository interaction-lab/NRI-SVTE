using RosSharp.RosBridgeClient;
using RosSharp_Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriSVTE
{
    public class PictureGalleryManager : Singleton<PictureGalleryManager>
    {
        List<GameObject> PictureList { get; set; } = new List<GameObject>();

        private ImageSubscriber _imageSubscriber;
        private AnimationPublisher animPub;
        public ImageSubscriber ImageSubscriber
        {
            get
            {
                if (!_imageSubscriber)
                {
                    _imageSubscriber = FindObjectOfType<ImageSubscriber>();
                }
                return _imageSubscriber;
            }
        } // TODO: replace inspector public variable with better getter

        public void TakePicture()
        {
            animPub.PublishAnim(AnimationPublisher.ANIMATION_CMD.gotit);
            PictureList.Add(Instantiate(Resources.Load<GameObject>(ResourcePathConstants.PictureObject)));
            GameObject curPic = PictureList[PictureList.Count - 1];
            curPic.transform.SetParent(transform);
            curPic.transform.localPosition = Vector3.forward; // maybe we can aim this at where the person is looking...
            // curPic.transform.Rotate(new Vector3(0, 0, 180)); // fixes inversion issue TODO: move this down

            MeshRenderer rend = curPic.transform.GetChild(1).GetComponent<MeshRenderer>(); // TODO: get rid of child
            Texture2D canvasTexture = new Texture2D(1, 1);
            canvasTexture.LoadImage(ImageSubscriber.ImageData);
            canvasTexture.Apply();
            rend.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard")); // TODO: move this to resource path constants
            rend.material.SetTexture("_MainTex", canvasTexture);
        }
    }
}