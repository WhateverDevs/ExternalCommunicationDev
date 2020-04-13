using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstscript : MonoBehaviour
{
    /*
     * ⦁	Cuando el casco y sensores de EEG bien colocados, se pulsa un botón en la GUI de la plataforma de Bioseñales -> envía evento “protocol_start” a la plataforma RV.
⦁	Tarea de calibración individual
⦁	Plataforma RV inicia el protocolo y empieza el intercambio de mensajes.
⦁	Cuando termina el protocolo, plataforma RV envía evento “protocol_end”.

JSON
“DATA”: {
	“ID”: “workload”,
	“BLOCKS”: [
	{
“TIMESTAMP”: ts,
“VALUES”: array of doubles (ch0-s0, ch0-s1, … ch0-sN)
}
	]
}

Protocolo de mensajes

Bitbrain a RV
⦁	Evento “workload”, matriz de 2x1 (valor, calidad) cada 128 ms
⦁	Valor en rango en 0-100; calidad 0-1 (no válida, válida)
⦁	Evento “attention”, matriz de 2x1 (valor, calidad) cada 128 ms
⦁	Valor en rango en 0-100; calidad 0-1 (no válida, válida)
⦁	Evento “protocol_start”, único valor

RV a Bitbrain
⦁	Evento “protocol_marker”, único valor
⦁	Definiríamos una lista de posibles valores de interés
⦁	Evento “protocol_end”, único valor

TCP (puerto 5555) 

     */
/*
 * public IEnumerator StartArchisim()
        {
            yield return LaunchArchisimProcess();

            _entityController = ApplicationManager.Instance.EntityController;
            vehiclesDict.Clear();

            msgARCHISIM = new ArchisimMessage();
            msgARCHISIM.control.IsAlive = false;

            msgDS = new SimulatorMessage();
            msgTrafficLight = new TrafficLightMessage();
            msgPed = new PedestrianMessage();
            //SetCockpitStates();

            //_setPedestrianStates();
            SetupThreads();
            SendFirstMessage();
        }

        public void StopArchisim()
        {
            _entityController = null;
            //_playersController = null;

            SendLastMessage();

            CloseCarThread();
            ClosePedestrianThread();
            CloseTrafficLightThread();

            vehiclesDict.Clear();

            StopArchisimProcess();
        }

        private IEnumerator LaunchArchisimProcess()
        {
            const int maxAttempts = 5;
            const int commandTimeout = 3;

            for (int i = 0; i < maxAttempts && archisimProcess == null; ++i)
            {
                Process[] processes = Process.GetProcessesByName("Dr2");

                if (processes.Length != 0)
                {
                    archisimProcess = processes[0];
                }
                else
                {
                    #if START_ARCHISIM_PROCESS_AUTO
                    string projectFolder = Directory.GetParent(Application.dataPath).FullName;

                    try
                    {
                        ProcessStartInfo cmdStartInfo = new ProcessStartInfo
                                                        {
                                                            FileName = @"C:\Windows\System32\cmd.exe",
                                                            RedirectStandardOutput = true,
                                                            RedirectStandardError = true,
                                                            RedirectStandardInput = true,
                                                            UseShellExecute = false,
                                                            CreateNoWindow = true
                                                        };

                        Process cmdProcess = new Process {StartInfo = cmdStartInfo};
                        cmdProcess.ErrorDataReceived += CmdDataReceived;
                        cmdProcess.OutputDataReceived += CmdDataReceived;
                        cmdProcess.EnableRaisingEvents = true;
                        cmdProcess.Start();
                        cmdProcess.BeginOutputReadLine();
                        cmdProcess.BeginErrorReadLine();

                        cmdProcess.StandardInput.WriteLine("cd " + projectFolder + "/DR2/SbArchi");
                        cmdProcess.StandardInput.WriteLine(configuration.configData.archisimProcessCommand);
                        cmdProcess.StandardInput.WriteLine("exit");
                        if (!cmdProcess.WaitForExit(commandTimeout * 1000)) cmdProcess.Kill();
                    }
                    catch (Exception e)
                    {
                        ITCLDebug.LogError(e.Message);
                    }
                    #endif
                }

                yield return waitOneSecond;
            }
        }

        private void StopArchisimProcess()
        {
            #if START_ARCHISIM_PROCESS_AUTO
            try
            {
                Process[] processes = Process.GetProcessesByName("Dr2");

                if (processes.Length != 0)
                {
                    archisimProcess = processes[0];
                    archisimProcess.Kill();
                }

                //Debug.Log("ARCHISIM: Killed");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            #endif
        }
 */
}
