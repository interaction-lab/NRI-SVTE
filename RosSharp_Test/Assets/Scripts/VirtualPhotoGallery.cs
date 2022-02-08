using RosSharp.RosBridgeClient;
using NRISVTE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KuriSVTE
{
    public class VirtualPhotoGallery : Singleton<VirtualPhotoGallery>
    {
        List<GameObject> PictureList { get; set; } = new List<GameObject>();
        public Camera pictureCamera;
        public GameObject picture;
        public float zCoord = 0f;

        public void TakePicture()
        {
            // creates picture cube
            PictureList.Add(Instantiate(picture));
            GameObject curPic = PictureList[PictureList.Count - 1];
            curPic.transform.SetParent(transform);

            // changes position of the picture cube
            curPic.transform.localPosition = new Vector3(-0.7f, 0.151f, zCoord);

            // changes rotation of the picture cube
            curPic.transform.localRotation = Quaternion.Euler(0, -90, 0);

            // sets the texture onto the cube
            MeshRenderer rend = curPic.transform.GetChild(0).GetComponent<MeshRenderer>(); // TODO: get rid of child
            Texture2D canvasTexture = new Texture2D(pictureCamera.activeTexture.width, pictureCamera.activeTexture.height);
            RenderTexture.active = pictureCamera.activeTexture;
            canvasTexture.ReadPixels(new Rect(0, 0, pictureCamera.activeTexture.width, pictureCamera.activeTexture.height), 0, 0);
            canvasTexture.Apply();
            RenderTexture.active = null;
            rend.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard")); //             TODO: move this to resource path constants
            rend.material.SetTexture("_MainTex", canvasTexture);

            // increments the z coordinate so each new picture taken is placed side by side
            zCoord -= 0.27f;
        }
    }
}
