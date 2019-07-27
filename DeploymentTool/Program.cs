using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentTool
{
	class Program
	{
		private static readonly string GardenBranch = ConfigurationManager.AppSettings["GARDEN_BRANCH"];
		private static readonly string DeployBranch = ConfigurationManager.AppSettings["DEPLOY_BRANCH"];
		private static readonly string CAdminFolder = ConfigurationManager.AppSettings["CADMIN_PRJ_FOLDER"];
		private static readonly string DeployFolder = ConfigurationManager.AppSettings["CADMIN_DEPLOY_PRJ_FOLDER"];
		private static readonly string SolutionFile = ConfigurationManager.AppSettings["CADMIN_SOLUTION_FILE"];
		private static readonly string VsCommandFolder = ConfigurationManager.AppSettings["VS_COMMAND_FOLDER"];
		private static readonly string PublishProfile = ConfigurationManager.AppSettings["CADMIN_PUBLISH_PROFILE"];
		static void Main(string[] args)
		{
			//Utilities.ExecutePublishCommand("\"C:\\Program Files(x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsDevCmd.bat\"");
			//Utilities.ExecutePublishCommand(Publish());
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			//git config credential.helper store
			Console.WriteLine("#Developed by ThanhLD");
			//Utilities.ExecuteCommand("git log", CAdminFolder);
			PrintMessageBelow("Hế lô bấy bì, muốn deploy branch gì nào?");
			var currentCodingBranch = Console.ReadLine();

			PrintMessageBelow("Trạng thái file vừa code");
			Utilities.ExecuteCommand("git status", CAdminFolder);

			PrintMessage("Hãy chắc chắn code đã chạy ngon dưới local trước khi ấn phím bất kỳ để xác nhận deploy");
			Console.ReadLine();

			PrintMessageBelow("Commit lên branch hiện tại...");
			Utilities.ExecuteCommand("git add .", CAdminFolder);
			Utilities.ExecuteCommand("git commit -m \"Auto commit by mr.Robot\" ", CAdminFolder);
			Utilities.ExecuteCommand(Push(currentCodingBranch), CAdminFolder);

			PrintMessageBelow($"Commit lên branch {GardenBranch}...");
			Utilities.ExecuteCommand(Checkout(GardenBranch), CAdminFolder);
			Utilities.ExecuteCommand(Pull(GardenBranch), CAdminFolder);
			Utilities.ExecuteCommand(Pull(currentCodingBranch), CAdminFolder);
			Utilities.ExecuteCommand(Push(GardenBranch), CAdminFolder);

			PrintMessage("Đã merge vào Garden, hãy giải quyết conflict nếu có, sau đó ấn '1' để tiếp tục");
			var input = Console.ReadLine();
			while (input != "1")
			{
				PrintMessageBelow("Nhập '1' để confirm cơ mà?");
				input = Console.ReadLine();
			}
			Utilities.ExecuteCommand("git add .", CAdminFolder);
			Utilities.ExecuteCommand("git commit -m \"Resolve conflict by mr.Robot\" ", CAdminFolder);
			Utilities.ExecuteCommand(Push(GardenBranch), CAdminFolder);

			Utilities.ExecuteCommand("git add .", DeployFolder);
			Utilities.ExecuteCommand("git reset --h", DeployFolder);
			Utilities.ExecuteCommand(Pull(DeployBranch), DeployFolder);


			PrintMessage("Chức năng publish đang hoàn thiện, nên :'( publish hộ cái nhé, publish xong ấn phím 1 :\"> ");
			input = Console.ReadLine();
			while (input != "1")
			{
				Console.WriteLine("Nhập '1' để confirm cơ mà?");
				input = Console.ReadLine();
			}

			//PrintMessageBelow("Đang tiến hành publish code...");
			//Utilities.ExecutePublishCommand(Publish());

			PrintMessageBelow("Đang deploy...");
			Utilities.ExecuteCommand("git add .", DeployFolder);
			Utilities.ExecuteCommand("git reset Web.config", DeployFolder);
			Utilities.ExecuteCommand("git commit -m \"Deployed by mr.Robot\" ", DeployFolder);
			Utilities.ExecuteCommand(Push(DeployBranch), DeployFolder);
			PrintMessage("Đã deploy xong, ấn phím bất kỳ để thoát!");
			Console.ReadKey();
		}

		private static String SwitchFolder(String desPath)
		{
			return $"cd {desPath}";
		}

		private static String Pull(String desBranch)
		{
			return $"git pull origin {desBranch}";
		}

		private static String Push(String desBranch)
		{
			return $"git push origin {desBranch}";
		}

		private static String Checkout(String desBranch)
		{
			return $"git checkout {desBranch}";
		}

		private static void PrintMessage(String msg)
		{
			Console.WriteLine("=====================================================");
			Console.WriteLine(msg);
			Console.WriteLine("=====================================================");
		}

		private static void PrintMessageBelow(String msg)
		{
			Console.WriteLine("=====================================================");
			Console.WriteLine(msg);
			Console.WriteLine("-----------");
		}

		private static string Publish()
		{
			return $"msbuild {SolutionFile} /p:DeployOnBuild=true /p:PublishProfile={PublishProfile}";
		}

		private static string Publish1()
		{
			return "\"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\VC\\Auxiliary\\Build\\vcvarsall.bat\" && mstest /help";
		}

	}
}
