using System;
using System.Net;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;


namespace IpHlpApidotnet
{
    #region UDP

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPSTATS
    {
        public int dwInDatagrams;
        public int dwNoPorts;
        public int dwInErrors;
        public int dwOutDatagrams;
        public int dwNumAddrs;
    }

    public struct MIB_UDPTABLE
    {
        public int dwNumEntries;
        public MIB_UDPROW[] table;

    }

    public struct MIB_UDPROW
    {
        public IPEndPoint Local;
    }

    public struct MIB_EXUDPTABLE
    {
        public int dwNumEntries;
        public MIB_EXUDPROW[] table;

    }

    public struct MIB_EXUDPROW
    {
        public IPEndPoint Local;
        public int dwProcessId;
        public string ProcessName;
    }

    #endregion

    #region TCP
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPSTATS
    {
        public int dwRtoAlgorithm;
        public int dwRtoMin;
        public int dwRtoMax;
        public int dwMaxConn;
        public int dwActiveOpens;
        public int dwPassiveOpens;
        public int dwAttemptFails;
        public int dwEstabResets;
        public int dwCurrEstab;
        public int dwInSegs;
        public int dwOutSegs;
        public int dwRetransSegs;
        public int dwInErrs;
        public int dwOutRsts;
        public int dwNumConns;
    }


    public struct MIB_TCPTABLE
    {
        public int dwNumEntries;
        public MIB_TCPROW[] table;

    }

    public struct MIB_TCPROW
    {
        public string StrgState;
        public int iState;
        public IPEndPoint Local;
        public IPEndPoint Remote;
    }

    public struct MIB_EXTCPTABLE
    {
        public int dwNumEntries;
        public MIB_EXTCPROW[] table;

    }

    public struct MIB_EXTCPROW
    {
        public string StrgState;
        public int iState;
        public IPEndPoint Local;
        public IPEndPoint Remote;
        public int dwProcessId;
        public string ProcessName;
    }




    #endregion


    public class IPHlpAPI32Wrapper
    {
        public const byte NO_ERROR = 0;
        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
        public int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER |
            FORMAT_MESSAGE_FROM_SYSTEM |
            FORMAT_MESSAGE_IGNORE_INSERTS;



        [DllImport("iphlpapi.dll", SetLastError = true)]
        public extern static int GetUdpStatistics(ref MIB_UDPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetUdpTable(byte[] UcpTable, out int pdwSize, bool bOrder);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public extern static int GetTcpStatistics(ref MIB_TCPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetTcpTable(byte[] pTcpTable, out int pdwSize, bool bOrder);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public extern static int AllocateAndGetTcpExTableFromStack(ref IntPtr pTable, bool bOrder, IntPtr heap, int zero, int flags);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public extern static int AllocateAndGetUdpExTableFromStack(ref IntPtr pTable, bool bOrder, IntPtr heap, int zero, int flags);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr GetProcessHeap();

        [DllImport("kernel32", SetLastError = true)]
        private static extern int FormatMessage(int flags, IntPtr source, int messageId,
            int languageId, StringBuilder buffer, int size, IntPtr arguments);


        public static string GetAPIErrorMessageDescription(int ApiErrNumber)
        {
            System.Text.StringBuilder sError = new System.Text.StringBuilder(512);
            int lErrorMessageLength;
            lErrorMessageLength = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, (IntPtr)0, ApiErrNumber, 0, sError, sError.Capacity, (IntPtr)0);

            if (lErrorMessageLength > 0)
            {
                string strgError = sError.ToString();
                strgError = strgError.Substring(0, strgError.Length - 2);
                return strgError + " (" + ApiErrNumber.ToString() + ")";
            }
            return "none";

        }










    }
}

