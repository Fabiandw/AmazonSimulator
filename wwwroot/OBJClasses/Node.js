class Nodemodel extends THREE.Group
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

		var nodeGeometry = new THREE.PlaneGeometry(2, 2, 2);
		var nodeMaterial = new THREE.MeshLambertMaterial({ color: 0xdc4500, side: THREE.DoubleSide });
		var node = new THREE.Mesh(nodeGeometry, nodeMaterial);

		this.add(node);
	}
}
