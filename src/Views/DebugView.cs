using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;

using TestAPI;
using Controllers;

namespace Views

{
	public class DebugView : View
	{
		public DebugView(WebSocket socket)
		{
			Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
		}
  }
}
