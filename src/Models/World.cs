using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using System.Threading.Tasks;

namespace Models
{
  public class World : IObservable<Command>, IUpdatable
  {
    private List<string> iList = new List<string> { "null3", "null2", "null1", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "E1", "E2", "E3", "E4", "F1", "F2", "F3", "F4", "G1", "G2", "G3", "G4", "H1", "H2", "H3", "H4", "I1", "I2", "I3", "I4", "K1", "K2", "K3", "K4", "L1", "L2", "L3", "L4", "M1", "M2", "M3", "M4", "N1", "N2", "N3", "N4", "O1", "O2", "O3", "O4", "P1", "P2", "P3", "P4", "T1", "T2", "T3", "T4" };
    private List<double> xList = new List<double> { 0, 0, 0, 0, -15, -15, -15, 0, 0, 15, 15, 15, -30, -30, -30, -30, -15, 0, 15, 30, 30, 30, 30, -3, -6, -9, -12, -18, -21, -24, -27, -12, -9, -6, -3, -12, -9, -6, -3, 3, 6, 9, 12, 3, 6, 9, 12, 18, 21, 24, 27, 18, 21, 24, 27, 12, 9, 6, 3, -27, -24, -21, -18, -27, -24, -21, -18, -27, -24, -21, -18, -12, -9, -6, -3, 3, 6, 9, 12, 18, 21, 24, 27, 27, 24, 21, 18 };
    private List<double> zList = new List<double> { -12, -8, -4, 0, 0, 9, 18, 18, 9, 18, 9, 0, 0, 9, 18, 27, 27, 27, 27, 27, 18, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 18, 18, 18, 18, 18, 18, 18, 18, 9, 9, 9, 9, 18, 18, 18, 18, 9, 9, 9, 9, 0, 0, 0, 0, 9, 9, 9, 9, 18, 18, 18, 18, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 0, 0, 0, 0 };
    public List<Model> worldObjects = new List<Model>();
    private List<IObserver<Command>> observers = new List<IObserver<Command>>();

    public Graph graph;
    private Tasks taskClass;
    private Robot robot1, robot2, robot3, robot4;
    private Truck truck;

    public World()
    {
      robot1 = CreateRobot("r1", 0, 0.301, 0, 50);
      robot2 = CreateRobot("r2", 0, 0.301, -4, 100);
      robot3 = CreateRobot("r3", 0, 0.301, -8, 150);
      robot4 = CreateRobot("r4", 0, 0.301, -12, 200);
      truck = CreateTruck(-150, 0.001, -24);
      MakeNodes(23);
      for(int i=0; i<4; i++){
        CreatePackage(-300, 1.401, -300);
      }
      graph = new Graph();
      graph = Graphy();
      taskClass = new Tasks(this);
    }
		
    public Graph Graphy()
    {
      Graph g = new Graph();

      g.add_vertex("null3", new Dictionary<string, int>() { { "null2", 4 } });
      g.add_vertex("null2", new Dictionary<string, int>() { { "null1", 4 }, { "null3", 4 } });
      g.add_vertex("null1", new Dictionary<string, int>() { { "A", 4 }, { "null2", 4 } });
      g.add_vertex("A", new Dictionary<string, int>() { { "B", 15 }, { "A1", 3 }, { "A2", 6 }, { "A3", 9 }, { "A4", 12 }, { "null1", 4 } });
      g.add_vertex("A1", new Dictionary<string, int>() { { "B", 12 } });
      g.add_vertex("A2", new Dictionary<string, int>() { { "B", 9 } });
      g.add_vertex("A3", new Dictionary<string, int>() { { "B", 6 } });
      g.add_vertex("A4", new Dictionary<string, int>() { { "B", 3 } });
      g.add_vertex("B", new Dictionary<string, int>() { { "C", 9 }, { "J", 9 }, { "B1", 3 }, { "B2", 6 }, { "B3", 9 }, { "B4", 12 } });
      g.add_vertex("B1", new Dictionary<string, int>() { { "J", 12 } });
      g.add_vertex("B2", new Dictionary<string, int>() { { "J", 9 } });
      g.add_vertex("B3", new Dictionary<string, int>() { { "J", 6 } });
      g.add_vertex("B4", new Dictionary<string, int>() { { "J", 3 } });
      g.add_vertex("C", new Dictionary<string, int>() { { "D", 9 }, { "F", 15 }, { "C1", 3 }, { "C2", 6 }, { "C3", 9 }, { "C4", 12 } });
      g.add_vertex("C1", new Dictionary<string, int>() { { "F", 12 } });
      g.add_vertex("C2", new Dictionary<string, int>() { { "F", 9 } });
      g.add_vertex("C3", new Dictionary<string, int>() { { "F", 6 } });
      g.add_vertex("C4", new Dictionary<string, int>() { { "F", 3 } });
      g.add_vertex("D", new Dictionary<string, int>() { { "E", 15 }, { "N", 9 }, { "D1", 3 }, { "D2", 6 }, { "D3", 9 }, { "D4", 12 } });
      g.add_vertex("D1", new Dictionary<string, int>() { { "E", 12 } });
      g.add_vertex("D2", new Dictionary<string, int>() { { "E", 9 } });
      g.add_vertex("D3", new Dictionary<string, int>() { { "E", 6 } });
      g.add_vertex("D4", new Dictionary<string, int>() { { "E", 3 } });
      g.add_vertex("E", new Dictionary<string, int>() { { "F", 9 }, { "G", 15 }, { "E1", 3 }, { "E2", 6 }, { "E3", 9 }, { "E4", 12 } });
      g.add_vertex("E1", new Dictionary<string, int>() { { "G", 12 } });
      g.add_vertex("E2", new Dictionary<string, int>() { { "G", 9 } });
      g.add_vertex("E3", new Dictionary<string, int>() { { "G", 6 } });
      g.add_vertex("E4", new Dictionary<string, int>() { { "G", 3 } });
      g.add_vertex("F", new Dictionary<string, int>() { { "A", 9 }, { "H", 15 }, { "F1", 3 }, { "F2", 6 }, { "F3", 9 }, { "F4", 12 } });
      g.add_vertex("F1", new Dictionary<string, int>() { { "H", 12 } });
      g.add_vertex("F2", new Dictionary<string, int>() { { "H", 9 } });
      g.add_vertex("F3", new Dictionary<string, int>() { { "H", 6 } });
      g.add_vertex("F4", new Dictionary<string, int>() { { "H", 3 } });
      g.add_vertex("G", new Dictionary<string, int>() { { "H", 9 }, { "R", 15 }, { "G1", 3 }, { "G2", 6 }, { "G3", 9 }, { "G4", 12 } });
      g.add_vertex("G1", new Dictionary<string, int>() { { "R", 12 } });
      g.add_vertex("G2", new Dictionary<string, int>() { { "R", 9 } });
      g.add_vertex("G3", new Dictionary<string, int>() { { "R", 6 } });
      g.add_vertex("G4", new Dictionary<string, int>() { { "R", 3 } });
      g.add_vertex("H", new Dictionary<string, int>() { { "I", 9 }, { "S", 15 }, { "H1", 3 }, { "H2", 6 }, { "H3", 9 }, { "H4", 12 } });
      g.add_vertex("H1", new Dictionary<string, int>() { { "S", 12 } });
      g.add_vertex("H2", new Dictionary<string, int>() { { "S", 9 } });
      g.add_vertex("H3", new Dictionary<string, int>() { { "S", 6 } });
      g.add_vertex("H4", new Dictionary<string, int>() { { "S", 3 } });
      g.add_vertex("I", new Dictionary<string, int>() { { "A", 15 }, { "I1", 3 }, { "I2", 6 }, { "I3", 9 }, { "I4", 12 } });
      g.add_vertex("I1", new Dictionary<string, int>() { { "A", 12 } });
      g.add_vertex("I2", new Dictionary<string, int>() { { "A", 9 } });
      g.add_vertex("I3", new Dictionary<string, int>() { { "A", 6 } });
      g.add_vertex("I4", new Dictionary<string, int>() { { "A", 3 } });
      g.add_vertex("J", new Dictionary<string, int>() { { "K", 9 } });
      g.add_vertex("K", new Dictionary<string, int>() { { "L", 9 }, { "C", 15 }, { "K1", 3 }, { "K2", 6 }, { "K3", 9 }, { "K4", 12 } });
      g.add_vertex("K1", new Dictionary<string, int>() { { "C", 12 } });
      g.add_vertex("K2", new Dictionary<string, int>() { { "C", 9 } });
      g.add_vertex("K3", new Dictionary<string, int>() { { "C", 6 } });
      g.add_vertex("K4", new Dictionary<string, int>() { { "C", 3 } });
      g.add_vertex("L", new Dictionary<string, int>() { { "M", 9 }, { "D", 15 }, { "L1", 3 }, { "L2", 6 }, { "L3", 9 }, { "L4", 12 } });
      g.add_vertex("L1", new Dictionary<string, int>() { { "D", 12 } });
      g.add_vertex("L2", new Dictionary<string, int>() { { "D", 9 } });
      g.add_vertex("L3", new Dictionary<string, int>() { { "D", 6 } });
      g.add_vertex("L4", new Dictionary<string, int>() { { "D", 3 } });
      g.add_vertex("M", new Dictionary<string, int>() { { "N", 15 }, { "M1", 3 }, { "M2", 6 }, { "M3", 9 }, { "M4", 12 } });
      g.add_vertex("M1", new Dictionary<string, int>() { { "N", 12 } });
      g.add_vertex("M2", new Dictionary<string, int>() { { "N", 9 } });
      g.add_vertex("M3", new Dictionary<string, int>() { { "N", 6 } });
      g.add_vertex("M4", new Dictionary<string, int>() { { "N", 3 } });
      g.add_vertex("N", new Dictionary<string, int>() { { "O", 15 }, { "N1", 3 }, { "N2", 6 }, { "N3", 9 }, { "N4", 12 } });
      g.add_vertex("N1", new Dictionary<string, int>() { { "O", 12 } });
      g.add_vertex("N2", new Dictionary<string, int>() { { "O", 9 } });
      g.add_vertex("N3", new Dictionary<string, int>() { { "O", 6 } });
      g.add_vertex("N4", new Dictionary<string, int>() { { "O", 3 } });
      g.add_vertex("O", new Dictionary<string, int>() { { "P", 15 }, { "E", 15 }, { "O1", 3 }, { "O2", 6 }, { "O3", 9 }, { "O4", 12 } });
      g.add_vertex("O1", new Dictionary<string, int>() { { "P", 12 } });
      g.add_vertex("O2", new Dictionary<string, int>() { { "P", 9 } });
      g.add_vertex("O3", new Dictionary<string, int>() { { "P", 6 } });
      g.add_vertex("O4", new Dictionary<string, int>() { { "P", 3 } });
      g.add_vertex("P", new Dictionary<string, int>() { { "Q", 15 }, { "G", 15 }, { "P1", 3 }, { "P2", 6 }, { "P3", 9 }, { "P4", 12 } });
      g.add_vertex("P1", new Dictionary<string, int>() { { "Q", 12 } });
      g.add_vertex("P2", new Dictionary<string, int>() { { "Q", 9 } });
      g.add_vertex("P3", new Dictionary<string, int>() { { "Q", 6 } });
      g.add_vertex("P4", new Dictionary<string, int>() { { "Q", 3 } });
      g.add_vertex("Q", new Dictionary<string, int>() { { "R", 9 } });
      g.add_vertex("R", new Dictionary<string, int>() { { "S", 9 } });
      g.add_vertex("S", new Dictionary<string, int>() { { "T", 9 } });
      g.add_vertex("T", new Dictionary<string, int>() { { "I", 15 }, { "T1", 3 }, { "T2", 6 }, { "T3", 9 }, { "T4", 12 } });
      g.add_vertex("T1", new Dictionary<string, int>() { { "I", 12 } });
      g.add_vertex("T2", new Dictionary<string, int>() { { "I", 9 } });
      g.add_vertex("T3", new Dictionary<string, int>() { { "I", 6 } });
      g.add_vertex("T4", new Dictionary<string, int>() { { "I", 3 } });

      return g;
    }

    public void TruckHere()
    {
      for(int i=0; i<4; i++){
      CreatePackage(-300, 1.401, -300);
      }
      RobotOnPath(robot1, "A");
      RobotOnPath(robot2, "null1");
      RobotOnPath(robot3, "null2");
      RobotOnPath(robot4, "null3");
    }

    private Robot CreateRobot(string rName, double x, double y, double z, int counter)
    {
      Robot r = new Robot(rName, x, y, z, x, y, z, 0, 0, 0, counter);
      worldObjects.Add(r);
      return r;
    }

    private Truck CreateTruck(double x, double y, double z)
    {
      Truck t = new Truck(x, y, z, 0, (90 * Math.PI / 180), 0);
      worldObjects.Add(t);
      return t;
    }

    public void CreatePackage(double x, double y, double z)
    {
      Package s = new Package(x, y, z, 0, 0, 0);
      worldObjects.Add(s);
    }

    private void RobotOnPath(Robot r, string begin)
    {
      if (r.robotPath == false)
      {
        r.ChangeRobotPath(true);
        Random rnd = new Random();
        int goal = rnd.Next(23, 87);
        string target = iList[goal];
        List<string> path = FindRoute(begin, target);
        r.GetPath(target, path, iList, xList, zList);
      }
    }

    public void RobotGoesBack(Robot r, string begin, string end)
    {
      r.ChangeRobotDropped(false);
      List<string> path = FindRoute(begin, end);
      r.GetPath(r.target, path, iList, xList, zList);
    }

    private List<string> FindRoute(string start, string destination)
    {
      List<string> Points = new List<string>();

      graph.shortest_path(start, destination).ForEach(x => Points.Add(x));

      Points.Reverse();

      for (var i = 0; i < Points.Count; i++)
      {
      }

      return Points;
    }

    private void MakeNodes(int nrNodes)
    {
      for (int c = 0; c < nrNodes; c++)
      {
        string i = iList[c];
        double x = xList[c];
        double z = zList[c];

        Node n = new Node(i, x, 0, z, 0, 0, 0);
        worldObjects.Add(n);
      }
    }

    public IDisposable Subscribe(IObserver<Command> observer)
    {
      if (!observers.Contains(observer))
      {
        observers.Add(observer);

        SendCreationCommandsToObserver(observer);
      }
      return new Unsubscriber<Command>(observers, observer);
    }

    private void SendCommandToObservers(Command c)
    {
      for (int i = 0; i < this.observers.Count; i++)
      {
        this.observers[i].OnNext(c);
      }
    }

    private void SendCreationCommandsToObserver(IObserver<Command> obs)
    {
      foreach (Model m3d in worldObjects)
      {
        obs.OnNext(new UpdateModel3DCommand(m3d));
      }
    }

    public bool Update(int tick)
    {
      this.taskClass.Update();

      for (int i = 0; i < worldObjects.Count; i++)
      {
        Model u = worldObjects[i];

        if (u is IUpdatable)
        {
          bool needsCommand = ((IUpdatable)u).Update(tick);

          if (needsCommand)
          {
            SendCommandToObservers(new UpdateModel3DCommand(u));
          }
        }
      }

      return true;
    }
  }

  internal class Unsubscriber<Command> : IDisposable
  {
    private List<IObserver<Command>> _observers;
    private IObserver<Command> _observer;

    internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
    {
      this._observers = observers;
      this._observer = observer;
    }

    public void Dispose()
    {
      if (_observers.Contains(_observer))
        _observers.Remove(_observer);
    }
  }
}
