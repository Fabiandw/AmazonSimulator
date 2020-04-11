class Packagemodel extends THREE.Group
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

		var geometry = new THREE.BoxGeometry(1.9, 1.9, 1.9);
		var material = new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/package/cratejpg.jpg"), side: THREE.DoubleSide});
		var box = new THREE.Mesh(geometry, material);
		this.add(box)
	}
}
