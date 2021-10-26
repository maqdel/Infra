using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using System.Net;

using log4net;


namespace maqdel.Infra.Net
{    

    public class TraceData {
        public IPAddress IP;
        public bool Finded;
        public string MachineName;
    }

    public class NetworkTracer
    {        
        private readonly ILog _logger = LogManager.GetLogger(typeof(NetworkTracer));

        private Task _mainTask;

        private List<TraceData> _traceDataList;

        private IPAddress _initialIP;

        private int _ipA;
        private int _ipB;
        private int _ipC;
        private int _ipD;

        private int _maxIP = 255;

        public bool IsRunning { get; private set; }
        public bool StopRun { get; private set; }

        public NetworkTracer(IPAddress InitialIP)
        {
            this._logger.InfoFormat("NetworkTracer, InitialIP:{0}", InitialIP);
            try
            {
                this._initialIP = InitialIP;
                
                _ipA = this._initialIP.GetAddressBytes()[0];
                _ipB = this._initialIP.GetAddressBytes()[1];
                _ipC = this._initialIP.GetAddressBytes()[2];
                _ipD = this._initialIP.GetAddressBytes()[3];

                //var x = 1;
            }
            catch (Exception ex)
            {
                this._logger.Error("NetworkTracer, Exception:", ex);
                throw ex;
            }
        }

        public void Start()
        {            
            this._logger.InfoFormat("Start");
            try
            {
                this.IsRunning = true;
                this._mainTask = Task.Factory.StartNew(TraceIPData);
            }
            catch (Exception ex)
            {
                this._logger.Error(", Exception:", ex);
                throw ex;
            }            
        }

        private void TraceIPData()
        {            
            this._logger.InfoFormat("TraceIPData");
            try
            {
                this._traceDataList = new List<TraceData>();
                TraceData td;
                for (var a = _ipA; a <= _maxIP; a++)
                {
                    if (StopRun) break;
                    for (var b = _ipB; b <= _maxIP; b++)
                    {
                        if (StopRun) break;
                        for (var c = _ipC; c <= _maxIP; c++)
                        {
                            if (StopRun) break;
                            for (var d = _ipD; d <= _maxIP; d++)
                            {
                                if (StopRun) break;

                                td = new TraceData();

                                td.IP = IPAddress.Parse(a.ToString() + "." + b.ToString() + "." + c.ToString() + "." + d.ToString());

                                td.MachineName = this.GetMachineNameFromIPAddress(td.IP.ToString());

                                if (td.MachineName != string.Empty) {
                                    td.Finded = true; }
                                else {
                                    td.Finded = false; }
                                this._traceDataList.Add(td);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.Error("TraceIPData, Exception:", ex);
                throw ex;
            }
            this.IsRunning = false;
        }

        private string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                this._logger.Error("GetMachineNameFromIPAddress, Exception:", ex);
            }
            return machineName;
        }

        public List<TraceData> GetTracedData()
        {
            List<TraceData> answer = new List<TraceData>();
            this._logger.InfoFormat("GetTracedData");
            try
            {
                answer = _traceDataList;
            }
            catch (Exception ex)
            {
                this._logger.Error("GetTracedData, Exception:", ex);
                throw ex;
            }
            return answer;
        }

        public void Stop()
        {
            this._logger.InfoFormat("Stop");
            try
            {
                this.StopRun = true;
                this.IsRunning = false;
            }
            catch (Exception ex)
            {
                this._logger.Error("Stop, Exception:", ex);
                throw ex;
            }
        }

        //public void method()
        //{
        //    this._logger.InfoFormat("");
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        this._logger.Error(", Exception:", ex);
        //        throw ex;
        //    }
        //}
    }
}
