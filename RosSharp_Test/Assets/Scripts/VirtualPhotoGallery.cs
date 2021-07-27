using RosSharp.RosBridgeClient;
using RosSharp_Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriSVTE
{
    public class VirtualPhotoGallery : Singleton<VirtualPhotoGallery>
    {
        List<GameObject> PictureList { get; set; } = new List<GameObject>();
        public Texture pictureTexture;
        public GameObject picture;

        public void TakePicture()
        {
            PictureList.Add(Instantiate(picture));
            GameObject curPic = PictureList[PictureList.Count - 1];
            curPic.transform.SetParent(transform);
            curPic.transform.localPosition = Vector3.forward; // maybe we can aim this at where the person is looking...
            // curPic.transform.Rotate(new Vector3(0, 0, 180)); // fixes inversion issue TODO: move this down

            Texture updatedTexture = pictureTexture;

            MeshRenderer rend = curPic.transform.GetChild(0).GetComponent<MeshRenderer>(); // TODO: get rid of child
            Texture2D canvasTexture = new Texture2D(1, 1);
            //canvasTexture.LoadImage(ImageSubscriber.ImageData);
            //canvasTexture.Apply();
            rend.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard")); // TODO: move this to resource path constants
            //rend.material.SetTexture("_MainTex", canvasTexture);
            //rend.material = updatedPicture;
            rend.material.SetTexture("_MainTex", updatedTexture);
        }
    }
}
