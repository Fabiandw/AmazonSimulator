using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Controllers
{
	struct ObservingClient
	{
		public View currentView;
		public IDisposable unsubscribe;
	}

	public class SimulationController
	{
		private World world;
		private List<ObservingClient> views = new List<ObservingClient>();
		private bool running = false;
		private int tickTime = 25;

		public SimulationController(World world)
		{
			this.world = world;
		}

		public void AddView(View view)
		{
			ObservingClient oc = new ObservingClient();

			oc.unsubscribe = this.world.Subscribe(view);
			oc.currentView = view;

			views.Add(oc);
		}

		public void RemoveView(View view)
		{
			for (int i = 0; i < views.Count; i++)
			{
				ObservingClient currentOC = views[i];

				if (currentOC.currentView == view)
				{
					views.Remove(currentOC);
					currentOC.unsubscribe.Dispose();
				}
			}
		}

		public void Simulate()
		{
			running = true;

			while (running)
			{
				// Tasks(world);
				world.Update(tickTime);
				Thread.Sleep(tickTime);
			}
		}

		public void EndSimulation()
		{
			running = false;
		}
	}
}
