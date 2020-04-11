using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
	public class Package : Model
	{
		private double targetZ, targetY;
		public bool dropped = false;
		public Package(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("package", x, y, z, rotationX, rotationY, rotationZ)
		{
			

		}
		public void Store(){
			Random rnd = new Random();
			double random = rnd.Next(1,4);
			switch(random){
				case 1:
				this.targetY = this.y+0.1;
				break;
				case 2:
				this.targetY = this.y+2.9;
				break;
				case 3:
				this.targetY = this.y+5.5;
				break;

			}
			this.targetZ = this.z+3;
			this.dropped=true;
			this.needsUpdate = true;
		}
		public override bool Update(int tick){
			if(dropped){
				if (this.y <= targetY){
				this.Move(this.x, this.y+0.1, this.z);
				return true;		
				}		
				if (this.z <= targetZ){
				this.Move(this.x, this.y, this.z+0.1);
				return true;
				}
				
			dropped = false;
			}
			
			return base.Update(tick);
		}
		
	}
}
