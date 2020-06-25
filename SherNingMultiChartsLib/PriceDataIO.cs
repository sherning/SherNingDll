//==============================================================================
// Name           : Price Data IO (Reader and Writer class)
// Description    : Import and export data as a Txt
// Version        : v1.0
// Date Created   : 03 - June - 2020
// Time Taken     : 
// Remarks        :
//==============================================================================
// Copyright      : 2020, Sher Ning Technologies           
// License        :      
//==============================================================================

/* ------------------------------- Version History -------------------------------
 * Reader and Writer class for reading and writing Price Time, High, Low, Open, Close
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace SherNingMultiChartsLib
{
    #region Price Data Writer
    class PriceDataWriter
    {
        // class properties
        public string SymbolName { get; set; }
        public int ResolutionSize { get; set; }
        public string ResolutionType { get; set; }
        public int MaxBars { get; set; }
        public int NumOfBars { get; private set; }

        // class fields
        private DataTable DataTable;
        private string[] BarInformationNames;
        private readonly string DirectoryPath;
        private string FilePath;
        private bool HasWrittenToText;

        // class constructor
        public PriceDataWriter()
        {
            DirectoryPath = @"C:\MultiChartPriceData";
        }

        #region public class functions
        public void Initialize(string symName, int resoSize, string resoType, int maxBars = 100)
        {
            BarInformationNames = new string[]
            {
                "Time", "High", "Low", "Open", "Close"
            };

            SymbolName = symName;
            ResolutionSize = resoSize;
            ResolutionType = resoType;

            // Negative number to set maximum num of bars.
            if (maxBars < 0)
                MaxBars = int.MaxValue;
            else
                MaxBars = maxBars;

            string dataTableName = string.Format("{0} {1} {2} Price Data",
                SymbolName, ResolutionSize, ResolutionType);

            FilePath = string.Format(@"{0}\{1}.txt", DirectoryPath, dataTableName);

            // Setup Data Table
            DataTable = new DataTable(dataTableName);
            AddColumnHeaders();

            // Reset data
            NumOfBars = 0;
            HasWrittenToText = false;
        }

        public void AddPriceData(DateTime time, double high, double low, double open, double close)
        {
            if (NumOfBars == MaxBars) return;

            DataTable.Rows.Add(time, high, low, open, close);
            NumOfBars++;
        }

        #endregion

        private void AddColumnHeaders()
        {
            for (int i = 0; i < BarInformationNames.Length; i++)
            {
                if (i == 0)
                    DataTable.Columns.Add(BarInformationNames[i], typeof(DateTime));
                else
                    DataTable.Columns.Add(BarInformationNames[i], typeof(double));
            }
        }

        public void WriteToText()
        {
            // Write only once.
            if (HasWrittenToText == true) return;

            if (Directory.Exists(FilePath) == false)
            {
                DirectoryInfo newDirectory = new DirectoryInfo(DirectoryPath);
                newDirectory.Create();
            }

            // There are no data to write
            if (NumOfBars == 0) return;

            using (StreamWriter sw = new StreamWriter(FilePath, append: false))
            {
                sw.WriteLine("\t\t\t\t\t" + DataTable.TableName);
                sw.WriteLine("\t\t\t\t\t\t" + NumOfBars + " Price Bars");

                // blank line
                sw.WriteLine();

                // write column headers
                foreach (DataColumn item in DataTable.Columns) sw.Write("\t\t " + item);
                sw.WriteLine();

                // write information
                for (int j = 0; j < NumOfBars; j++)
                {
                    sw.Write(DataTable.Rows[j][BarInformationNames[0]] + "\t\t");
                    sw.Write(DataTable.Rows[j][BarInformationNames[1]] + "\t\t");
                    sw.Write(DataTable.Rows[j][BarInformationNames[2]] + "\t\t");
                    sw.Write(DataTable.Rows[j][BarInformationNames[3]] + "\t\t");
                    sw.Write(DataTable.Rows[j][BarInformationNames[4]] + "\t\t");
                    sw.WriteLine();
                }

                HasWrittenToText = true;
                // MC does not contain Field()
                //foreach (DataRow row in DataTable.Rows)
                //{
                //    sw.Write(row.Field<DateTime>(0) + "\t\t");
                //    sw.Write(row.Field<double>(1) + "\t\t");
                //    sw.Write(row.Field<double>(2) + "\t\t");
                //    sw.Write(row.Field<double>(3) + "\t\t");
                //    sw.Write(row.Field<double>(4) + "\t\t");
                //    sw.WriteLine();
                //}
            }
        }
    }

    #endregion

    #region Price Data Reader
    class PriceDataReader
    {
        // class properties
        public Bars GetBars { get { return Bars; } }

        // class fields
        private int NumOfBars;
        private string FileName;
        private string ColumnHeaders;
        private Bars Bars;

        // class constructor
        public PriceDataReader(string filepath)
        {
            ReadTxtFile(filepath);
        }

        // class methods
        public void ReadTxtFile(string filePath)
        {
            NumOfBars = 0;
            Bars = new Bars();

            // Check if file is valid
            if (File.Exists(filePath) == false) return;

            using (StreamReader sr = new StreamReader(filePath))
            {
                // File Name - Do nothing
                FileName = sr.ReadLine();

                // Number of bars in file
                string[] str1 = sr.ReadLine().Split(' ');
                NumOfBars = int.Parse(str1[0]);
                Bars.SetMaxBars(NumOfBars);

                // Blank Line - Do nothing
                sr.ReadLine();

                // Column Header - Do nothing
                ColumnHeaders = sr.ReadLine();

                // Read data information
                string[] delimiters = new string[]
                {
                    "\t", "\t\t"
                };

                while (sr.EndOfStream == false)
                {
                    // split line
                    string[] str2 = sr.ReadLine().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    // add data to bars
                    Bars.AddTime(DateTime.Parse(str2[0]));
                    Bars.AddHigh  (double.Parse(str2[1]));
                    Bars.AddLow   (double.Parse(str2[2]));
                    Bars.AddOpen  (double.Parse(str2[3]));
                    Bars.AddClose (double.Parse(str2[4]));
                }
            }
        }

        // Print does not work for MC. Remember to comment out.
        public void Print()
        {
            // Write file name and num of price bars
            Console.WriteLine(FileName);
            Console.WriteLine("\t\t\t\t\t\t" + NumOfBars + " Price Bars\n");

            // write column headers
            Console.WriteLine(ColumnHeaders);

            // Print data, newest bar in the first
            for (int i = 0; i < NumOfBars; i++)
            {
                Console.WriteLine($"{Bars.Time[i]}\t\t{Bars.High[i]}" +
                    $"\t\t{Bars.Low[i]}\t\t{Bars.Open[i]}\t\t{Bars.Close[i]}");
            }
        }
    }
    #endregion

}
