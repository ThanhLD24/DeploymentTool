using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentTool
{
	class Utilities
	{
		public static void ExecuteCommand(string command, string directory)
		{
			try
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.RedirectStandardInput = true;
				startInfo.RedirectStandardOutput = true;
				startInfo.CreateNoWindow = true;
				startInfo.UseShellExecute = false;
				if (!string.IsNullOrEmpty(directory))
					startInfo.WorkingDirectory = directory;
				startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				startInfo.FileName = "cmd.exe";
				startInfo.Arguments = $"/c {command}";
				process.StartInfo = startInfo;
				process.Start();
				process.StandardInput.Flush();
				process.StandardInput.Close();
				process.WaitForExit();
				Console.WriteLine(process.StandardOutput.ReadToEnd());
			}
			catch (Exception e)
			{
				Console.WriteLine("Lỗi cmnr bạn êi, chịu khó deploy bằng tay đi :'( ");
			}
		}

		public static void ExecutePublishCommand(string command)
		{
			try
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.RedirectStandardInput = true;
				startInfo.RedirectStandardOutput = true;
				startInfo.CreateNoWindow = true;
				startInfo.UseShellExecute = false;
				//startInfo.WorkingDirectory = @"C:\Program Files(x86)\Microsoft Visual Studio\2017\Community\Common7\Tools";
				startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				startInfo.FileName = "cmd.exe";
				startInfo.Arguments = $"/k  {command}";
				process.StartInfo = startInfo;
				process.Start();

				//using (StreamWriter sw = process.StandardInput)
				//{
				//	if (sw.BaseStream.CanWrite)
				//	{
				//		sw.WriteLine("/k \"C:\\Program Files(x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsDevCmd.bat\"");
				//		sw.WriteLine(command);
				//	}
				//}
				process.StandardInput.Flush();
				process.StandardInput.Close();
				process.WaitForExit();
				Console.WriteLine(process.StandardOutput.ReadToEnd());
			}
			catch (Exception e)
			{
				Console.WriteLine("Lỗi cmnr bạn êi, chịu khó deploy bằng tay đi :'( ");
			}
		}

		public static void ExecutePublishCommand1(string command) {
			try
			{

				Process cmd = new Process();
				cmd.StartInfo.FileName = "cmd.exe";
				cmd.StartInfo.RedirectStandardInput = true;
				cmd.StartInfo.RedirectStandardOutput = true;
				cmd.StartInfo.CreateNoWindow = true;
				cmd.StartInfo.UseShellExecute = false;
				cmd.Start();
				cmd.StandardInput.WriteLine($"cmd.exe /k \"C:\\Program Files(x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsDevCmd.bat\"");
				cmd.StandardInput.WriteLine(command);
				cmd.StandardInput.Flush();
				cmd.StandardInput.Close();
				cmd.WaitForExit();
				Console.WriteLine(cmd.StandardOutput.ReadToEnd());
			}
			catch(Exception e)
			{
				Console.WriteLine("Lỗi cmnr bạn êi, chịu khó deploy bằng tay đi :'( ");
			}
		}
	}
}
