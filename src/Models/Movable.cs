using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Controllers;
using System.Threading;

namespace Models
{
    public abstract class Movable : Model
    {
        public Movable(String type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
            : base(type, x, y, z, rotationX, rotationY, rotationZ)
        {
        }

        private double _tX = 0;
        private double _tY = 0;
        private double _tZ = 0;
        public double tX => _tX;
        public double tY => _tY;
        public double tZ => _tZ;
        private string _target;
        public string target => _target;

        public void MoveTarget(double targetX, double targetY, double targetZ)
        {
            this._tX = targetX;
            this._tY = targetY;
            this._tZ = targetZ;

            this.needsUpdate = true;
        }

        public void ChangeTarget(string b)
        {
            _target = b;
        }
    }
}