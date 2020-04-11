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
	public class View : IObserver<Command>
	{
		public WebSocket socket;

		public View()
		{

		}

		public void OnCompleted()
		{
			throw new NotImplementedException();
		}

		public void OnError(Exception error)
		{
			throw new NotImplementedException();
		}

		public virtual void OnNext(Command value)
		{
		}
  }
}
