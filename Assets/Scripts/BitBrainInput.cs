using TMPro;
using UnityEngine;
using WhateverDevs.Core.Runtime.Configuration;
using WhateverDevs.ExternalCommunication.Runtime;
using Zenject;

namespace ExternalCommunicationDev
{
    /// <summary>
    ///     Sample class to test some bitbrain features
    /// </summary>
    public class BitBrainInput : MonoBehaviour
    {
        private BitBrainSampleManager sampleManager;

        public TMP_Text TextMesh;

        [Inject]
        public void Construct(IConfigurationManager configurationManager)
        {
            sampleManager = new BitBrainSampleManager();
            configurationManager.GetConfiguration(out ExternalCommunicationConfigurationData a);
            sampleManager.SetConfigurationData(a);
        }

        /// <summary>
        /// Function to force the functionality of the bitbrain sample
        /// 1: Checkprocess
        /// 2: Setup the threads
        /// 3: SendFirstMessage Id = "protocol_start";
        /// 4: Send init first calibration task Id = "calibration1 (0)";
        /// 5: Send end first calibration task Id = "calibration1 (1)";
        /// 6: Send init second calibration task Id = "calibration2 (0)";
        /// 7: Send end second calibration task Id = "calibration2 (1)";
        /// 8: SendLastMessage Id = "protocol_end";
        /// 9: Closethreads
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) sampleManager.CheckProcess();

            if (Input.GetKeyDown(KeyCode.Alpha2)) sampleManager.SetupThreads();

            if (Input.GetKeyDown(KeyCode.Alpha3)) sampleManager.SendFirstMessage();

            if (Input.GetKeyDown(KeyCode.Alpha4)) sampleManager.Calibration(0, 0);

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                sampleManager.Calibration(0, 1);
                ChangeTask();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6)) sampleManager.Calibration(1, 0);

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                sampleManager.Calibration(1, 1);
                EndCalibration();
            }

            if (Input.GetKeyDown(KeyCode.Alpha8)) sampleManager.SendLastMessage();

            if (Input.GetKeyDown(KeyCode.Alpha9)) sampleManager.CloseThread();
        }

        private void ChangeTask()
        {
            TextMesh.text = "Cuenta atrás de 3 en 3 a partir de " + UnityEngine.Random.Range(600, 999);
        }

        private void EndCalibration()
        {
            TextMesh.text = "Calibración terminada";
            sampleManager.CloseThread();
        }

        /// <summary>
        /// Closing thread on quitting
        /// </summary>
        void OnApplicationQuit()
        {
            sampleManager.CloseThread();
        }

        /// <summary>
        /// Closing thread on disabling 
        /// </summary>
        private void OnDestroy()
        {
            sampleManager.CloseThread();
        }
    }
}