using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Controllers;

namespace Models
{
    public class Node : Model
    {
        private string _i;

        public Node(string i, double x, double y, double z, double rotationX, double rotationY, double rotationZ) :
            base("node", x, y, z, rotationX, rotationY, rotationZ)
        {
            Move(this.x, -0.001, this.z);
            Rotate(Math.PI / 2, this.rotationY, this.rotationZ);
            _i = i;
        }
    }
}