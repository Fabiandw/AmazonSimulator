using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Models
{
  public class Tasks
  {
    private int ReadySetGo = 0;
    private int cycles = 0;

    private World world;
    private List<Model> worldObjects;

    public Tasks(World world, List<Model> worldObjects)
    {
      this.world = world;
      this.worldObjects = worldObjects;
    }

    public void Update()
    {
      Truck truck = (Truck)worldObjects[4];
      Robot robot1 = (Robot)worldObjects[0];
      Robot robot2 = (Robot)worldObjects[1];
      Robot robot3 = (Robot)worldObjects[2];
      Robot robot4 = (Robot)worldObjects[3];
      Package package1 = (Package)worldObjects[28 + (4 * cycles)];
      Package package2 = (Package)worldObjects[29 + (4 * cycles)];
      Package package3 = (Package)worldObjects[30 + (4 * cycles)];
      Package package4 = (Package)worldObjects[31 + (4 * cycles)];

      if (truck.truckHere == true)
      {
        robot1.ChangeTruckHere(true);
        robot2.ChangeTruckHere(true);
        robot3.ChangeTruckHere(true);
        robot4.ChangeTruckHere(true);
      }

      if (robot1.robotReady == true)
      {
        MovePackage(robot1, package1, robot1.x, robot1.y, robot1.z);
        robot1.ChangePackage(package1);
        robot1.ChangeRobotReady(false);
        robot1.ChangeRobotLoaded(true);
        ReadySetGo++;
      }

      if (robot2.robotReady == true)
      {
        MovePackage(robot2, package2, robot2.x, robot2.y, robot2.z);
        robot2.ChangePackage(package2);
        robot2.ChangeRobotReady(false);
        robot2.ChangeRobotLoaded(true);
        ReadySetGo++;
      }

      if (robot3.robotReady == true)
      {
        MovePackage(robot3, package3, robot3.x, robot3.y, robot3.z);
        robot3.ChangePackage(package3);
        robot3.ChangeRobotReady(false);
        robot3.ChangeRobotLoaded(true);
        ReadySetGo++;
      }

      if (robot4.robotReady == true)
      {
        MovePackage(robot4, package4, robot4.x, robot4.y, robot4.z);
        robot4.ChangePackage(package4);
        robot4.ChangeRobotReady(false);
        robot4.ChangeRobotLoaded(true);
        ReadySetGo++;
      }

      if (robot1.robotDropped == true)
      {
        world.RobotGoesBack(robot1, robot1.target, "A");
      }

      if (robot2.robotDropped == true)
      {
        world.RobotGoesBack(robot2, robot2.target, "null1");
      }

      if (robot3.robotDropped == true)
      {
        world.RobotGoesBack(robot3, robot3.target, "null2");
      }

      if (robot4.robotDropped == true)
      {
        world.RobotGoesBack(robot4, robot4.target, "null3");
      }

      if (ReadySetGo == 4)
      {
        truck.ChangeTruckEmpty(true);
        robot1.ChangeRobotDone(false);
        robot2.ChangeRobotDone(false);
        robot3.ChangeRobotDone(false);
        robot4.ChangeRobotDone(false);
        ReadySetGo++;
        world.TruckHere();
      }

      if (robot1.robotReset == true && robot2.robotReset == true && robot3.robotReset == true && robot4.robotReset == true)
      {
        ReadySetGo = 0;
        cycles++;
        robot1.RESET();
        robot2.RESET();
        robot3.RESET();
        robot4.RESET();
        truck.RESET();
      }
    }

    private void MovePackage(Robot r, Package s, double x, double y, double z)
    {
      s.Move(r.x, s.y, r.z);
      s.needsUpdate = true;
    }
  }
}
