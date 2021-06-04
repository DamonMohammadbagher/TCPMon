using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using TCPMon_3._;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace TCPMon_3._1
{
   public class PublicClass
    {
      
       public static string SCHEMA = "<?xml version=\"1.0\" standalone=\"yes\"?><xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\"> <xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:MainDataTable=\"TCPIP\" msdata:UseCurrentLocale=\"true\"><xs:complexType><xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\"><xs:element name=\"TCPIP\">" +
          "<xs:complexType><xs:sequence><xs:element name=\"TIME\" type=\"xs:dateTime\" minOccurs=\"0\" />" +
              "<xs:element name=\"Local_IP\" type=\"xs:string\" minOccurs=\"0\" />" +
              "<xs:element name=\"Local_Port\" type=\"xs:int\" minOccurs=\"0\" />" +
              "<xs:element name=\"Remote_IP\" type=\"xs:string\" minOccurs=\"0\" />" +
              "<xs:element name=\"Remote_Port\" type=\"xs:int\" minOccurs=\"0\" />" +
              "<xs:element name=\"State\" type=\"xs:string\" minOccurs=\"0\" />" +
              "<xs:element name=\"State_Code\" type=\"xs:int\" minOccurs=\"0\" />" +
              "<xs:element name=\"Pid\" type=\"xs:int\" minOccurs=\"0\" />" +
              "<xs:element name=\"ProcessName\" type=\"xs:string\" minOccurs=\"0\" />" +
            "</xs:sequence>" +
          "</xs:complexType>" +
        "</xs:element>" +
      "</xs:choice>" +
    "</xs:complexType>" +
  "</xs:element>" +
"</xs:schema>";

       private static bool _IsProcessNameActive;
       public static bool IsProcessNameActive
       {
           get { return _IsProcessNameActive; }
           set { _IsProcessNameActive = value; }
       }


       private static int _ProcessPID_TO_Properties;
       public static int ProcessPID_TO_Properties
        {
            get { return _ProcessPID_TO_Properties; }
            set { _ProcessPID_TO_Properties = value; }
        }


       private static bool _IsLogActive;
       public static bool IsLogActive
        {
            get { return _IsLogActive; }
            set { _IsLogActive = value; }
        }
       
       private static bool _Filtering_IS_Established;
       public static bool Filtering_ISEstablished
        {
            get { return _Filtering_IS_Established; }
            set { _Filtering_IS_Established = value; }
        }

       private static bool _Filtering_IS_127001;
       public static bool Filtering_IS_127001
        {
            get { return _Filtering_IS_127001; }
            set { _Filtering_IS_127001 = value; }
        }

       private static bool _StopThread;
       public static bool StopThread
        {
            get { return _StopThread; }
            set { _StopThread = value; }
        }
      
       public static DataTable table = new DataTable("MasterTable");              
       public static DataColumn column;
       public static DataRow row;

       public static void Settable() 
       {

           try
           {


               table.Columns.Clear();
               table.Rows.Clear();
               column = new DataColumn();
               column.DataType = System.Type.GetType("System.Int32");
               column.ColumnName = "Pid";
               table.Columns.Add(column);

               // Create second column.
               column = new DataColumn();
               column.DataType = Type.GetType("System.String");
               column.ColumnName = "ProcessName";
               table.Columns.Add(column);
           }
           catch (Exception err)
           {
               System.Diagnostics.Debug.WriteLine(err.Message);
           }
       }
       public static string SetRow(int Pid)
       {
           string Process_Name = " ";
           try
           {
               Process_Name = GetProcessName(Pid);

           }
           catch (Exception err)
           {
              // System.Diagnostics.Debug.WriteLine(err.Message);
               row = table.NewRow();
               row["Pid"] = Pid;
               try
               {

                   if (_IsProcessNameActive)
                   {
                       
                       Process_Name = Process.GetProcessById(Pid).MainModule.ModuleName;
                   }
                   else if (!_IsProcessNameActive)
                   {
                       Process_Name = Process.GetProcessById(Pid).MainModule.FileName;                      
                   }
               }
               catch (Exception err1)
               {
                   try
                   {
                       Process_Name = Process.GetProcessById(Pid).ProcessName;
                       
                   }
                   catch (Exception err2)
                   {

                       Process_Name = " ";
                   }
                  
               }
               
               row["ProcessName"] = Process_Name;
               table.Rows.Add(row);
           }
           
           return Process_Name;
          
               
       }      
       public static string GetProcessName(int Pid) 
       {
           string expression;
           string result = "";
          
              
               DataRow[] foundRows;
               expression = "Pid = '" + Pid.ToString() + "'";
               // Use the Select method to find all rows matching the filter.
               if (table.Rows != null)
               {
                   foundRows = table.Select(expression);
                   result = foundRows[0][1].ToString();
               }
              
               //// Print column 0 of each returned row.

        
           return result;
       }      
       public static DataTable TCPIPTable = new DataTable("TCPIP");
       public static DataColumn TCPIPcolumn;
       public static DataRow TCPIProw;       
       public static void TCPIP_settable() 
       {
           try
           {

               TCPIPTable.Columns.Clear();
               TCPIPTable.Rows.Clear();
              


                   
                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = System.Type.GetType("System.DateTime");
                   TCPIPcolumn.ColumnName = "TIME";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   // Create second column.
                   TCPIPcolumn = new DataColumn();
                   //TCPIPcolumn.DataType = Type.GetType("System.Net.IPAddress");
                   TCPIPcolumn.DataType = Type.GetType("System.String");
                   TCPIPcolumn.ColumnName = "Local_IP";
                   TCPIPTable.Columns.Add(TCPIPcolumn);


                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
                   TCPIPcolumn.ColumnName = "Local_Port";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = Type.GetType("System.String");
                   TCPIPcolumn.ColumnName = "Remote_IP";
                   TCPIPTable.Columns.Add(TCPIPcolumn);


                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
                   TCPIPcolumn.ColumnName = "Remote_Port";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = System.Type.GetType("System.String");
                   TCPIPcolumn.ColumnName = "State";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   // Create second column.
                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = Type.GetType("System.Int32");
                   TCPIPcolumn.ColumnName = "State_Code";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
                   TCPIPcolumn.ColumnName = "Pid";
                   TCPIPTable.Columns.Add(TCPIPcolumn);

                   // Create second column.
                   TCPIPcolumn = new DataColumn();
                   TCPIPcolumn.DataType = Type.GetType("System.String");
                   TCPIPcolumn.ColumnName = "ProcessName";
                   TCPIPTable.Columns.Add(TCPIPcolumn);
                  
            
           }
           catch (Exception err)
           {
               System.Diagnostics.Debug.WriteLine(err.Message);
           }
       }       
       public static void GetRowsTODataTable(DateTime Time, string LIP, string LPORT, string RIP, string RPORT, string States, int State_Code, int pid, string Processname) 
       {
           try
           {
               TCPIProw = TCPIPTable.NewRow();
               TCPIProw["TIME"] = Time;
               TCPIProw["Local_IP"] = LIP;
               TCPIProw["Local_Port"] = LPORT;
               TCPIProw["Remote_IP"] = RIP;
               TCPIProw["Remote_Port"] = RPORT;
               TCPIProw["State"] = States;
               TCPIProw["State_Code"] = State_Code;
               TCPIProw["pid"] = pid;
               TCPIProw["ProcessName"] = Processname;
               TCPIPTable.Rows.Add(TCPIProw);
           }
           catch (Exception err)
           {


           }
          
       }

       public static void EventRise_NewRows()
       {
           if (IsLogActive)
           {
            //   TCPIPTable.TableNewRow += new DataTableNewRowEventHandler(TCPIPTable_TableNewRow);
           }
       }
       
       public static void saveXml_Logs() 
       {
           SaveFileDialog fs = new SaveFileDialog();          
           fs.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
           fs.FilterIndex = 1;
           fs.RestoreDirectory = true;

           if (fs.ShowDialog() == DialogResult.OK)
           {
               if (fs.FileName != null)
               {
                   // Insert code to read the stream here.
                   TCPIPTable.WriteXml(fs.FileName);
                  // TCPIPTable.WriteXmlSchema("test.xml");
                   
               }
           }

       }

       public static DataTable Load_XML_TCPIPTable = new DataTable("TCPIP");
       public static DataColumn Load_XML_TCPIPcolumn;
       public static DataRow Load_XML_TCPIProw;

       public static void LoadXML_TCPIP_settable()
       {
           try
           {

               Load_XML_TCPIPTable.Columns.Clear();
               Load_XML_TCPIPTable.Rows.Clear();




               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = System.Type.GetType("System.DateTime");
               Load_XML_TCPIPcolumn.ColumnName = "TIME";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               // Create second column.
               Load_XML_TCPIPcolumn = new DataColumn();
               //TCPIPcolumn.DataType = Type.GetType("System.Net.IPAddress");
               Load_XML_TCPIPcolumn.DataType = Type.GetType("System.String");
               Load_XML_TCPIPcolumn.ColumnName = "Local_IP";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);


               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
               Load_XML_TCPIPcolumn.ColumnName = "Local_Port";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = Type.GetType("System.String");
               Load_XML_TCPIPcolumn.ColumnName = "Remote_IP";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);


               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
               Load_XML_TCPIPcolumn.ColumnName = "Remote_Port";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = System.Type.GetType("System.String");
               Load_XML_TCPIPcolumn.ColumnName = "State";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               // Create second column.
               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = Type.GetType("System.Int32");
               Load_XML_TCPIPcolumn.ColumnName = "State_Code";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = System.Type.GetType("System.Int32");
               Load_XML_TCPIPcolumn.ColumnName = "Pid";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);

               // Create second column.
               Load_XML_TCPIPcolumn = new DataColumn();
               Load_XML_TCPIPcolumn.DataType = Type.GetType("System.String");
               Load_XML_TCPIPcolumn.ColumnName = "ProcessName";
               Load_XML_TCPIPTable.Columns.Add(Load_XML_TCPIPcolumn);


           }
           catch (Exception err)
           {
               System.Diagnostics.Debug.WriteLine(err.Message);
           }
       }

       public static void saveSchema() 
       {
           try
           {
               if (File.Exists("TempSchema.xml"))
               {
                   File.Delete("TempSchema.xml");
                   File.AppendAllText("TempSchema.xml", SCHEMA);
               }
               if (!File.Exists("TempSchema.xml"))
               {
                   File.AppendAllText("TempSchema.xml", SCHEMA);
               }           
           }
           catch (Exception err)
           {
               MessageBox.Show(null, err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
           }
           
       }
    }
}
