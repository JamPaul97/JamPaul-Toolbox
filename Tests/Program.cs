using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Logger.Logger.Initialize();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadExit += Application_ThreadExit;
			Application.Run(new Form1());
		}

		private static void Application_ThreadExit(object sender, EventArgs e)
		{
			Logger.Logger.Finalize();
		}
	}
}
