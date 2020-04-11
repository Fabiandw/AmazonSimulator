using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : Model
    {
        private int _counter = 100;
        private bool _truckHere = false;
        private bool _truckEmpty = false;
        public bool truckHere => _truckHere;
        public bool truckEmpty => _truckEmpty;

        public Truck(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("truck",
            x, y, z, rotationX, rotationY, rotationZ)
        {
        }

        public void ChangeTruckHere(bool b)
        {
            this._truckHere = b;
        }

        public void ChangeTruckEmpty(bool b)
        {
            this._truckEmpty = b;
        }

        public override bool Update(int tick)
        {
            if (this.x == 0)
            {
                ChangeTruckHere(true);
            }
            else
            {
                ChangeTruckHere(false);
            }

            if (_truckHere && !_truckEmpty)
            {
            }

            else if (!_truckHere && !_truckEmpty)
            {
                this.Move(this.x + 1, this.y, this.z);
            }

            else if (_truckEmpty == true)
            {
                if (_counter <= 0)
                {
                    this.Move(this.x + 2, this.y, this.z);
                    ChangeTruckHere(false);
                }

                _counter--;
            }

            return base.Update(tick);
        }

        public void RESET()
        {
            ChangeTruckEmpty(false);
            ChangeTruckHere(false);
            this.Move(-150, this.y, this.z);
        }
    }
}