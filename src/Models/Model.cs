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
    public abstract class Model : IUpdatable
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string type { get; }
        public Guid guid { get; set; }
        public double x => _x;
        public double y => _y;
        public double z => _z;
        public double rotationX => _rX;
        public double rotationY => _rY;
        public double rotationZ => _rZ;
        public bool needsUpdate = true;

        public Model(String type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = type;
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }

        public void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            this.needsUpdate = true;
        }

        public void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            this.needsUpdate = true;
        }

        public virtual bool Update(int tick)
        {
            if (this.needsUpdate)
            {
                this.needsUpdate = false;
                return true;
            }

            return false;
        }
    }
}