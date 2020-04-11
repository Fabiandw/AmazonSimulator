using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Controllers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
  public class Robot : Movable
  {
    private int hazRun = 0;
    private int hazRunTheSecond = 0;
    private int _counter;
    private int ogCounter;

    private Package _package;

    public Package package { get { return _package; } }

    private bool _truckHere = false;
    private bool _robotPath = false;
    private bool _robotReady = false;
    private bool _robotLoaded = false;
    private bool _robotDropped = false;
    private bool _robotPlaced = false;
    private bool _robotDone = true;
    private bool _robotReset = false;

    public bool truckHere { get { return _truckHere; } }
    public bool robotPath { get { return _robotPath; } }
    public bool robotReady { get { return _robotReady; } }
    public bool robotLoaded { get { return _robotLoaded; } }
    public bool robotDropped { get { return _robotDropped; } }
    public bool robotPlaced { get { return _robotPlaced; } }
    public bool robotDone { get { return _robotDone; } }
    public bool robotReset { get { return _robotReset; } }

    public Robot(string rName, double targetX, double targetY, double targetZ, double x, double y, double z, double rotationX, double rotationY, double rotationZ, int counter) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
    {
      MoveTarget(targetX, targetY, targetZ);

      this.ogCounter = counter;
      this._counter = counter;
    }

    public async void GetPath(string target, List<string> path, List<string> iList, List<double> xList, List<double> zList)
    {
      ChangeTarget(target);

      for (int i = 0; i < path.Count(); i++)
      {
        string next = path[i];
        int nodeindex = iList.IndexOf(next);
        double tx = xList[nodeindex];
        double tz = zList[nodeindex];
        this.MoveTarget(tx, 0.301, tz);
        await Task.Delay(2000);
        hazRun++;
      }

      ChangeRobotPlaced(true);

      if (hazRun == path.Count() && hazRunTheSecond == 0)
      {
        ChangeRobotDropped(true);
        package.Store();
        ChangePackage(null);

        hazRun = 0;
        hazRunTheSecond++;
      }

      if (hazRun == path.Count() && hazRunTheSecond == 1)
      {
        this.Rotate(this.rotationX, 0, this.rotationZ);
        ChangeRobotReset(true);
        this.needsUpdate = true;
      }
    }

    public void MoveBetween(double x, double y, double z)
    {
      if (package != null)
      {
        package.Move(x, package.y, z);
      }

      this.Move(x, y, z);

      this.needsUpdate = true;
    }

    public void ChangePackage(Package b)
    {
      this._package = b;
    }

    public void ChangeTruckHere(bool b)
    {
      this._truckHere = b;
    }

    public void ChangeRobotPath(bool b)
    {
      this._robotPath = b;
    }

    public void ChangeRobotReady(bool b)
    {
      this._robotReady = b;
    }

    public void ChangeRobotLoaded(bool b)
    {
      this._robotLoaded = b;
    }

    public void ChangeRobotDropped(bool b)
    {
      this._robotDropped = b;
    }

    public void ChangeRobotPlaced(bool b)
    {
      this._robotPlaced = b;
    }

    public void ChangeRobotDone(bool b)
    {
      this._robotDone = b;
    }

    public void ChangeRobotReset(bool b)
    {
      this._robotReset = b;
    }

    public override bool Update(int tick)
    {
      if (this.x >= this.tX - 0.1 && this.x <= this.tX + 0.1)
      {
        if (this.z >= this.tZ - 0.1 && this.z <= this.tZ + 0.1)
        {

        }

        else
        {
          if (this.z < this.tZ)
          {
            this.MoveBetween(this.x, this.y, this.z + 0.2);
            this.Rotate(this.rotationX, 0, this.rotationZ);
          }

          else if (this.z > this.tZ)
          {
            this.MoveBetween(this.x, this.y, this.z - 0.2);
            this.Rotate(this.rotationX, (-Math.PI), this.rotationZ);
          }
        }
      }

      else
      {
        if (this.x < this.tX)
        {
          this.MoveBetween(this.x + 0.2, this.y, this.z);
          this.Rotate(this.rotationX, (Math.PI / 2), this.rotationZ);
        }

        else if (this.x > this.tX)
        {
          this.MoveBetween(this.x - 0.2, this.y, this.z);
          this.Rotate(this.rotationX, (-Math.PI / 2), this.rotationZ);
        }
      }

      if (truckHere && _counter > 0)
      {
        _counter--;
      }

      if (_counter <= 0 && !robotLoaded)
      {
        ChangeRobotReady(true);
      }

      return base.Update(tick);
    }

    public void RESET()
    {
      hazRun = 0;
      hazRunTheSecond = 0;
      _counter = ogCounter;
      ChangeTruckHere(false);
      ChangeRobotPath(false);
      ChangeRobotReady(false);
      ChangeRobotLoaded(false);
      ChangeRobotDropped(false);
      ChangeRobotPlaced(false);
      ChangeRobotDone(true);
      ChangeRobotReset(false);

    }
  }
}
