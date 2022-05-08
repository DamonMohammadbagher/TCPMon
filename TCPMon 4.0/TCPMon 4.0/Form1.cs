using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using IpHlpApidotnet;
using System.Runtime.InteropServices;
using System.Collections;
using TCPMon_3._1;
using System.Diagnostics;
using BarChart;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TCPMon_3._
{  
    public partial class Form1 : Form
    {
      
        private IpHlpApidotnet.IPHelper MyAPI = new IPHelper();
        public static DataTable dt = new DataTable();
        ColorDialog colorDialog = new ColorDialog();
        public DataSet dataset = new DataSet();
        public DataGridView LoadXML_DataGridView3 = new DataGridView();
        string XMLFileName = "";
        string SafeXMLFileName = "";
        public BackgroundWorker BGW_LoadXMLCore = new BackgroundWorker();
        public void BGW_LoadXMLCore_Method(BackgroundWorker bgw, DoWorkEventArgs e) 
        {
            try
            {
                Thread.Sleep(1);
                PublicClass.Load_XML_TCPIPTable.Rows.Clear();
                PublicClass.saveSchema();
                PublicClass.Load_XML_TCPIPTable.ReadXmlSchema("TempSchema.xml");
                PublicClass.Load_XML_TCPIPTable.ReadXml(XMLFileName);
                LoadXML_DataGridView3.DataSource = null;
                

            }
            catch (Exception err)
            {
                MessageBox.Show(null, err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }
        }
        public static DataRow dr;
        [StructLayout(LayoutKind.Sequential)]
        public struct PIDRows
        {
            public int ID;
            public string ProcessName;
        }
        public struct _Table
        {
            public IPEndPoint LocalIP;
            public int LPORT;
            public IPEndPoint RemoteIP;
            public int RPORT;
            public int states;
            public string  states_String;
            public int PID;
            public string ProcessName;
            public string FullSTR;            
            public int IsLive;
        }       
        public static _Table[] Table1;
        public static _Table[] Table2;
        // x64 code
        public static _Table[] Table1_x64;
        public static _Table[] Table2_x64;
        //x64 code
        public static PIDRows[] PIDTable;
        public ArrayList _PIDTable = new ArrayList();
        public static int Endpoits, Estab, Sync, Listen, TotalLogs;
        public static bool find;
        public void setstatus() 
        {
          //  Endpoint_txt.Text = Endpoits.ToString();
           // Established_txt.Text = Estab.ToString();
        //    Closewait_txt.Text = Closewait.ToString();
        }
        public delegate void _set();
        public static void _set_DatagridTORefresh_Delegate()
        {
            _set_DatagridTORefresh_Delegate();
        }
        public  void _set_DatagridTORefresh() 
        {
            Thread.Sleep(10000);
                       
        }
        public BackgroundWorker bg_Core;
        // version 64bit value 
        //public static TcpRow _X64_TcpRows;
        // version 64bit value 

        public delegate void _BGWMethod(BackgroundWorker _bb, DoWorkEventArgs _ee);
        
        private void Core_Method()
        {
            bool initial = true;
            while (true)
            {
                if (PublicClass.StopThread) 
                {
                    break;
                }
                //////1///////////////////////////////////////////////////////////////////////
                try
                {
                    
                    try
                    {


                        Endpoits = 0;
                        Listen = 0;
                        Sync = 0;
                        Estab = 0;
                       
                        //x64 code
                        int _x64_counter = 0;
                        int _x64_counter_tmp = 0;
                        foreach (TcpRow _X64_TcpRows_tmp in ManagedIpHelper.GetExtendedTcpTable(true))
                        {
                            _x64_counter_tmp++;                           
                        }
                        Table1_x64 = new _Table[_x64_counter_tmp];
                        foreach (TcpRow _X64_TcpRows in ManagedIpHelper.GetExtendedTcpTable(true))
                        {
                            
                            if (PublicClass.StopThread)
                            {
                                Core.Abort();
                                break;
                            }
                            try
                            {
                                switch (PublicClass.Filtering_ISEstablished)
                                {

                                    case false:
                                        { // add to x64 table code
                                            switch (PublicClass.Filtering_IS_127001)
                                            {
                                                case true:
                                                    {
                                                        if (_X64_TcpRows.LocalEndPoint.Address.ToString() != "127.0.0.1")
                                                        {
                                                            Table1_x64[_x64_counter].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table1_x64[_x64_counter].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table1_x64[_x64_counter].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table1_x64[_x64_counter].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table1_x64[_x64_counter].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table1_x64[_x64_counter].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table1_x64[_x64_counter].states = 3;
                                                                    break;
                                                            }
                                                            Table1_x64[_x64_counter].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table1_x64[_x64_counter].PID = _X64_TcpRows.ProcessId;
                                                            Table1_x64[_x64_counter].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table1_x64[_x64_counter].IsLive = 2;
                                                            Table1_x64[_x64_counter].FullSTR = Table1_x64[_x64_counter].LPORT.ToString() + Table1_x64[_x64_counter].RemoteIP.Address.ToString() + Table1_x64[_x64_counter].RPORT.ToString() + Table1_x64[_x64_counter].states.ToString();
                                                        }
                                                        break;
                                                    }
                                                case false:
                                                    {
                                                        Table1_x64[_x64_counter].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                        Table1_x64[_x64_counter].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                        Table1_x64[_x64_counter].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                        Table1_x64[_x64_counter].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                        switch (_X64_TcpRows.State)
                                                        {
                                                            case TcpState.Established: Estab++; Table1_x64[_x64_counter].states = 5;
                                                                break;
                                                            case TcpState.Listen: Listen++; Table1_x64[_x64_counter].states = 2;
                                                                break;
                                                            case TcpState.SynSent: Sync++; Table1_x64[_x64_counter].states = 3;
                                                                break;
                                                        }
                                                        Table1_x64[_x64_counter].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                        Table1_x64[_x64_counter].PID = _X64_TcpRows.ProcessId;
                                                        Table1_x64[_x64_counter].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                        Table1_x64[_x64_counter].IsLive = 2;
                                                        Table1_x64[_x64_counter].FullSTR = Table1_x64[_x64_counter].LPORT.ToString() + Table1_x64[_x64_counter].RemoteIP.Address.ToString() + Table1_x64[_x64_counter].RPORT.ToString() + Table1_x64[_x64_counter].states.ToString();


                                                        break;
                                                    }

                                            }
                                        }
                                        break;
                                    case true: 
                                        {
                                            switch (PublicClass.Filtering_IS_127001)
                                            {
                                                case true:
                                                    {
                                                        if (_X64_TcpRows.LocalEndPoint.Address.ToString() != "127.0.0.1" && _X64_TcpRows.State==TcpState.Established)
                                                        {
                                                            Table1_x64[_x64_counter].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table1_x64[_x64_counter].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table1_x64[_x64_counter].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table1_x64[_x64_counter].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table1_x64[_x64_counter].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table1_x64[_x64_counter].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table1_x64[_x64_counter].states = 3;
                                                                    break;
                                                            }
                                                            Table1_x64[_x64_counter].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table1_x64[_x64_counter].PID = _X64_TcpRows.ProcessId;
                                                            Table1_x64[_x64_counter].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table1_x64[_x64_counter].IsLive = 2;
                                                            Table1_x64[_x64_counter].FullSTR = Table1_x64[_x64_counter].LPORT.ToString() + Table1_x64[_x64_counter].RemoteIP.Address.ToString() + Table1_x64[_x64_counter].RPORT.ToString() + Table1_x64[_x64_counter].states.ToString();
                                                        }
                                                        break;
                                                    }
                                                case false:
                                                    {
                                                        if (_X64_TcpRows.State == TcpState.Established)
                                                        {
                                                            Table1_x64[_x64_counter].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table1_x64[_x64_counter].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table1_x64[_x64_counter].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table1_x64[_x64_counter].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table1_x64[_x64_counter].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table1_x64[_x64_counter].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table1_x64[_x64_counter].states = 3;
                                                                    break;
                                                            }
                                                            Table1_x64[_x64_counter].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table1_x64[_x64_counter].PID = _X64_TcpRows.ProcessId;
                                                            Table1_x64[_x64_counter].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table1_x64[_x64_counter].IsLive = 2;
                                                            Table1_x64[_x64_counter].FullSTR = Table1_x64[_x64_counter].LPORT.ToString() + Table1_x64[_x64_counter].RemoteIP.Address.ToString() + Table1_x64[_x64_counter].RPORT.ToString() + Table1_x64[_x64_counter].states.ToString();
                                                        }

                                                        break;
                                                    }

                                            }
                                        }
                                        break;



                                        
                                }
                                // add to x64 table code
                                //Table1_x64[_x64_counter].LocalIP = _X64_TcpRows.LocalEndPoint;
                                //Table1_x64[_x64_counter].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                //Table1_x64[_x64_counter].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                //Table1_x64[_x64_counter].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                //switch (_X64_TcpRows.State)
                                //                {
                                //                    case TcpState.Established : Estab++;  Table1_x64[_x64_counter].states = 5;
                                //                        break;
                                //                    case TcpState.Listen : Listen++;  Table1_x64[_x64_counter].states = 2;
                                //                        break;
                                //                    case TcpState.SynSent: Sync++; Table1_x64[_x64_counter].states = 3;
                                //                        break;
                                //                }                              
                                //Table1_x64[_x64_counter].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                //Table1_x64[_x64_counter].PID = _X64_TcpRows.ProcessId;
                                //Table1_x64[_x64_counter].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                //Table1_x64[_x64_counter].IsLive = 2;
                                //Table1_x64[_x64_counter].FullSTR = Table1_x64[_x64_counter].LPORT.ToString() + Table1_x64[_x64_counter].RemoteIP.Address.ToString() + Table1_x64[_x64_counter].RPORT.ToString() + Table1_x64[_x64_counter].states.ToString();

                              //  BeginInvoke(new Additem(Additems), Table1_x64[_x64_counter]);
                                if (initial)
                                {
                                    //BeginInvoke(new Additem(Additems), Table1[i]);
                                    BeginInvoke(new Additem(Additems), Table1_x64[_x64_counter]);


                                }
                                   // add to x64 table code 
                            }
                            catch (Exception e)
                            {
                             //   MessageBox.Show(e.Message + "\n" + "Step 1 Level 0-1");
                                //MyAPI._GetExTcpConnections();
                            }
                            _x64_counter++;
                        }
                        // x64 code
                        ////////////////////MyAPI._GetExTcpConnections();
                        ////////////////////Table1 = new _Table[MyAPI._TcpExConnections.dwNumEntries];                        
                        ////////////////////for (int i = 0; i < MyAPI._TcpExConnections.dwNumEntries; i++)
                        ////////////////////{
                        ////////////////////    if (PublicClass.StopThread)
                        ////////////////////    {
                        ////////////////////        Core.Abort();
                        ////////////////////        break;
                        ////////////////////    }
                        ////////////////////    try
                        ////////////////////    {
                        ////////////////////        if (MyAPI._TcpExConnections.table != null)
                        ////////////////////        {
                        ////////////////////            switch (PublicClass.Filtering_ISEstablished)
                        ////////////////////            {
                        ////////////////////                case false:
                        ////////////////////                    if (MyAPI._TcpExConnections.table[i].iState >= 2 && MyAPI._TcpExConnections.table[i].iState <= 5)
                        ////////////////////                    {
                        ////////////////////                        switch (MyAPI._TcpExConnections.table[i].iState)
                        ////////////////////                        {
                        ////////////////////                            case 5: Estab++;
                        ////////////////////                                break;
                        ////////////////////                            case 2: Listen++;
                        ////////////////////                                break;
                        ////////////////////                            case 3: Sync++;
                        ////////////////////                                break;
                        ////////////////////                        }

                        ////////////////////                        switch (PublicClass.Filtering_IS_127001)
                        ////////////////////                        {
                        ////////////////////                            case true:
                        ////////////////////                                if (MyAPI._TcpExConnections.table[i].Local.Address.ToString() != "127.0.0.1" & MyAPI._TcpExConnections.table[i].Remote.Address.ToString() != "127.0.0.1")
                        ////////////////////                                {
                        ////////////////////                                    Table1[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////////                                    Table1[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////////                                    Table1[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////////                                    Table1[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////////                                    Table1[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////////                                    Table1[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////////                                    Table1[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////////                                    Table1[i].ProcessName = PublicClass.SetRow(Table1[i].PID);
                        ////////////////////                                    Table1[i].IsLive = 2;
                        ////////////////////                                    Table1[i].FullSTR = Table1[i].LPORT.ToString() + Table1[i].RemoteIP.Address.ToString() + Table1[i].RPORT.ToString() + Table1[i].states.ToString();
                        ////////////////////                                }
                        ////////////////////                                //System.Diagnostics.Debug.WriteLine(Table1[i].FullSTR);
                        ////////////////////                                break;
                        ////////////////////                            case false:
                        ////////////////////                                Table1[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////////                                Table1[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////////                                Table1[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////////                                Table1[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////////                                Table1[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////////                                Table1[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////////                                Table1[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////////                                Table1[i].ProcessName = PublicClass.SetRow(Table1[i].PID);
                        ////////////////////                                Table1[i].IsLive = 2;
                        ////////////////////                                Table1[i].FullSTR = Table1[i].LPORT.ToString() + Table1[i].RemoteIP.Address.ToString() + Table1[i].RPORT.ToString() + Table1[i].states.ToString();
                        ////////////////////                                //System.Diagnostics.Debug.WriteLine(Table1[i].FullSTR);
                        ////////////////////                                break;
                        ////////////////////                        }
                        ////////////////////                    }
                        ////////////////////                    break;
                        ////////////////////                case true:
                        ////////////////////                    if (MyAPI._TcpExConnections.table[i].iState == 5)
                        ////////////////////                    {
                        ////////////////////                        switch (MyAPI._TcpExConnections.table[i].iState)
                        ////////////////////                        {
                        ////////////////////                            case 5: Estab++;
                        ////////////////////                                break;
                        ////////////////////                            case 2: Listen++;
                        ////////////////////                                break;
                        ////////////////////                            case 3: Sync++;
                        ////////////////////                                break;
                        ////////////////////                        }

                        ////////////////////                        switch (PublicClass.Filtering_IS_127001)
                        ////////////////////                        {
                        ////////////////////                            case true:
                        ////////////////////                                if (MyAPI._TcpExConnections.table[i].Local.Address.ToString() != "127.0.0.1" & MyAPI._TcpExConnections.table[i].Remote.Address.ToString() != "127.0.0.1")
                        ////////////////////                                {
                        ////////////////////                                    Table1[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////////                                    Table1[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////////                                    Table1[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////////                                    Table1[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////////                                    Table1[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////////                                    Table1[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////////                                    Table1[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////////                                    Table1[i].ProcessName = PublicClass.SetRow(Table1[i].PID);
                        ////////////////////                                    Table1[i].IsLive = 2;
                        ////////////////////                                    Table1[i].FullSTR = Table1[i].LPORT.ToString() + Table1[i].RemoteIP.Address.ToString() + Table1[i].RPORT.ToString() + Table1[i].states.ToString();
                        ////////////////////                                }
                        ////////////////////                                //System.Diagnostics.Debug.WriteLine(Table1[i].FullSTR);
                        ////////////////////                                break;
                        ////////////////////                            case false:
                        ////////////////////                                Table1[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////////                                Table1[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////////                                Table1[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////////                                Table1[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////////                                Table1[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////////                                Table1[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////////                                Table1[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////////                                Table1[i].ProcessName = PublicClass.SetRow(Table1[i].PID);
                        ////////////////////                                Table1[i].IsLive = 2;
                        ////////////////////                                Table1[i].FullSTR = Table1[i].LPORT.ToString() + Table1[i].RemoteIP.Address.ToString() + Table1[i].RPORT.ToString() + Table1[i].states.ToString();
                        ////////////////////                                //System.Diagnostics.Debug.WriteLine(Table1[i].FullSTR);
                        ////////////////////                                break;
                        ////////////////////                        }
                        ////////////////////                    }
                        ////////////////////                    break;
                        ////////////////////            }

                                   

                        ////////////////////        }


                        ////////////////////        if (initial)
                        ////////////////////        {
                        ////////////////////            BeginInvoke(new Additem(Additems), Table1[i]);
                                   

                        ////////////////////        }
                        ////////////////////    }
                        ////////////////////    catch (Exception err)
                        ////////////////////    {
                        ////////////////////       //
                        ////////////////////      //  MessageBox.Show(err.Message + "\n" + "Loop 1 Step 1 Level 1");
                        ////////////////////      //
                        ////////////////////    }

                        ////////////////////}
                    }
                    catch (Exception err)
                    {
                        //
                       // MessageBox.Show(err.Message + "\n" + "Step 1 Level 1");
                    }
                    //////2///////////////////////////////////////////////////////////////////////
                    //  BeginInvoke(new _set(setstatus));
                    //////3///////////////////////////////////////////////////////////////////////
                    //try
                    //{

                    //    if (!initial)
                    //    {
                    //        for (int i = 0; i < Table1.Length; i++)
                    //        {
                    //            if (PublicClass.StopThread)
                    //            {
                    //                Core.Abort();
                    //                break;
                    //            }
                    //            for (int b = 0; b < Table2.Length; b++)
                    //            {
                    //                if (Table1[i].IsLive != 1)
                    //                {
                    //                    if (Table1[i].FullSTR == Table2[b].FullSTR)
                    //                    {
                    //                        Table2[b].IsLive = 1;
                    //                        Table1[i].IsLive = 1;
                    //                        // break;
                    //                    }
                    //                }
                    //            }
                    //            if (Table1[i].IsLive != 1)
                    //            {
                    //                try
                    //                {
                    //                    //add table2[i] to listview                           
                    //                    BeginInvoke(new Additem(Additems), Table1[i]);
                    //                    Table1[i].IsLive = 1;
                    //                }
                    //                catch (Exception err)
                    //                {


                    //                }

                    //            }
                    //            Thread.Sleep(1);
                    //        }
                    //    }

                    //}
                    //catch (Exception err)
                    //{

                    //  //  MessageBox.Show(err.Message + "\n" + "Step 3 Level 1");
                    //}


                    try
                    {

                        if (!initial)
                        {
                            for (int i = 0; i < Table1_x64.Length; i++)
                            {
                                if (PublicClass.StopThread)
                                {
                                    Core.Abort();
                                    break;
                                }
                                for (int b = 0; b < Table2_x64.Length; b++)
                                {
                                    if (Table1_x64[i].IsLive != 1)
                                    {
                                        if (Table1_x64[i].FullSTR == Table2_x64[b].FullSTR)
                                        {
                                            Table2_x64[b].IsLive = 1;
                                            Table1_x64[i].IsLive = 1;
                                            // break;
                                        }
                                    }
                                }
                                if (Table1_x64[i].IsLive != 1)
                                {
                                    try
                                    {
                                        //add table2[i] to listview                           
                                        BeginInvoke(new Additem(Additems), Table1_x64[i]);
                                        Table1_x64[i].IsLive = 1;
                                    }
                                    catch (Exception err)
                                    {
                                      //  MessageBox.Show(err.Message + "\n" + "Step 1 Level 1-1");

                                    }

                                }
                                Thread.Sleep(1);
                            }
                        }

                    }
                    catch (Exception err)
                    {

                      //    MessageBox.Show(err.Message + "\n" + "Step 3 Level 2-1");
                    }

                    //////3///////////////////////////////////////////////////////////////////////

                    //////4///////////////////////////////////////////////////////////////////////

                    initial = false;

                    try
                    {
                        Before_after = 0;
                        BeginInvoke(new DeleteItem(_DeleteItems), (object)1);
                    }
                    catch (Exception err)
                    {
                        //
                     //   MessageBox.Show(err.Message + "\n" + "Step 4 Level 1");
                    }

                    BeginInvoke(new information(Information));
                   // Table2 = null;
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    Thread.Sleep(1);
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////
                    Endpoits = 0;
                    Listen = 0;
                    Sync = 0;
                    Estab = 0;
                    //////2///////////////////////////////////////////////////////////////////////
                    try
                    {

                        //x64 code
                        int _x64_counter_2 = 0;
                        int _x64_counter_tmp_2 = 0;
                        foreach (TcpRow _X64_TcpRows_tmp in ManagedIpHelper.GetExtendedTcpTable(true))
                        {
                            _x64_counter_tmp_2++;
                            
                        }
                        Table2_x64 = new _Table[_x64_counter_tmp_2];

                        foreach (TcpRow _X64_TcpRows in ManagedIpHelper.GetExtendedTcpTable(true))
                        {
                           
                            if (PublicClass.StopThread)
                            {
                                Core.Abort();
                                break;
                            }
                            try
                            {
                                switch (PublicClass.Filtering_ISEstablished)
                                {

                                    case false:
                                        { // add to x64 table code
                                            switch (PublicClass.Filtering_IS_127001)
                                            {
                                                case true: 
                                                    {
                                                        if (_X64_TcpRows.LocalEndPoint.Address.ToString() != "127.0.0.1")
                                                        {
                                                            // add to x64 table code
                                                            Table2_x64[_x64_counter_2].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table2_x64[_x64_counter_2].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table2_x64[_x64_counter_2].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table2_x64[_x64_counter_2].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table2_x64[_x64_counter_2].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table2_x64[_x64_counter_2].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table2_x64[_x64_counter_2].states = 3;
                                                                    break;
                                                            }
                                                            Table2_x64[_x64_counter_2].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table2_x64[_x64_counter_2].PID = _X64_TcpRows.ProcessId;
                                                            Table2_x64[_x64_counter_2].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table2_x64[_x64_counter_2].IsLive = 2;
                                                            Table2_x64[_x64_counter_2].FullSTR = Table1_x64[_x64_counter_2].LPORT.ToString() + Table1_x64[_x64_counter_2].RemoteIP.Address.ToString() + Table1_x64[_x64_counter_2].RPORT.ToString() + Table1_x64[_x64_counter_2].states.ToString();
                                                        }
                                                        break; 
                                                    }
                                                case false:
                                                    {
                                                        Table2_x64[_x64_counter_2].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                        Table2_x64[_x64_counter_2].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                        Table2_x64[_x64_counter_2].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                        Table2_x64[_x64_counter_2].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                        switch (_X64_TcpRows.State)
                                                        {
                                                            case TcpState.Established: Estab++; Table2_x64[_x64_counter_2].states = 5;
                                                                break;
                                                            case TcpState.Listen: Listen++; Table2_x64[_x64_counter_2].states = 2;
                                                                break;
                                                            case TcpState.SynSent: Sync++; Table2_x64[_x64_counter_2].states = 3;
                                                                break;
                                                        }
                                                        Table2_x64[_x64_counter_2].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                        Table2_x64[_x64_counter_2].PID = _X64_TcpRows.ProcessId;
                                                        Table2_x64[_x64_counter_2].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                        Table2_x64[_x64_counter_2].IsLive = 2;
                                                        Table2_x64[_x64_counter_2].FullSTR = Table1_x64[_x64_counter_2].LPORT.ToString() + Table1_x64[_x64_counter_2].RemoteIP.Address.ToString() + Table1_x64[_x64_counter_2].RPORT.ToString() + Table1_x64[_x64_counter_2].states.ToString();
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    case true:
                                        { // add to x64 table code
                                            switch (PublicClass.Filtering_IS_127001)
                                            {
                                                case true:
                                                    {
                                                        if (_X64_TcpRows.LocalEndPoint.Address.ToString() != "127.0.0.1" && _X64_TcpRows.State == TcpState.Established)
                                                        {
                                                            Table2_x64[_x64_counter_2].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table2_x64[_x64_counter_2].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table2_x64[_x64_counter_2].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table2_x64[_x64_counter_2].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table2_x64[_x64_counter_2].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table2_x64[_x64_counter_2].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table2_x64[_x64_counter_2].states = 3;
                                                                    break;
                                                            }
                                                            Table2_x64[_x64_counter_2].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table2_x64[_x64_counter_2].PID = _X64_TcpRows.ProcessId;
                                                            Table2_x64[_x64_counter_2].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table2_x64[_x64_counter_2].IsLive = 2;
                                                            Table2_x64[_x64_counter_2].FullSTR = Table1_x64[_x64_counter_2].LPORT.ToString() + Table1_x64[_x64_counter_2].RemoteIP.Address.ToString() + Table1_x64[_x64_counter_2].RPORT.ToString() + Table1_x64[_x64_counter_2].states.ToString();
                                                        }
                                                        break;
                                                    }
                                                case false:
                                                    {
                                                        if (_X64_TcpRows.State == TcpState.Established)
                                                        {
                                                            Table2_x64[_x64_counter_2].LocalIP = _X64_TcpRows.LocalEndPoint;
                                                            Table2_x64[_x64_counter_2].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                                            Table2_x64[_x64_counter_2].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                                            Table2_x64[_x64_counter_2].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                                            switch (_X64_TcpRows.State)
                                                            {
                                                                case TcpState.Established: Estab++; Table2_x64[_x64_counter_2].states = 5;
                                                                    break;
                                                                case TcpState.Listen: Listen++; Table2_x64[_x64_counter_2].states = 2;
                                                                    break;
                                                                case TcpState.SynSent: Sync++; Table2_x64[_x64_counter_2].states = 3;
                                                                    break;
                                                            }
                                                            Table2_x64[_x64_counter_2].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                                            Table2_x64[_x64_counter_2].PID = _X64_TcpRows.ProcessId;
                                                            Table2_x64[_x64_counter_2].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                                            Table2_x64[_x64_counter_2].IsLive = 2;
                                                            Table2_x64[_x64_counter_2].FullSTR = Table1_x64[_x64_counter_2].LPORT.ToString() + Table1_x64[_x64_counter_2].RemoteIP.Address.ToString() + Table1_x64[_x64_counter_2].RPORT.ToString() + Table1_x64[_x64_counter_2].states.ToString();
                                                        }
                                                        break;
                                                    }
                                            }
                                        } 
                                        break;
                                }
                                // add to x64 table code
                                //Table2_x64[_x64_counter_2].LocalIP = _X64_TcpRows.LocalEndPoint;
                                //Table2_x64[_x64_counter_2].LPORT = _X64_TcpRows.LocalEndPoint.Port;
                                //Table2_x64[_x64_counter_2].RemoteIP = _X64_TcpRows.RemoteEndPoint;
                                //Table2_x64[_x64_counter_2].RPORT = _X64_TcpRows.RemoteEndPoint.Port;
                                //switch (_X64_TcpRows.State)
                                //{
                                //    case TcpState.Established: Estab++; Table2_x64[_x64_counter_2].states = 5;
                                //        break;
                                //    case TcpState.Listen: Listen++; Table2_x64[_x64_counter_2].states = 2;
                                //        break;
                                //    case TcpState.SynSent: Sync++; Table2_x64[_x64_counter_2].states = 3;
                                //        break;
                                //}
                                //Table2_x64[_x64_counter_2].states_String = _X64_TcpRows.State.ToString().ToUpper();
                                //Table2_x64[_x64_counter_2].PID = _X64_TcpRows.ProcessId;
                                //Table2_x64[_x64_counter_2].ProcessName = PublicClass.SetRow(_X64_TcpRows.ProcessId);
                                //Table2_x64[_x64_counter_2].IsLive = 2;
                                //Table2_x64[_x64_counter_2].FullSTR = Table1_x64[_x64_counter_2].LPORT.ToString() + Table1_x64[_x64_counter_2].RemoteIP.Address.ToString() + Table1_x64[_x64_counter_2].RPORT.ToString() + Table1_x64[_x64_counter_2].states.ToString();

                              //  BeginInvoke(new Additem(Additems), Table2_x64[_x64_counter_2]);
                                // add to x64 table code 
                            }
                            catch (Exception e)
                            {
                            //    MessageBox.Show(e.Message + "\n" + "Step 1 Level 2");

                            }
                            _x64_counter_2++;
                        }
                        // x64 code

                        ////////////////MyAPI._GetExTcpConnections();
                        ////////////////Table2 = new _Table[MyAPI._TcpExConnections.dwNumEntries];
                        ////////////////for (int i = 0; i < MyAPI._TcpExConnections.dwNumEntries; i++)
                        ////////////////{
                        ////////////////    if (PublicClass.StopThread)
                        ////////////////    {
                        ////////////////        Core.Abort();
                        ////////////////        break;
                        ////////////////    }
                        ////////////////    try
                        ////////////////    {
                        ////////////////        if (MyAPI._TcpExConnections.table != null)
                        ////////////////        {
                        ////////////////            switch (PublicClass.Filtering_ISEstablished)
                        ////////////////            {
                        ////////////////                case false:
                        ////////////////                    if (MyAPI._TcpExConnections.table[i].iState >= 2 && MyAPI._TcpExConnections.table[i].iState <= 5)
                        ////////////////                    {
                        ////////////////                        switch (MyAPI._TcpExConnections.table[i].iState)
                        ////////////////                        {
                        ////////////////                            case 5: Estab++;
                        ////////////////                                break;
                        ////////////////                            case 2: Listen++;
                        ////////////////                                break;
                        ////////////////                            case 3: Sync++;
                        ////////////////                                break;
                        ////////////////                        }

                        ////////////////                        switch (PublicClass.Filtering_IS_127001)
                        ////////////////                        {
                        ////////////////                            case true:
                        ////////////////                                if (MyAPI._TcpExConnections.table[i].Local.Address.ToString() != "127.0.0.1" & MyAPI._TcpExConnections.table[i].Remote.Address.ToString() != "127.0.0.1")
                        ////////////////                                {
                        ////////////////                                    Table2[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////                                    Table2[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////                                    Table2[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////                                    Table2[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////                                    Table2[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////                                    Table2[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////                                    Table2[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////                                    Table2[i].ProcessName = PublicClass.SetRow(Table2[i].PID);
                        ////////////////                                    Table2[i].IsLive = 0;
                        ////////////////                                    Table2[i].FullSTR = Table2[i].LPORT.ToString() + Table2[i].RemoteIP.Address.ToString() + Table2[i].RPORT.ToString() + Table2[i].states.ToString();
                        ////////////////                                    // System.Diagnostics.Debug.WriteLine(Table2[i].FullSTR);
                        ////////////////                                }
                        ////////////////                                break;
                        ////////////////                            case false:

                        ////////////////                                Table2[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////                                Table2[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////                                Table2[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////                                Table2[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////                                Table2[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////                                Table2[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////                                Table2[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////                                Table2[i].ProcessName = PublicClass.SetRow(Table2[i].PID);
                        ////////////////                                Table2[i].IsLive = 0;
                        ////////////////                                Table2[i].FullSTR = Table2[i].LPORT.ToString() + Table2[i].RemoteIP.Address.ToString() + Table2[i].RPORT.ToString() + Table2[i].states.ToString();
                        ////////////////                                // System.Diagnostics.Debug.WriteLine(Table2[i].FullSTR);

                        ////////////////                                break;


                        ////////////////                        }
                                               
                        ////////////////                    }
                        ////////////////                    break;
                        ////////////////                case true:
                        ////////////////                    if (MyAPI._TcpExConnections.table[i].iState == 5)
                        ////////////////                    {
                        ////////////////                        switch (MyAPI._TcpExConnections.table[i].iState)
                        ////////////////                        {
                        ////////////////                            case 5: Estab++;
                        ////////////////                                break;
                        ////////////////                            case 2: Listen++;
                        ////////////////                                break;
                        ////////////////                            case 3: Sync++;
                        ////////////////                                break;
                        ////////////////                        }

                        ////////////////                        switch (PublicClass.Filtering_IS_127001)
                        ////////////////                        {
                        ////////////////                            case true:
                        ////////////////                                if (MyAPI._TcpExConnections.table[i].Local.Address.ToString() != "127.0.0.1" & MyAPI._TcpExConnections.table[i].Remote.Address.ToString() != "127.0.0.1")
                        ////////////////                                {
                        ////////////////                                    Table2[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////                                    Table2[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////                                    Table2[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////                                    Table2[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////                                    Table2[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////                                    Table2[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////                                    Table2[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////                                    Table2[i].ProcessName = PublicClass.SetRow(Table2[i].PID);
                        ////////////////                                    Table2[i].IsLive = 0;
                        ////////////////                                    Table2[i].FullSTR = Table2[i].LPORT.ToString() + Table2[i].RemoteIP.Address.ToString() + Table2[i].RPORT.ToString() + Table2[i].states.ToString();
                        ////////////////                                    // System.Diagnostics.Debug.WriteLine(Table2[i].FullSTR);
                        ////////////////                                }
                        ////////////////                                break;
                        ////////////////                            case false:

                        ////////////////                                Table2[i].LocalIP = MyAPI._TcpExConnections.table[i].Local;
                        ////////////////                                Table2[i].LPORT = MyAPI._TcpExConnections.table[i].Local.Port;
                        ////////////////                                Table2[i].RemoteIP = MyAPI._TcpExConnections.table[i].Remote;
                        ////////////////                                Table2[i].RPORT = MyAPI._TcpExConnections.table[i].Remote.Port;
                        ////////////////                                Table2[i].states = MyAPI._TcpExConnections.table[i].iState;
                        ////////////////                                Table2[i].states_String = MyAPI._TcpExConnections.table[i].StrgState;
                        ////////////////                                Table2[i].PID = MyAPI._TcpExConnections.table[i].dwProcessId;
                        ////////////////                                Table2[i].ProcessName = PublicClass.SetRow(Table2[i].PID);
                        ////////////////                                Table2[i].IsLive = 0;
                        ////////////////                                Table2[i].FullSTR = Table2[i].LPORT.ToString() + Table2[i].RemoteIP.Address.ToString() + Table2[i].RPORT.ToString() + Table2[i].states.ToString();
                        ////////////////                                // System.Diagnostics.Debug.WriteLine(Table2[i].FullSTR);

                        ////////////////                                break;
                        ////////////////                        }
                        ////////////////                    }
                        ////////////////                    break;
                        ////////////////            }

                                  
                        ////////////////        }
                        ////////////////    }
                        ////////////////    catch (Exception err)
                        ////////////////    {
                        ////////////////        //
                        ////////////////     //   MessageBox.Show(err.Message + "\n" + "Loop 1 Step 1 Level 2");
                        ////////////////    }
                        ////////////////}
                    }
                    catch (Exception err)
                    {

                     //   MessageBox.Show(err.Message + "\n" + "Step 1 Level 1-2");
                    }
                    // Endpoits = Table1.Length;
                    //////2///////////////////////////////////////////////////////////////////////
                    //  BeginInvoke(new _set(setstatus));
                    //////3///////////////////////////////////////////////////////////////////////

                    try
                    {

                        for (int i = 0; i < Table2_x64.Length; i++)
                        {
                            if (PublicClass.StopThread)
                            {
                                Core.Abort();
                                break;
                            }
                            for (int b = 0; b < Table1_x64.Length; b++)
                            {
                                if (Table2_x64[i].IsLive != 1)
                                {
                                    if (Table2_x64[i].FullSTR == Table1_x64[b].FullSTR)
                                    {
                                        Table1_x64[b].IsLive = 1;
                                        Table2_x64[i].IsLive = 1;
                                        
                                    }
                                }
                            }
                            if (Table2_x64[i].IsLive != 1)
                            {
                                try
                                {
                                    //add table2[i] to listview                           
                                    BeginInvoke(new Additem(Additems), Table2_x64[i]);
                                    Table2_x64[i].IsLive = 1;
                                }
                                catch (Exception err)
                                {


                                }

                            }
                           // Thread.Sleep(1);
                        }
                    }
                    catch (Exception err)
                    {

                    //     MessageBox.Show(err.Message + "\n" + "Step 3 Level 3-2");
                    }

                    //try
                    //{

                    //    for (int i = 0; i < Table2.Length; i++)
                    //    {
                    //        if (PublicClass.StopThread)
                    //        {
                    //            Core.Abort();
                    //            break;
                    //        }
                    //        for (int b = 0; b < Table1.Length; b++)
                    //        {
                    //            if (Table2[i].IsLive != 1)
                    //            {
                    //                if (Table2[i].FullSTR == Table1[b].FullSTR)
                    //                {
                    //                    Table1[b].IsLive = 1;
                    //                    Table2[i].IsLive = 1;
                    //                   //  break;
                    //                }
                    //            }
                    //        }
                    //        if (Table2[i].IsLive != 1)
                    //        {
                    //            try
                    //            {
                    //                //add table2[i] to listview                           
                    //                BeginInvoke(new Additem(Additems), Table2[i]);
                    //                Table2[i].IsLive = 1;
                    //            }
                    //            catch (Exception err)
                    //            {


                    //            }

                    //        }
                    //        Thread.Sleep(1);
                    //    }
                    //}
                    //catch (Exception err)
                    //{

                    //  //  MessageBox.Show(err.Message + "\n" + "Step 3 Level 2");
                    //}
                    //////3///////////////////////////////////////////////////////////////////////

                    //////4///////////////////////////////////////////////////////////////////////

                    try
                    {
                        Before_after = 1;
                        BeginInvoke(new DeleteItem(_DeleteItems), (object)1);
                    }
                    catch (Exception err)
                    {
                       // MessageBox.Show(err.Message + "\n" + "Step 4 Level 4-2");

                    }
                    //Table1 = null;

                    BeginInvoke(new information(Information));
                    Thread.Sleep(10);
                    
                }
                catch (Exception err)
                {

                 //   MessageBox.Show(err.Message + "\n" + "Loop for Core Method");
                }
            }
        }
        ListViewItem Items;
        private BarChart.HBarChart barChart;
        private Thread Core;
        public static int Before_after;
        public static int PublicID;
        public void _DeleteItems(object id)
        {
            try
            {
                if (Before_after == 1)
                {
                    for (int w = 0; w < listView1.Items.Count; w++)
                    {
                        int found = 0;
                        for (int x = 0; x < Table2_x64.Length; x++)
                        {
                            if (listView1.Items[w].Name == Table2_x64[x].FullSTR)
                            {
                                found = 1;
                            }
                        }
                        if (found == 0)
                        {

                            listView1.Items[w].Remove();
                        }
                    }

                }
                else if (Before_after == 0)
                {
                    for (int w = 0; w < listView1.Items.Count; w++)
                    {
                        int found = 0;
                        for (int x = 0; x < Table1_x64.Length; x++)
                        {
                            if (listView1.Items[w].Name == Table1_x64[x].FullSTR)
                            {
                                found = 1;
                            }
                        }
                        if (found == 0)
                        {

                            listView1.Items[w].Remove();
                        }
                    }
                }
              
            }
            catch (Exception em)
            {
                
                
            }
          
            
        }
        public delegate void DeleteItem(object id);
        public static string PROCESSNAME;
        private void Additems(object Table_to_Add)
        {
            
            try
            {

                if (((_Table)Table_to_Add).LocalIP != null)
                {
                    PublicClass.GetRowsTODataTable(DateTime.Now, ((_Table)Table_to_Add).LocalIP.Address.ToString(), ((_Table)Table_to_Add).LPORT.ToString(), ((_Table)Table_to_Add).RemoteIP.Address.ToString(), ((_Table)Table_to_Add).RPORT.ToString(), ((_Table)Table_to_Add).states_String, ((_Table)Table_to_Add).states, ((_Table)Table_to_Add).PID, ((_Table)Table_to_Add).ProcessName);
                    Thread.Sleep(2);
                    Items = new ListViewItem();
                    Items.Name = ((_Table)Table_to_Add).FullSTR;
                    Items.SubItems.Add(DateTime.Now.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).LocalIP.Address.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).LocalIP.Port.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).RemoteIP.Address.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).RemoteIP.Port.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).states_String.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).PID.ToString());
                    Items.SubItems.Add(((_Table)Table_to_Add).ProcessName);
                    Items.SubItems.Add(((_Table)Table_to_Add).IsLive.ToString());
                    listView1.Items.Add(Items);
                }
            }
            catch (Exception err)
            {
                
                
            }
           
        }
        public delegate void Additem(object Table_to_Add);
        public delegate void information();
        public void Information()
        {
            try
            {
                Estab_label.Text = Estab.ToString();
                Sync_label.Text = Sync.ToString();
                Total_label.Text = Table1_x64.Length.ToString();
                Totallog_Label.Text = PublicClass.TCPIPTable.Rows.Count.ToString();
            }
            catch (Exception emm)
            {
                
              
            }
           
        }
        public Form1()
        {
            
            InitializeComponent();
            
           
            BGW_LoadXMLCore.DoWork += new DoWorkEventHandler(BGW_LoadXMLCore_DoWork);
            BGW_LoadXMLCore.ProgressChanged += new ProgressChangedEventHandler(BGW_LoadXMLCore_ProgressChanged);
            BGW_LoadXMLCore.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_LoadXMLCore_RunWorkerCompleted);
            BGW_LoadXMLCore.WorkerReportsProgress = true;
            BGW_LoadXMLCore.WorkerSupportsCancellation = true;

            try
            {
                PublicClass.IsLogActive = false;
                barChart = new BarChart.HBarChart();
                this.panel1.Controls.Add(barChart);
                barChart.Dock = DockStyle.Fill;

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
        
        //private void bg_set_to_delete(BackgroundWorker bgw,DoWorkEventArgs e,int id) 
        //{
        //    bgw.ReportProgress(0, (object) id);           
        //}
       
        //void bg_Core_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
          
        //}

        //void bg_Core_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    if (Before_after == 1)
        //    {
        //        for (int w = 0; w < listView1.Items.Count; w++)
        //        {
        //            int found = 0;
        //            for (int x = 0; x < Table2.Length; x++)
        //            {
        //                if (listView1.Items[w].Name == Table2[x].FullSTR)
        //                {
        //                    found = 1;
        //                }
        //            }
        //            if (found == 0)
        //            {
        //                // listView1.BeginUpdate();
        //                listView1.Items[w].Remove();
        //                //  listView1.EndUpdate();
        //            }
        //        }

        //    }
        //    else if (Before_after == 0) 
        //    {
        //        for (int w = 0; w < listView1.Items.Count; w++)
        //        {
        //            int found = 0;
        //            for (int x = 0; x < Table1.Length; x++)
        //            {
        //                if (listView1.Items[w].Name == Table1[x].FullSTR)
        //                {
        //                    found = 1;
        //                }
        //            }
        //            if (found == 0)
        //            {
        //                // listView1.BeginUpdate();
        //                listView1.Items[w].Remove();
        //                //  listView1.EndUpdate();
        //            }
        //        }
        //    }
        //}

        //void bg_Core_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    bg_set_to_delete(bg_Core, e,PublicID);         
        //}

        
        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView2.AllowUserToOrderColumns = false;    
                   
            PublicClass.StopThread = false;
            PublicClass.IsProcessNameActive = true;
            // set datesoure for Datagrid "log view"
            PublicClass.TCPIP_settable();
            //dataGridView2.DataSource = PublicClass.TCPIPTable;
            //
            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = false;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;
            listView1.Columns.Add(" ", 1, HorizontalAlignment.Left);
            listView1.Columns.Add("Time", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Local IP", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Port", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("Remote IP", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Port", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("State", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("PID", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Process Name", 100, HorizontalAlignment.Left);
            

          //  bg_Core.RunWorkerAsync();
            PublicClass.Filtering_IS_127001 = true;
            FL2.Text = "Don't View 127.0.0.1 (Local/Remote)";

            
            dt.Columns.Add("Established", typeof(System.Double));
            dt.Columns.Add("SyncSent", typeof(System.Double));
            dt.Columns.Add("Listen", typeof(System.Double));

            barChart.Description.Text = "TCP States chart";

            dr = dt.NewRow();

            dr[0] = 0;
            dr[1] = 0;
            dr[2] = 0;


            dt.Rows.Add(dr);


            this.barChart.DataSource = dt;
            barChart.BarsGap = trackBarGap.Value;
            barChart.RedrawChart();
            barChart.BarWidth = trackBarWidthBar.Value;
            barChart.RedrawChart();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                PublicClass.StopThread = false;                
                PublicClass.Settable();
                listView1.Items.Clear();
                Start_Time_Label.Text = DateTime.Now.ToString();
                Core = new Thread(Core_Method);
                Core.Start();
                MonitorStatus_TXT.Text = "Monitoring";
                button2.Enabled = true;
                button1.Enabled = false;
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            catch (Exception err)
            {


            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Core != null)
            {
                if (Core.IsAlive)
                {
                    PublicClass.StopThread = true;
                    Core.Abort();
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            tabControl2.Refresh();
            tabControl1.Refresh();

        }

        private void listenSyncSentEstablishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               // find = false;
                if (listenSyncSentEstablishedToolStripMenuItem.Checked)
                {
                    listenSyncSentEstablishedToolStripMenuItem.Checked = false;
                    if (establishedToolStripMenuItem.Checked == false)
                    {
                        establishedToolStripMenuItem.Checked = true;
                        FL1.Text = "Established Connections";
                        FL1.ToolTipText = "View Only Established Connections";
                      //MyExternalClass.BoolStates = false;
                        PublicClass.Filtering_ISEstablished = true;
                    }
                }
                else
                {
                    //MyExternalClass.BoolStates = true;
                    PublicClass.Filtering_ISEstablished = false;
                    listenSyncSentEstablishedToolStripMenuItem.Checked = true;
                    establishedToolStripMenuItem.Checked = false;
                    FL1.Text = "";
                    FL1.ToolTipText = "";
                }

            }
            catch (Exception err)
            {


            }
            
        }

        private void establishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            PublicClass.Filtering_ISEstablished = true;
            try
            {
              //  find = false;
                if (establishedToolStripMenuItem.Checked)
                {
                    establishedToolStripMenuItem.Checked = false;
                    if (listenSyncSentEstablishedToolStripMenuItem.Checked == false)
                    {
                        listenSyncSentEstablishedToolStripMenuItem.Checked = true;
                        //toolStripLabel1.Text = "View All Connections";
                        FL1.Text = "";
                        FL1.ToolTipText = "";
                        //MyExternalClass.BoolStates = true;
                        PublicClass.Filtering_ISEstablished = false;
                    }
                }
                else
                {
                    //MyExternalClass.BoolStates = false;
                    PublicClass.Filtering_ISEstablished = true;
                    establishedToolStripMenuItem.Checked = true;
                    listenSyncSentEstablishedToolStripMenuItem.Checked = false;
                    //toolStripLabel1.Text = "View Established Connections";
                    FL1.Text = "Established Connections";
                    FL1.ToolTipText = "View Only Established Connections";
                }


            }
            catch (Exception err)
            {


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PublicClass.StopThread = true;

                if (Core != null)
                {
                    if (Core.IsAlive)
                    {
                        Core.Abort();
                    }
                }
                MonitorStatus_TXT.Text = "";
                button1.Enabled = true;
                startToolStripMenuItem.Enabled = true;
                Estab_label.Text = "0";
                Sync_label.Text = "0";
                Total_label.Text = "0";
            }
            catch (Exception err)
            {


            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            tabControl2.Refresh();
            tabControl1.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabControl2.Refresh();
            tabControl1.Refresh();
        }

        private void dontViewConnectionFor127001ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (!dontViewConnectionFor127001ToolStripMenuItem.Checked)
                {
                    dontViewConnectionFor127001ToolStripMenuItem.Checked = true;
                    viewConnectionFor127ToolStripMenuItem.Checked = false;
                    FL2.Text = "Don't View 127.0.0.1 (Local/Remote)";
                    FL2.ToolTipText = "Do Not View Connection for 127.0.0.1 (LocalIP & RemoteIP) Best Performance";
                    //MyExternalClass.is_Filtering_127001_Active = true;
                    PublicClass.Filtering_IS_127001 = true;
                }
                else
                {
                    dontViewConnectionFor127001ToolStripMenuItem.Checked = false;
                    viewConnectionFor127ToolStripMenuItem.Checked = true;
                    FL2.Text = "";
                    FL2.ToolTipText = "";
                    //MyExternalClass.is_Filtering_127001_Active = false;
                    PublicClass.Filtering_IS_127001 = false;

                }

            }
            catch (Exception err)
            {


            }
        }

        private void viewConnectionFor127ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (!viewConnectionFor127ToolStripMenuItem.Checked)
                {
                    viewConnectionFor127ToolStripMenuItem.Checked = true;
                    dontViewConnectionFor127001ToolStripMenuItem.Checked = false;
                    FL2.Text = "";
                    FL2.ToolTipText = "";
                    //MyExternalClass.is_Filtering_127001_Active = false;
                    PublicClass.Filtering_IS_127001 = false;

                }
                else
                {
                    viewConnectionFor127ToolStripMenuItem.Checked = false;
                    dontViewConnectionFor127001ToolStripMenuItem.Checked = true;
                    FL2.Text = "Don't View 127.0.0.1 (Local/Remote)";
                    FL2.ToolTipText = "Do Not View Connection for 127.0.0.1 (LocalIP & RemoteIP) Best Performance";
                    //MyExternalClass.is_Filtering_127001_Active = true;
                    PublicClass.Filtering_IS_127001 = true;

                }


            }
            catch (Exception err)
            {


            }
        }

        private void aboutTCPMON30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                AboutBox1 MyAboutInfo = new AboutBox1();
                MyAboutInfo.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                PublicClass.StopThread = false;
                PublicClass.Settable();
                listView1.Items.Clear();
                Start_Time_Label.Text = DateTime.Now.ToString();
                Core = new Thread(Core_Method);
                Core.Start();
                MonitorStatus_TXT.Text = "Monitoring";
                button2.Enabled = true;
                button1.Enabled = false;
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            catch (Exception err)
            {


            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (Core != null)
                {
                    if (Core.IsAlive)
                    {
                        PublicClass.StopThread = true;
                        Core.Abort();
                    }
                }
                MonitorStatus_TXT.Text = "";
                button1.Enabled = true;
                startToolStripMenuItem.Enabled = true;
                Estab_label.Text = "0";
                Sync_label.Text = "0";
                Total_label.Text = "0";
            }
            catch (Exception err)
            {


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            //dataGridView1.DataSource = PublicClass.table;            
            //dataGridView1.Refresh();
            //PublicClass.test();
            //PublicClass.TCPIP_settable();
            PublicClass.saveXml_Logs();
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _LogState.Text = "";
                dataGridView2.DataSource = null;
                offToolStripMenuItem.Checked = true;
                onToolStripMenuItem.Checked = false;
            }
            catch (Exception err)
            {
                
                
            }
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _LogState.Text = "On";
                dataGridView2.DataSource = PublicClass.TCPIPTable;
                onToolStripMenuItem.Checked = true;
                offToolStripMenuItem.Checked = false;

            }
            catch (Exception err)
            {
                
                
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PublicClass.ProcessPID_TO_Properties = 0;
                if (dataGridView2.CurrentRow != null)
                {
                    PublicClass.TCPIPTable.Rows[dataGridView2.CurrentRow.Index].ItemArray[7].ToString();

                    PublicClass.ProcessPID_TO_Properties = (int)PublicClass.TCPIPTable.Rows[dataGridView2.CurrentRow.Index].ItemArray[7];

                    LogPropertyForm MLP = new LogPropertyForm();
                    MLP.ShowDialog();
                    MLP.Close();
                }
            }
            catch (Exception err)
            {
                
                
            }

           
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PublicClass.ProcessPID_TO_Properties = 0;
                if (dataGridView2.CurrentRow != null)
                {
                   // PublicClass.TCPIPTable.Rows[dataGridView2.CurrentRow.Index].ItemArray[7].ToString();

                    PublicClass.ProcessPID_TO_Properties = (int)PublicClass.TCPIPTable.Rows[dataGridView2.CurrentRow.Index].ItemArray[7];

                    LogPropertyForm MLP = new LogPropertyForm();
                    MLP.ShowDialog();
                    MLP.Close();
                }
            }
            catch (Exception err)
            {


            }
        }

        private void createLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PublicClass.saveXml_Logs();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                PublicClass.StopThread = true;

                if (Core != null)
                {
                    if (Core.IsAlive)
                    {
                        Core.Abort();
                    }
                }
                MonitorStatus_TXT.Text = "";
                button1.Enabled = true;
                startToolStripMenuItem.Enabled = true;
                Estab_label.Text = "0";
                Sync_label.Text = "0";
                Total_label.Text = "0";
                Close();
            }
            catch (Exception err)
            {


            }
        }
        //0000000000000000000 CHART TOOLS 000000000000000000000

        private void trackBarBorder_Scroll(object sender, EventArgs e)
        {
            try
            {
                barChart.Border.Width = trackBarBorder.Value;
                barChart.RedrawChart();
                labelBorder.Text = String.Format("Size({0})", trackBarBorder.Value);
            }
            catch (Exception err)
            {


            }
        }

        private void buttonColorBorder_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = this.colorDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    barChart.Border.Color = colorDialog.Color;
                    barChart.RedrawChart();
                }
            }
            catch (Exception err)
            {


            }
        }

        private void trackBarGap_Scroll(object sender, EventArgs e)
        {
            try
            {
                barChart.BarsGap = trackBarGap.Value;
                barChart.RedrawChart();

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Gap siz({0})", trackBarGap.Value.ToString());
                labelGapValue.Text = sb.ToString();
            }
            catch (Exception err)
            {


            }
        }

        private void trackBarWidthBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                barChart.BarWidth = trackBarWidthBar.Value;
                barChart.RedrawChart();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Bar Width({0})", trackBarWidthBar.Value.ToString());

                labelBarWidthValue.Text = sb.ToString();
            }
            catch (Exception err)
            {


            }
        }

        private void radioButtonBKGradient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                barChart.Background.PaintingMode = CBackgroundProperty.PaintingModes.LinearGradient;
                barChart.RedrawChart();
            }
            catch (Exception err)
            {


            }
        }

        private void radioButtonBKRadial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                barChart.Background.PaintingMode = CBackgroundProperty.PaintingModes.RadialGradient;
                barChart.RedrawChart();

            }
            catch (Exception err)
            {


            }
        }

        private void radioButtonBKSolid_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                barChart.Background.PaintingMode = CBackgroundProperty.PaintingModes.SolidColor;
                barChart.RedrawChart();

            }
            catch (Exception err)
            {


            }
        }

        private void buttonBKColor1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = colorDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    barChart.Background.GradientColor1 = colorDialog.Color;
                    barChart.RedrawChart();
                }
            }
            catch (Exception err)
            {


            }
        }

        private void buttonBKColor2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = colorDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    barChart.Background.GradientColor2 = colorDialog.Color;
                    barChart.RedrawChart();
                }
            }
            catch (Exception err)
            {


            }
        }

        private void buttonBKColorSolid_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = colorDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    barChart.Background.SolidColor = colorDialog.Color;
                    barChart.RedrawChart();
                }
            }
            catch (Exception err)
            {


            }
        }

        private void defaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                trackBarBorder.Value = 0;
                trackBarGap.Value = 8;
                trackBarWidthBar.Value = 59;
                barChart.BarWidth = trackBarWidthBar.Value;
                barChart.RedrawChart();
                barChart.BarsGap = trackBarGap.Value;
                barChart.RedrawChart();
                barChart.Border.Width = trackBarBorder.Value;
                barChart.RedrawChart();
                StringBuilder sb = new StringBuilder();
                labelBorder.Text = String.Format("Size({0})", trackBarBorder.Value);
                sb = new StringBuilder();
                sb.AppendFormat("Gap siz({0})", trackBarGap.Value.ToString());
                labelGapValue.Text = sb.ToString();
                sb = new StringBuilder();
                sb.AppendFormat("Bar Width({0})", trackBarWidthBar.Value.ToString());
                labelBarWidthValue.Text = sb.ToString();
            }
            catch (Exception err)
            {


            }
        }
        
        //0000000000000000000 CHART TOOLS 000000000000000000000


        private void onToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                timer1.Start();
                _ChartState.Text = "On";
                onToolStripMenuItem2.Checked = true;
                offToolStripMenuItem2.Checked = false;
            }
            catch (Exception err)
            {
                
                
            }
            
        }

        private void offToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                timer1.Stop();
                onToolStripMenuItem2.Checked = false;
                offToolStripMenuItem2.Checked = true;
                _ChartState.Text = " ";
                dr[0] = 0;
                dr[1] = 0;
                dr[2] = 0;
            }
            catch (Exception err)
            {
                
                
            }
           

        }

        private void viewProcessNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!viewProcessNameToolStripMenuItem.Checked)
            {
                viewProcessNameToolStripMenuItem.Checked = true;
                viewProcessNameWithPathToolStripMenuItem.Checked = false;
                PublicClass.IsProcessNameActive = true;
                PublicClass.table.Clear();
            }
        }

        private void viewProcessNameWithPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!viewProcessNameWithPathToolStripMenuItem.Checked)
            {
                viewProcessNameWithPathToolStripMenuItem.Checked = true;
                viewProcessNameToolStripMenuItem.Checked = false;
                PublicClass.IsProcessNameActive = false;
                PublicClass.table.Clear();

            }
        }
       
        private void loadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                OpenFileDialog OpenXMLFile = new OpenFileDialog();
                OpenXMLFile.FileName = null;
                OpenXMLFile.Filter = "XML files (*.xml)|*.xml";
                OpenXMLFile.FilterIndex = 0;
                OpenXMLFile.RestoreDirectory = true;
                if (OpenXMLFile.ShowDialog() == DialogResult.OK)
                {
                    if (OpenXMLFile.FileName != null)
                    {
                        // Insert code to read the stream here.
                        XMLFileName = OpenXMLFile.FileName;
                        SafeXMLFileName = OpenXMLFile.SafeFileName;

                    }
                }

                if (OpenXMLFile.FileName != null & OpenXMLFile.FileName !="" )
                {
                    TabPage LoadXMLPage = new TabPage("Loaded Xml File " + "(" + SafeXMLFileName + ")");
                    LoadXMLPage.Name = "XMLPage";
                    if (tabControl1.TabPages.Count == 2)
                    {
                        tabControl1.TabPages.Add(LoadXMLPage);
                    }
                    else
                    {
                          tabControl1.TabPages.RemoveAt(2);
                          tabControl1.TabPages.Add(LoadXMLPage);                                    
                    }
                    
                    LoadXML_DataGridView3.RowHeadersVisible = false;
                    LoadXML_DataGridView3.AllowUserToAddRows = false;
                    LoadXML_DataGridView3.AllowUserToDeleteRows = false;
                    LoadXML_DataGridView3.AllowUserToResizeColumns = false;
                    LoadXML_DataGridView3.AllowUserToResizeRows = false;
                    LoadXML_DataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    LoadXML_DataGridView3.MultiSelect = false;
                    LoadXML_DataGridView3.ReadOnly = true;
                    LoadXML_DataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    LoadXML_DataGridView3.Dock = DockStyle.Fill;
                    LoadXML_DataGridView3.BackgroundColor = Color.White;                    
                    LoadXMLPage.Controls.Add(LoadXML_DataGridView3);
                    tabControl1.SelectedIndex = 2;
                    try
                    {
                      
                        if (!BGW_LoadXMLCore.IsBusy)
                        {
                            BGW_LoadXMLCore.RunWorkerAsync();
                        }
                        //PublicClass.Load_XML_TCPIPTable.Rows.Clear();
                        //PublicClass.saveSchema();
                        //PublicClass.Load_XML_TCPIPTable.ReadXmlSchema("TempSchema.xml");
                        //PublicClass.Load_XML_TCPIPTable.ReadXml(XMLFileName);
                        //LoadXML_DataGridView3.DataSource = null;
                        //LoadXML_DataGridView3.DataSource = PublicClass.Load_XML_TCPIPTable;
                        //LoadXML_DataGridView3.Refresh();
                        //tabControl1.SelectedIndex = 2;
                       

                    }
                    catch (Exception err1)
                    {
                        MessageBox.Show(null,err1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }
            }
            catch (Exception err)
            {
               

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //Thread.Sleep(200);
                if (dr[1].ToString() != Sync.ToString())
                {
                    dr[1] = Sync;
                }
                if (dr[1].ToString() != Estab.ToString())
                {
                    dr[0] = Estab;
                }
                if (dr[2].ToString() != Listen.ToString())
                {
                    dr[2] = Listen;
                }
            }
            catch (Exception err)
            {


            }
        }

        void BGW_LoadXMLCore_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadXML_DataGridView3.DataSource = PublicClass.Load_XML_TCPIPTable;
            LoadXML_DataGridView3.Refresh();
            tabControl1.SelectedIndex = 2;
            MessageBox.Show(null, "Loading Complete...", "File: " + SafeXMLFileName , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void BGW_LoadXMLCore_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            LoadXML_DataGridView3.DataSource = PublicClass.Load_XML_TCPIPTable;
           
            LoadXML_DataGridView3.Refresh();
            tabControl1.SelectedIndex = 2;
        }
      
        void BGW_LoadXMLCore_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = new object[2];
            obj[0] = BGW_LoadXMLCore;
            obj[1] = e;
            BeginInvoke(new _BGWMethod(BGW_LoadXMLCore_Method),(obj));
            
            
        }




        #region Managed IP Helper API

        public class TcpTable : IEnumerable<TcpRow>
        {
            #region Private Fields

            private IEnumerable<TcpRow> tcpRows;

            #endregion

            #region Constructors

            public TcpTable(IEnumerable<TcpRow> tcpRows)
            {
                this.tcpRows = tcpRows;
            }

            #endregion

            #region Public Properties

            public IEnumerable<TcpRow> Rows
            {
                get { return this.tcpRows; }
            }

            #endregion

            #region IEnumerable<TcpRow> Members

            public IEnumerator<TcpRow> GetEnumerator()
            {
                return this.tcpRows.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.tcpRows.GetEnumerator();
            }

            #endregion
        }

        public class TcpRow
        {
            #region Private Fields

            private IPEndPoint localEndPoint;
            private IPEndPoint remoteEndPoint;
            private TcpState state;
            private int processId;

            #endregion

            #region Constructors

            public TcpRow(IpHelper.TcpRow tcpRow)
            {
                this.state = tcpRow.state;
                this.processId = tcpRow.owningPid;

                int localPort = (tcpRow.localPort1 << 8) + (tcpRow.localPort2) + (tcpRow.localPort3 << 24) + (tcpRow.localPort4 << 16);
                long localAddress = tcpRow.localAddr;
                this.localEndPoint = new IPEndPoint(localAddress, localPort);

                int remotePort = (tcpRow.remotePort1 << 8) + (tcpRow.remotePort2) + (tcpRow.remotePort3 << 24) + (tcpRow.remotePort4 << 16);
                long remoteAddress = tcpRow.remoteAddr;
                this.remoteEndPoint = new IPEndPoint(remoteAddress, remotePort);
            }

            #endregion

            #region Public Properties

            public IPEndPoint LocalEndPoint
            {
                get { return this.localEndPoint; }
            }

            public IPEndPoint RemoteEndPoint
            {
                get { return this.remoteEndPoint; }
            }

            public TcpState State
            {
                get { return this.state; }
            }

            public int ProcessId
            {
                get { return this.processId; }
            }

            #endregion
        }

        public static class ManagedIpHelper
        {
            #region Public Methods

            public static TcpTable GetExtendedTcpTable(bool sorted)
            {
                List<TcpRow> tcpRows = new List<TcpRow>();

                IntPtr tcpTable = IntPtr.Zero;
                int tcpTableLength = 0;

                if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, sorted, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) != 0)
                {
                    try
                    {
                        tcpTable = Marshal.AllocHGlobal(tcpTableLength);
                        if (IpHelper.GetExtendedTcpTable(tcpTable, ref tcpTableLength, true, IpHelper.AfInet, IpHelper.TcpTableType.OwnerPidAll, 0) == 0)
                        {
                            IpHelper.TcpTable table = (IpHelper.TcpTable)Marshal.PtrToStructure(tcpTable, typeof(IpHelper.TcpTable));

                            IntPtr rowPtr = (IntPtr)((long)tcpTable + Marshal.SizeOf(table.length));
                            for (int i = 0; i < table.length; ++i)
                            {
                                tcpRows.Add(new TcpRow((IpHelper.TcpRow)Marshal.PtrToStructure(rowPtr, typeof(IpHelper.TcpRow))));
                                rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(typeof(IpHelper.TcpRow)));
                            }
                        }
                    }
                    finally
                    {
                        if (tcpTable != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(tcpTable);
                        }
                    }
                }

                return new TcpTable(tcpRows);
            }

            #endregion
        }

        #endregion

        #region P/Invoke IP Helper API

        /// <summary>
        /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366073.aspx"/>
        /// </summary>
        public static class IpHelper
        {
            #region Public Fields

            public const string DllName = "iphlpapi.dll";
            public const int AfInet = 2;

            #endregion

            #region Public Methods

            /// <summary>
            /// <see cref="http://msdn2.microsoft.com/en-us/library/aa365928.aspx"/>
            /// </summary>
            [DllImport(IpHelper.DllName, SetLastError = true)]
            public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, int ipVersion, TcpTableType tcpTableType, int reserved);
          

            #endregion

            #region Public Enums

            /// <summary>
            /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366386.aspx"/>
            /// </summary>
            public enum TcpTableType
            {
                BasicListener,
                BasicConnections,
                BasicAll,
                OwnerPidListener,
                OwnerPidConnections,
                OwnerPidAll,
                OwnerModuleListener,
                OwnerModuleConnections,
                OwnerModuleAll,
            }

            #endregion

            #region Public Structs

            /// <summary>
            /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366921.aspx"/>
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct TcpTable
            {
                public uint length;
                public TcpRow row;
            }

            /// <summary>
            /// <see cref="http://msdn2.microsoft.com/en-us/library/aa366913.aspx"/>
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct TcpRow
            {
                public TcpState state;
                public uint localAddr;
                public byte localPort1;
                public byte localPort2;
                public byte localPort3;
                public byte localPort4;
                public uint remoteAddr;
                public byte remotePort1;
                public byte remotePort2;
                public byte remotePort3;
                public byte remotePort4;
                public int owningPid;
            }

            #endregion
        }

        #endregion

       

       
  
    }
}
