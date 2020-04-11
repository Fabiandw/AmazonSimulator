class Robotmodel extends THREE.Group
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

		var geometry = new THREE.BoxGeometry(1.8, 0.6, 1.8);
		var cubeMaterials =
			[
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_side.png"), side: THREE.DoubleSide }), //LEFT
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_top.png"), side: THREE.DoubleSide }), //TOP
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_front.png"), side: THREE.DoubleSide }), //FRONT
				new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("Textures/Robot/robot_front.png"), side: THREE.DoubleSide }), //BACK
			];

		var material = new THREE.MeshFaceMaterial(cubeMaterials);
		var robot = new THREE.Mesh(geometry, material);

		robot.recieveShadow = true;
		robot.castShadow = true;

		// Voorlicht
		addSpotLight(selfRef, 0xffffff, - 0.5, 0, 0.5, 1, 0, 0, 4);
		addSpotLight(selfRef, 0xffffff, 0.5, 0, 0.5, 1, 0, 0, 4);
		addPointLight(selfRef, 0xffffff, -0.5, 0.5, 1, 1, 5);
		addPointLight(selfRef, 0xffffff, 0.5, 0.5, 1, 1, 5);
		// Achterlicht
		addPointLight(selfRef, 0xd80808, -0.5, 0, -1, 2, 5);
		addPointLight(selfRef, 0xd80808, 0.5, 0, -1, 2, 5);

		this.add(robot);
	}
}
