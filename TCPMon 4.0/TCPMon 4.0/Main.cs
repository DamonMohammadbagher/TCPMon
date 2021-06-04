using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace IpHlpApidotnet
{
    public class IPHelper
    {
        private const int NO_ERROR = 0;
        private const int MIB_TCP_STATE_CLOSED = 1;
        private const int MIB_TCP_STATE_LISTEN = 2;
        private const int MIB_TCP_STATE_SYN_SENT = 3;
        private const int MIB_TCP_STATE_SYN_RCVD = 4;
        private const int MIB_TCP_STATE_ESTAB = 5;
        private const int MIB_TCP_STATE_FIN_WAIT1 = 6;
        private const int MIB_TCP_STATE_FIN_WAIT2 = 7;
        private const int MIB_TCP_STATE_CLOSE_WAIT = 8;
        private const int MIB_TCP_STATE_CLOSING = 9;
        private const int MIB_TCP_STATE_LAST_ACK = 10;
        private const int MIB_TCP_STATE_TIME_WAIT = 11;
        private const int MIB_TCP_STATE_DELETE_TCB = 12;




        /*
         * Tcp Struct
         * */

        public IpHlpApidotnet.MIB_TCPTABLE _TcpConnection;
        public IpHlpApidotnet.MIB_TCPSTATS TcpStats;
        public IpHlpApidotnet.MIB_EXTCPTABLE _TcpExConnections;
        public IpHlpApidotnet.MIB_EXTCPTABLE _Internal_TcpExConnections;


        /*
         * Udp Struct
         * */
        public IpHlpApidotnet.MIB_UDPSTATS UdpStats;
        public IpHlpApidotnet.MIB_UDPTABLE _UdpConnection;
        public IpHlpApidotnet.MIB_EXUDPTABLE _UdpExConnection;






        public IPHelper()
        {

        }


        #region Tcp Function

        public void _GetTcpStats()
        {
            try
            {
                TcpStats = new MIB_TCPSTATS();
                IPHlpAPI32Wrapper.GetTcpStatistics(ref TcpStats);

            }
            catch (Exception err)
            {

            }
        }


        public void _GetExTcpConnections()
        {
            try
            {
                // the size of the MIB_EXTCPROW struct =  6*DWORD
                int rowsize = 24;
                int BufferSize = 2000;

                // allocate a dumb memory space in order to retrieve  nb of connexion
                IntPtr lpTable = Marshal.AllocHGlobal(BufferSize);
                //getting infos
                int res = IPHlpAPI32Wrapper.AllocateAndGetTcpExTableFromStack(ref lpTable, true, IPHlpAPI32Wrapper.GetProcessHeap(), 0, 2);
                if (res != NO_ERROR)
                {
                    Debug.WriteLine("Erreur : " + IPHlpAPI32Wrapper.GetAPIErrorMessageDescription(res) + " " + res);
                    return; // Error. You should handle it
                }
                int CurrentIndex = 0;
                //get the number of entries in the table
                int NumEntries = (int)Marshal.ReadIntPtr(lpTable);
                lpTable = IntPtr.Zero;
                // free allocated space in memory
                Marshal.FreeHGlobal(lpTable);



                ///////////////////
                // calculate the real buffer size nb of entrie * size of the struct for each entrie(24) + the dwNumEntries
                BufferSize = (NumEntries * rowsize) + 4;
                // make the struct to hold the resullts
                _TcpExConnections = new IpHlpApidotnet.MIB_EXTCPTABLE();
                _Internal_TcpExConnections = new IpHlpApidotnet.MIB_EXTCPTABLE();
                // Allocate memory
                lpTable = Marshal.AllocHGlobal(BufferSize);
                res = IPHlpAPI32Wrapper.AllocateAndGetTcpExTableFromStack(ref lpTable, true, IPHlpAPI32Wrapper.GetProcessHeap(), 0, 2);
                if (res != NO_ERROR)
                {
                    Debug.WriteLine("error : " + IPHlpAPI32Wrapper.GetAPIErrorMessageDescription(res) + " " + res);
                    return; // Error. You should handle it
                }
                // New pointer of iterating throught the data
                IntPtr current = lpTable;
                CurrentIndex = 0;
                // get the (again) the number of entries
                NumEntries = (int)Marshal.ReadIntPtr(current);
                _TcpExConnections.dwNumEntries = NumEntries;
                // Make the array of entries
                _TcpExConnections.table = new MIB_EXTCPROW[NumEntries];
                // iterate the pointer of 4 (the size of the DWORD dwNumEntries)
                CurrentIndex += 4;
                current = (IntPtr)((int)current + CurrentIndex);
                // for each entries
                for (int i = 0; i < NumEntries; i++)
                {
                    
                        // The state of the connexion (in string)                   
                        _TcpExConnections.table[i].StrgState = this.convert_state((int)Marshal.ReadIntPtr(current));
                        // The state of the connexion (in ID)
                        _TcpExConnections.table[i].iState = (int)Marshal.ReadIntPtr(current);

                        // iterate the pointer of 4
                        current = (IntPtr)((int)current + 4);
                        // get the local address of the connexion
                        UInt32 localAddr = (UInt32)Marshal.ReadIntPtr(current);
                        // iterate the pointer of 4
                        current = (IntPtr)((int)current + 4);
                        // get the local port of the connexion
                        UInt32 localPort = (UInt32)Marshal.ReadIntPtr(current);
                        // iterate the pointer of 4
                        current = (IntPtr)((int)current + 4);
                        // Store the local endpoint in the struct and convertthe port in decimal (ie convert_Port())                       
                        _TcpExConnections.table[i].Local = new IPEndPoint(localAddr, (int)convert_Port(localPort));
                        // get the remote address of the connexion
                        UInt32 RemoteAddr = (UInt32)Marshal.ReadIntPtr(current);
                        // iterate the pointer of 4
                        current = (IntPtr)((int)current + 4);
                        UInt32 RemotePort = 0;
                        // if the remote address = 0 (0.0.0.0) the remote port is always 0
                        // else get the remote port
                        if (RemoteAddr != 0)
                        {
                            RemotePort = (UInt32)Marshal.ReadIntPtr(current);
                            RemotePort = convert_Port(RemotePort);
                        }
                        current = (IntPtr)((int)current + 4);

                        // store the remote endpoint in the struct  and convertthe port in decimal (ie convert_Port())
                        _TcpExConnections.table[i].Remote = new IPEndPoint(RemoteAddr, (int)RemotePort);
                        // store the process ID
                        _TcpExConnections.table[i].dwProcessId = (int)Marshal.ReadIntPtr(current);
                        // Store and get the process name in the struct
                        // _TcpExConnections.table[i].ProcessName = this.get_process_name(_TcpExConnections.table[i].dwProcessId);
                        _TcpExConnections.table[i].ProcessName = "";

                        current = (IntPtr)((int)current + 4);


                        /////////////////////////////////////////////
                        //////if (_TcpExConnections.table[i].iState == 3 || _TcpExConnections.table[i].iState == 5)
                        //////{

                        //////    // The state of the connexion (in string)
                        //////    _Internal_TcpExConnections.table[i].StrgState = this.convert_state((int)Marshal.ReadIntPtr(current));
                        //////    // The state of the connexion (in ID)
                        //////    _Internal_TcpExConnections.table[i].iState = (int)Marshal.ReadIntPtr(current);

                        //////    _Internal_TcpExConnections.table[i].Remote = new IPEndPoint(RemoteAddr, (int)RemotePort);
                        //////    // store the process ID
                        //////    _Internal_TcpExConnections.table[i].dwProcessId = (int)Marshal.ReadIntPtr(current);
                        //////    // Store and get the process name in the struct
                        //////    // _TcpExConnections.table[i].ProcessName = this.get_process_name(_TcpExConnections.table[i].dwProcessId);
                        //////    _Internal_TcpExConnections.table[i].ProcessName = "";
                        //////}
                        ///////////////////////////////////////
                    
                }
                // free the buffer
                Marshal.FreeHGlobal(lpTable);

                // re init the pointer
                current = IntPtr.Zero;
            }
            catch (Exception err)
            {


            }


        }


        public void _GetTcpConnections()
        {
            try
            {
                byte[] buffer = new byte[20000]; // Start with 20.000 bytes left for information about tcp table
                int pdwSize = 20000;
                int res = IPHlpAPI32Wrapper.GetTcpTable(buffer, out pdwSize, true);
                if (res != NO_ERROR)
                {
                    buffer = new byte[pdwSize];
                    res = IPHlpAPI32Wrapper.GetTcpTable(buffer, out pdwSize, true);
                    if (res != 0)
                        return;     // Error. You should handle it
                }

                _TcpConnection = new IpHlpApidotnet.MIB_TCPTABLE();

                int nOffset = 0;
                // number of entry in the
                _TcpConnection.dwNumEntries = Convert.ToInt32(buffer[nOffset]);
                nOffset += 4;
                _TcpConnection.table = new MIB_TCPROW[_TcpConnection.dwNumEntries];

                for (int i = 0; i < _TcpConnection.dwNumEntries; i++)
                {
                    // state
                    int st = Convert.ToInt32(buffer[nOffset]);
                    // state in string
                //    ((MIB_TCPROW)(_TcpConnection.table[i])).StrgState = convert_state(st);
                    // state  by ID
                  //  ((MIB_TCPROW)(_TcpConnection.table[i])).iState = st;
                    nOffset += 4;
                    // local address
                    string LocalAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                    nOffset += 4;
                    //local port in decimal
                    int LocalPort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                        (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);

                    nOffset += 4;
                    // store the remote endpoint
                   // ((MIB_TCPROW)(_TcpConnection.table[i])).Local = new IPEndPoint(IPAddress.Parse(LocalAdrr), LocalPort);

                    // remote address
                    string RemoteAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                    nOffset += 4;
                    // if the remote address = 0 (0.0.0.0) the remote port is always 0
                    // else get the remote port in decimal
                    int RemotePort;
                    //
                    if (RemoteAdrr == "0.0.0.0")
                    {
                        RemotePort = 0;
                    }
                    else
                    {
                        RemotePort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                            (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
                    }
                    nOffset += 4;
                  //  ((MIB_TCPROW)(_TcpConnection.table[i])).Remote = new IPEndPoint(IPAddress.Parse(RemoteAdrr), RemotePort);
                }
            }
            catch (Exception err)
            {


            }

        }


        #endregion

        #region Udp Functions

        public void _GetUdpStats()
        {
            try
            {
                UdpStats = new MIB_UDPSTATS();
                IPHlpAPI32Wrapper.GetUdpStatistics(ref UdpStats);
            }
            catch (Exception err)
            {


            }
        }


        public void _GetUdpConnections()
        {
            try
            {
                byte[] buffer = new byte[20000]; // Start with 20.000 bytes left for information about tcp table
                int pdwSize = 20000;
                int res = IPHlpAPI32Wrapper.GetUdpTable(buffer, out pdwSize, true);
                if (res != NO_ERROR)
                {
                    buffer = new byte[pdwSize];
                    res = IPHlpAPI32Wrapper.GetUdpTable(buffer, out pdwSize, true);
                    if (res != 0)
                        return;     // Error. You should handle it
                }

                _UdpConnection = new IpHlpApidotnet.MIB_UDPTABLE();

                int nOffset = 0;
                // number of entry in the
                _UdpConnection.dwNumEntries = Convert.ToInt32(buffer[nOffset]);
                nOffset += 4;
                _UdpConnection.table = new MIB_UDPROW[_UdpConnection.dwNumEntries];
                for (int i = 0; i < _UdpConnection.dwNumEntries; i++)
                {
                    string LocalAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                    nOffset += 4;

                    int LocalPort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                        (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
                    nOffset += 4;
                  //  ((MIB_UDPROW)(_UdpConnection.table[i])).Local = new IPEndPoint(IPAddress.Parse(LocalAdrr), LocalPort);
                }
            }
            catch (Exception err)
            {


            }

        }


        public void _GetExUdpConnections()
        {
            try
            {
                // the size of the MIB_EXTCPROW struct =  4*DWORD
                int rowsize = 12;
                int BufferSize = 100000;
                // allocate a dumb memory space in order to retrieve  nb of connexion
                IntPtr lpTable = Marshal.AllocHGlobal(BufferSize);
                //getting infos
                int res = IPHlpAPI32Wrapper.AllocateAndGetUdpExTableFromStack(ref lpTable, true, IPHlpAPI32Wrapper.GetProcessHeap(), 0, 2);
                if (res != NO_ERROR)
                {
                    Debug.WriteLine("Erreur : " + IPHlpAPI32Wrapper.GetAPIErrorMessageDescription(res) + " " + res);
                    return; // Error. You should handle it
                }
                int CurrentIndex = 0;
                //get the number of entries in the table
                int NumEntries = (int)Marshal.ReadIntPtr(lpTable);
                lpTable = IntPtr.Zero;
                // free allocated space in memory
                Marshal.FreeHGlobal(lpTable);

                ///////////////////
                // calculate the real buffer size nb of entrie * size of the struct for each entrie(24) + the dwNumEntries
                BufferSize = (NumEntries * rowsize) + 4;
                // make the struct to hold the resullts
                _UdpExConnection = new IpHlpApidotnet.MIB_EXUDPTABLE();
                // Allocate memory
                lpTable = Marshal.AllocHGlobal(BufferSize);
                res = IPHlpAPI32Wrapper.AllocateAndGetUdpExTableFromStack(ref lpTable, true, IPHlpAPI32Wrapper.GetProcessHeap(), 0, 2);
                if (res != NO_ERROR)
                {
                    Debug.WriteLine("Erreur : " + IPHlpAPI32Wrapper.GetAPIErrorMessageDescription(res) + " " + res);
                    return; // Error. You should handle it
                }
                // New pointer of iterating throught the data
                IntPtr current = lpTable;
                CurrentIndex = 0;
                // get the (again) the number of entries
                NumEntries = (int)Marshal.ReadIntPtr(current);
                _UdpExConnection.dwNumEntries = NumEntries;
                // Make the array of entries
                _UdpExConnection.table = new MIB_EXUDPROW[NumEntries];
                // iterate the pointer of 4 (the size of the DWORD dwNumEntries)
                CurrentIndex += 4;
                current = (IntPtr)((int)current + CurrentIndex);
                // for each entries
                for (int i = 0; i < NumEntries; i++)
                {
                    // get the local address of the connexion
                    UInt32 localAddr = (UInt32)Marshal.ReadIntPtr(current);
                    // iterate the pointer of 4
                    current = (IntPtr)((int)current + 4);
                    // get the local port of the connexion
                    UInt32 localPort = (UInt32)Marshal.ReadIntPtr(current);
                    // iterate the pointer of 4
                    current = (IntPtr)((int)current + 4);
                    // Store the local endpoint in the struct and convertthe port in decimal (ie convert_Port())
                    _UdpExConnection.table[i].Local = new IPEndPoint(localAddr, convert_Port(localPort));
                    // store the process ID
                    _UdpExConnection.table[i].dwProcessId = (int)Marshal.ReadIntPtr(current);
                    // Store and get the process name in the struct
                    _UdpExConnection.table[i].ProcessName = this.get_process_name(_UdpExConnection.table[i].dwProcessId);
                    current = (IntPtr)((int)current + 4);

                }
                // free the buffer
                Marshal.FreeHGlobal(lpTable);
                // re init the pointer
                current = IntPtr.Zero;
            }
            catch (Exception err)
            {


            }

        }


        #endregion

        #region helper fct

        private UInt16 convert_Port(UInt32 dwPort)
        {
            byte[] b = new Byte[2];
            try
            {

                // high weight byte
                b[0] = byte.Parse((dwPort >> 8).ToString());
                // low weight byte
                b[1] = byte.Parse((dwPort & 0xFF).ToString());

            }
            catch (Exception err)
            {


            }
            return BitConverter.ToUInt16(b, 0);
        }


        private string convert_state(int state)
        {
            string strg_state = "";
            try
            {

                switch (state)
                {
                    case MIB_TCP_STATE_CLOSED: strg_state = TcpState.Closed.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_LISTEN: strg_state = TcpState.Listen.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_SYN_SENT: strg_state = TcpState.SynSent.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_SYN_RCVD: strg_state = TcpState.SynReceived.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_ESTAB: strg_state = TcpState.Established.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_FIN_WAIT1: strg_state = TcpState.FinWait1.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_FIN_WAIT2: strg_state = TcpState.FinWait2.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_CLOSE_WAIT: strg_state = TcpState.CloseWait.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_CLOSING: strg_state = TcpState.Closing.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_LAST_ACK: strg_state = TcpState.LastAck.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_TIME_WAIT: strg_state = TcpState.TimeWait.ToString().ToUpper(); break;
                    case MIB_TCP_STATE_DELETE_TCB: strg_state = TcpState.DeleteTcb.ToString().ToUpper(); break;
                }

            }
            catch (Exception err)
            {

                return "";
            }
            return strg_state;
        }


        private string get_process_name(int processID)
        {
            //could be an error here if the process die before we can get his name
            try
            {
                Process p = Process.GetProcessById((int)processID);
                return p.ProcessName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "unknown";
            }

        }


        #endregion
    }
}
