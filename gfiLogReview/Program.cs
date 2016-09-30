using System;
using System.Collections.Generic;
using System.IO;

namespace gfiLogReview
{
	class Program
    {
        static public void Main(string[] args)
        {
            Setup();

        }



        static public void Setup()
        {

            string filePath = @"\\192.168.1.122\fmserver$\";
            string rvcFile = @"rcvLog.txt";
            string sendFile = @"sendLog.txt";


            string stringToCheck = "SUCCESS";


            Console.WindowWidth = Console.LargestWindowWidth - 20;
            Console.WindowHeight = Console.LargestWindowHeight - 20;

            /// Create temp files for parsing. Doing this to avoid file lock errors.  
            if (File.Exists(filePath + "tempRvcLog.txt"))
            {
                File.Delete(filePath + "tempRvcLog.txt");
                Console.WriteLine("send temp file deleted (Cleanup)");
            }
            else
            {
                File.Copy(filePath + rvcFile, filePath + "tempRvcLog.txt");
                Console.WriteLine("rcv temp file created");
            }
            ///same thing for SEND
            if (File.Exists(filePath + "tempSendLog.txt"))
            {
                File.Delete(filePath + "tempSendLog.txt");
                Console.WriteLine("Send temp file deleted (Cleanup)");
            }
            else
            {
                File.Copy(filePath + sendFile, filePath + "tempSendLog.txt");
                Console.WriteLine("Send temp file created");
            }
            Console.WriteLine(" ");
            Console.WriteLine("This is fancy!");
            Console.WriteLine(" ");

            List<string> rcvLogs = new List<string>(System.IO.File.ReadAllLines(filePath + "tempRvcLog.txt"));
            
            ///*********************************
            ///Begin filter process for RCV Files
            ///*********************************

            //filter array by status -  drop anything with "SUCCESS" status
            for (int i = rcvLogs.Count - 1; i >= 0; i--)
            {
                if (rcvLogs[i].Contains(stringToCheck))
                {
                    rcvLogs.RemoveAt(i);
                }
                
            }
            
            //display remaining array elements
            Console.WriteLine(" ");
            Console.WriteLine("******  Display Failed Received  ******");
            for (int i = rcvLogs.Count - 1; i >= rcvLogs.Count -15; i--)
            {
                    Console.WriteLine(rcvLogs[i]);
            }


            ///*********************************
            ///Begin filter process for Sent Files
            ///*********************************

            List<string> sendLogs = new List<string>(System.IO.File.ReadAllLines(filePath + "tempSendLog.txt"));

            //filter array by status -  drop anything with "SUCCESS" status
            for (int i = sendLogs.Count - 1; i >= 0; i--)
            {
                if (sendLogs[i].Contains(stringToCheck))
                {
                    sendLogs.RemoveAt(i);
                }

            }

            //display remaining array elements
            Console.WriteLine(" ");
            Console.WriteLine("******  Display Failed SENT  ******");
            for (int i = sendLogs.Count - 1; i >= sendLogs.Count - 15; i--)
            {
                Console.WriteLine(sendLogs[i]);
            }

            ///Cleanup temp files after parsing and output. 
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            File.Delete(filePath + "tempRvcLog.txt");
            Console.WriteLine("Rvc temp file deleted");
            File.Delete(filePath + "tempSendLog.txt");
            Console.WriteLine("Send temp file deleted");
			Console.WriteLine("Press enter to close...");
			Console.WriteLine(" ");
			Console.WriteLine(" ");
			Console.ReadLine();
		}
    }
}