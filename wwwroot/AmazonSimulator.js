const LoadStates = Object.freeze
	(
	{
		"NOT_LOADING": 1,
		"LOADING": 2,
		"LOADED": 3
	}
	);

function parseCommand(input = "")
{
	return JSON.parse(input);
}

let webSocket
let camera, scene, renderer;
let container;
let cameraControls;
let worldObjects = {};

function loadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
    new THREE.MTLLoader()
        .setPath(texturePath)
        .load(textureName, function (materials) {
            materials.preload();

            new THREE.OBJLoader()
                .setPath(modelPath)
                .setMaterials(materials)
                .load(modelName, function (object) {
                    onload(object);
                }, function () { }, function (e) { console.log("Error loading model"); console.log(e); });
        });
}

let createCamera = () => {
	camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 10000);
	cameraControls = new THREE.OrbitControls(camera);
	camera.position.z = 15;
	camera.position.y = 15;
	camera.position.x = 15;
	cameraControls.update();
}


let createRenderer = () => {
	renderer = new THREE.WebGLRenderer({ antialias: true });
	renderer.setPixelRatio(window.devicePixelRatio);
	renderer.setSize(window.innerWidth, window.innerHeight + 5);
	renderer.shadowMap.enabled = true;
	renderer.shadowMap.type = THREE.PCFSoftShadowMap;
	document.body.appendChild(renderer.domElement);
}

let createSkypbox = () => {
	var skyboxGeometry = new THREE.SphereGeometry(10000, 32, 32);
    var skyboxMaterial = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/skyboxtest.jpg"), side: THREE.DoubleSide });
    var skybox = new THREE.Mesh(skyboxGeometry, skyboxMaterial);
    scene.add(skybox);
}

let createRoad = () => {

	let road = new THREE.Group();
	loadOBJModel("Textures/Road/", "untitled.obj", "Textures/Road/", "untitled.mtl", (mesh) => {
		mesh.scale.set(3, 3, 3);
		//mesh.castShadow = true;
		road.add(mesh);
	})

	//road.rotation.y = Math.PI / 2.0;
	road.position.x = 0;
	road.position.y = -3.3;
	road.position.z = -25.8;
	scene.add(road);
}

let createWall = (x,z,horizontal) => {
	var geometry = new THREE.BoxGeometry(7, 7, 0.5);
	var material = new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/wall3.jpg"), side: THREE.DoubleSide});
	var wall = new THREE.Mesh(geometry, material);
	wall.position.x = x;
	wall.position.y = 3.5;
	wall.position.z = z;
	horizontal? wall.rotation.y = Math.PI / 2 : 0
	scene.add(wall)
}

let createShelf = (x,z) => {

	var shelf = new THREE.Group();
	loadOBJModel('/Textures/Shelves/', 'model.obj','/Textures/Shelves/', 'materials.mtl', (mesh) => {
		//mesh.rotation.y = Math.PI / 2.0;
		mesh.scale.set(4,5,4.5)
		shelf.add(mesh)
	})
	shelf.position.x = x;
	shelf.position.y = 1.9;
	shelf.position.z = z;
	scene.add(shelf);
}
let createDock = (x,z) => {

	var geometry = new THREE.BoxGeometry(9, 3, 3);
	var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/warehouse/textures/Wall1.jpg"), side: THREE.DoubleSide});
	var connector = new THREE.Mesh(geometry, material);
	connector.position.y = 1.51;
	connector.position.x = -11.5;
	connector.castShadow = true;
	connector.recieveShadow = true;
	var dock = new THREE.Group();
	loadOBJModel('/Textures/warehouse/', 'smallgarage.obj','/Textures/warehouse/', 'smallgarage.mtl', (mesh) => {
		//mesh.rotation.y = Math.PI / 2.0;
		mesh.scale.set(0.018,0.0226,0.028)
		mesh.castShadow = true;
		mesh.recieveShadow = true;
		dock.add(mesh)
	})
	dock.rotation.y = Math.PI / 2.0;
	dock.position.x = x;
	dock.position.y = 0.01;
	dock.position.z = z;
	dock.add(connector);
	scene.add(dock);

	var geometry = new THREE.PlaneGeometry(1.8, 0.6, 1.8);
	var material = new THREE.MeshBasicMaterial( {color: 0x000000, side: THREE.DoubleSide} );
	var plane = new THREE.Mesh( geometry, material );
	plane.rotation.y = Math.PI / 2.0;
	plane.position.x = 1.5001;
	plane.position.z = 0;
	plane.position.y = 0.3;
	scene.add( plane );

	var geometry = new THREE.PlaneGeometry(1.8, 0.6, 1.8);
	var material = new THREE.MeshBasicMaterial( {color: 0x000000, side: THREE.DoubleSide} );
	var plane = new THREE.Mesh( geometry, material );
	//plane.rotation.y = Math.PI / 2.0;
	plane.position.x = 0;
	plane.position.z = 2.001;
	plane.position.y = 0.3;
	scene.add( plane );

	var geometry = new THREE.PlaneGeometry(2, 2.4, 2);
	var material = new THREE.MeshBasicMaterial( {color: 0x000000, side: THREE.DoubleSide} );
	var plane = new THREE.Mesh( geometry, material );
	plane.rotation.y = Math.PI / 2.0;
	plane.position.x = -1.5001;
	plane.position.z = 0;
	plane.position.y = 1.2;
	scene.add( plane );
}

let createFloor = (x,z) => {
	var geometry = new THREE.PlaneGeometry(x, z, 70);
	var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/floor-min.jpg", function(texture){
		texture.wrapS = texture.wrapT = THREE.repeatWrapping;
		texture.offset.set(0,0);
		texture.repeat.set(x,z);
	}), side: THREE.DoubleSide });
	var plane = new THREE.Mesh(geometry, material);
	plane.rotation.x -= Math.PI / 2.0;
	plane.position.x = 0;
	plane.position.z = 0;
	plane.position.y = 0;
	plane.recieveShadow = true;
	plane.castShadow = true;
	scene.add(plane);
}

let createMeshes = () => {

	//floor
	createFloor(70,70)
	//walls
	for (i = -31.5;i<35 ;i+=7 ){
	createWall(i,35, false);
	createWall(i, -20.75, false);
	}
	for (i= -17.5; i<35; i+=7){
		createWall(-35, i, true);
		createWall(35,i,true);
	}
	//shelves
	for (i=3; i<=30; i+=9){
		createShelf(-25.5,i);
		createShelf(-19.5,i);
		createShelf(-10.5,i);
		createShelf(-4.5,i);
		createShelf(4.5,i);
		createShelf(10.5,i);
		createShelf(25.5,i);
		createShelf(19.5,i);
	}
	//dock
	createDock(0,-14);
	//road
	createRoad();
}

let createLights = () => {

	const ambientLight = new THREE.HemisphereLight(
		0xddeeff, //bright sky colour
		0x202020, //dim ground colour
		0.1 //intensity
	  );
	var light = new THREE.DirectionalLight(0xffffff);
	light.position.set(15, 10, 15);
	light.target.position.set(0, 0, 0);
	light.castShadow = true;
	light.shadowDarkness = 0.5;
	light.shadowCameraNear = 0.1;
	light.shadowCameraFar = 5;
	light.shadowCameraLeft = -0.5;
	light.shadowCameraRight = 0.5;
	light.shadowCameraTop = 0.5;
	light.shadowCameraBottom = -0.5;
	scene.add(ambientLight, light);	
}

function addSpotLight(object, color, x, y, z, intensity, targetx, targety, targetz){

	var spotLight = new THREE.SpotLight(color, intensity, 100, 0.5, 2, 1);
	spotLight.position.set(x, y, z);
	
	object.add(spotLight);
	object.add(spotLight.target);
	spotLight.target.position.set(targetx, targety, targetz)
	
}

function addPointLight(object, color, x, y, z, intensity, distance){

	var pointLight = new THREE.PointLight(color, intensity, distance);
	pointLight.position.set(x, y, z);
	object.add(pointLight);
}

let onWindowResize = () => {

	camera.aspect = window.innerWidth / window.innerHeight;
	camera.updateProjectionMatrix();
	renderer.setSize(window.innerWidth, window.innerHeight);
}

let animate = () => {
	requestAnimationFrame(animate);
	cameraControls.update();
	renderer.render(scene, camera);
}

let init = () => {
	scene = new THREE.Scene();

	createCamera();
	createRenderer();
	createMeshes();
	createLights();
	createSkypbox();
}

let onSocketMessage = (event) => {
	var command = parseCommand(event.data);

		if (command.command = "update")
		{
			if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
			{
				if (command.parameters.type == "robot")
				{
					console.log(command);
					robot = new Robotmodel();

					var group = new THREE.Group();
					group.add(robot);

					scene.add(group);
					worldObjects[command.parameters.guid] = group;
				}
			}

			if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
			{
				if (command.parameters.type == "truck")
				{
					console.log(command);
					th = new TruckModel();
					var group = new THREE.Group();
					group.add(th);

					scene.add(group);
					worldObjects[command.parameters.guid] = group;
				}
			}

			if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
			{
				if (command.parameters.type == "package")
				{
					console.log(command);
					package = new Packagemodel();

					var group = new THREE.Group();
					group.add(package);

					scene.add(group);
					worldObjects[command.parameters.guid] = group;
				}
			}

			if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
			{
				if (command.parameters.type == "node")
				{
					console.log(command);
					node = new Nodemodel();

					var group = new THREE.Group();
					group.add(node);

					scene.add(group);
					worldObjects[command.parameters.guid] = group;
				}
			}

			if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
			{
				if (command.parameters.type == "shelf")
				{
					console.log(command);
					shelf = new ShelfModel();

					var group = new THREE.Group();
					group.add(shelf);

					scene.add(group);
					worldObjects[command.parameters.guid] = group;
				}
			}

			var object = worldObjects[command.parameters.guid];

			object.position.x = command.parameters.x;
			object.position.y = command.parameters.y;
			object.position.z = command.parameters.z;

			object.rotation.x = command.parameters.rotationX;
			object.rotation.y = command.parameters.rotationY;
			object.rotation.z = command.parameters.rotationZ;
		}
}

webSocket = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/connect_client");
webSocket.onmessage = (event) => {
	onSocketMessage(event)
}

window.addEventListener('resize', onWindowResize, false);
window.onload = () => {
	init();
	animate();
}
