class TruckModel extends THREE.Group
{
	constructor()
	{
		super();

		this._loadState = LoadStates.NOT_LOADING;

		this.init();
	}

	get loadState()
	{
		return this._loadState;
	}

	init()
	{
		
		if (this._loadState != LoadStates.NOT_LOADING) return;

		this._loadState = LoadStates.LOADING;

		var selfRef = this;

		loadOBJModel('/Textures/Truck/', 'isuzu.obj','/Textures/Truck/', 'isuzu.mtl', (mesh) => {
			//mesh.rotation.y = Math.PI / 2.0;
			mesh.scale.set(3,3,3)
			selfRef.add(mesh)
			
		})
	}
}
