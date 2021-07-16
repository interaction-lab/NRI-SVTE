/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class ImageSubscriber : UnitySubscriber<MessageTypes.Sensor.CompressedImage>
    {
        public MeshRenderer meshRenderer;
        public MeshRenderer p1;
        public MeshRenderer p2;
        public MeshRenderer p3;
        public MeshRenderer p4;
        public MeshRenderer p5;
        public MeshRenderer[] pictures;

        private Texture2D texture2D;
        private byte[] imageData;
        private bool isMessageReceived;

        private Texture2D canvasTexture;
        private Texture2D[] pictureTextures;
        private byte[] canvasImageData;
        private int currentImage = 0;

        protected override void Start()
        {
			base.Start();
            texture2D = new Texture2D(1, 1);
            canvasTexture = new Texture2D(1, 1);
            pictureTextures = new Texture2D[5];
            pictures = new MeshRenderer[5];
            pictures[0] = p1;
            pictures[1] = p2;
            pictures[2] = p3;
            pictures[3] = p4;
            pictures[4] = p5;
            meshRenderer.material = new Material(Shader.Find("Standard"));
            for(int i = 0; i < 5; i++)
            {
                pictures[i].material = new Material(Shader.Find("Standard"));
                pictures[i].enabled = false;
                pictureTextures[i] = new Texture2D(1, 1);

            }
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.CompressedImage compressedImage)
        {
            imageData = compressedImage.data;
            canvasImageData = compressedImage.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            texture2D.LoadImage(imageData);
            texture2D.Apply();
            meshRenderer.material.SetTexture("_MainTex", texture2D);
            isMessageReceived = false;
        }

        public void SaveImage()
        {
            pictureTextures[currentImage].LoadImage(canvasImageData);
            pictureTextures[currentImage].Apply();
            pictures[currentImage].material.SetTexture("_MainTex", pictureTextures[currentImage]);
            pictures[currentImage].enabled = true;
            nextImage();
        }

        public void nextImage()
        {
            if (currentImage < 4)
                currentImage++;
            else
                currentImage = 0;
        }

    }
}

