using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour {
	[SerializeField] GameObject helpScreen;
	[SerializeField] GameObject helpInstructions;

    public int columns = 8;
    public int rows = 8;
    public GameObject[] floorTiles;
    public GameObject[] players;
    public GameObject[] selectors;
    public GameObject[] laserEmitters;
    public GameObject[] laserRecievers;
    public GameObject[] laserBeams;
    public GameObject[] doors;
    public GameObject[] walls;
    public GameObject[] treasures;
    public GameObject looseText;
    public GameObject[] mirrors;
    public GameObject[] lenses;
    public GameObject[] guards;
    public GameObject[] sounds;
    public GameObject[] onGuardTiles;
    //public GUIText timeGui;

	private int level = 0;
    private int securityLevel = 0;
    //private float timer;
    private Vector2 playerPos, selectorPos;
    private Vector3[] emitterPos, guardPos, receiverPos;
    private Vector2[] wallPos, doorPos;
    private List<Vector2> lensPos, mirrorPos, treasurePos;
    private GameObject[,] gameBoard;
    private int[,] gameBoardRotations;
    private GameObject board;
    private Transform boardHolder;
    //private List<Vector3> gridPositions = new List<Vector3>();

    void LevelSetup() {
		if (level == 0) {
            rows = 5;
            columns = 5;
        } else if (level == 1) {
            rows = 8;
            columns = 8;
        } else if (level == 2) {
            rows = 10;
            columns = 10;
        } else if (level == 3) {
            rows = 8;
            columns = 8;
        } else if (level == 4) {
            rows = 8;
            columns = 8;
        }
        board = new GameObject("Board");
        boardHolder = board.transform;
        DeleteCurrentBoard();
        gameBoard = new GameObject[rows, columns];
        gameBoardRotations = new int[rows, columns];
        lensPos = new List<Vector2>();
        mirrorPos = new List<Vector2>();
		if (level < 5) {
			if (level == 0) {
				TestLevelSetup ();
			} else if (level == 1) {
				LevelOneSetup ();
			} else if (level == 2) {
				LevelTwoSetup ();
			} else if (level == 3) {
				LevelThreeSetup ();
			} else if (level == 4) {
				LevelFourSetup ();
			}
			InitializeGameBoard ();
		} else {
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main")) {
				if (!SceneManager.GetSceneByName ("End Screen").isLoaded) {
					SceneManager.LoadScene ("End Screen");
				}
				SceneManager.SetActiveScene (SceneManager.GetSceneByName ("End Screen"));
				SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName ("Main"));
				level = 0;
			}
		}
    }
		
    void TestLevelSetup() {
		helpInstructions.SetActive (true);
        playerPos = new Vector2(4, 0);
        selectorPos = new Vector2(0, 0);
        treasurePos = new List<Vector2>();
        treasurePos.Add(new Vector2(2, 4));
        wallPos = new Vector2[] { new Vector2(1, 4), new Vector2(3, 4), new Vector2(1, 3), new Vector2(3, 3) };
        emitterPos = new Vector3[] { new Vector3(0, 1, 0) };
        receiverPos = new Vector3[] { new Vector3(4, 1, 0) };
        doorPos = new Vector2[] { new Vector2(2, 3) };
        guardPos = new Vector3[] { new Vector3(0, 4, 0) };
    }

    void LevelOneSetup() {
		helpInstructions.SetActive (true);
        playerPos = new Vector2(4, 0);
        selectorPos = new Vector2(0, 0);
        treasurePos = new List<Vector2>();
        treasurePos.Add(new Vector2(3, 7));
        wallPos = new Vector2[] { new Vector2(2, 5), new Vector2(2, 6), new Vector2(2, 7),
            new Vector2(4, 5), new Vector2(4, 6), new Vector2(4, 7) };
        //Z coordinate used to determine which way laser comes out of emitter
        //0 = right, 1 = left, 2 = up, 3 = down
        emitterPos = new Vector3[] { new Vector3(0, 3, 0), new Vector3(0, 2, 0) };
        receiverPos = new Vector3[] { new Vector3(7, 3, 0), new Vector3(7, 2, 0) };
        doorPos = new Vector2[] { new Vector2(3, 5) };
        guardPos = new Vector3[] { new Vector3(7, 7, 0) };
    }

    void LevelTwoSetup() {
		helpInstructions.SetActive (false);
        playerPos = new Vector2(0, 9);
        selectorPos = new Vector2(0, 0);
        treasurePos = new List<Vector2>();
        treasurePos.Add(new Vector2(4, 7));
        treasurePos.Add(new Vector2(9, 8));
        wallPos = new Vector2[] {
            new Vector2 (3, 8),
            new Vector2 (3, 7),
            new Vector2 (3, 6),
            new Vector2 (4, 8),
            new Vector2 (5, 8),
            new Vector2 (5, 6),
            new Vector2 (8, 9),
            new Vector2 (8, 7),
            new Vector2 (9, 9),
            new Vector2 (9, 7)
        };
        //Z coordinate used to determine which way laser comes out of emitter
        //0 = right, 1 = left, 2 = up, 3 = down
        emitterPos = new Vector3[] { new Vector3(1, 0, 2) };
        receiverPos = new Vector3[] { new Vector3(1, 9, 3), new Vector3(9, 1, 0) };
        doorPos = new Vector2[] { new Vector2(4, 6), new Vector2(5, 7), new Vector2(8, 8) };
        guardPos = new Vector3[] { new Vector3(3, 3, 0), new Vector3(9, 6, 0) };
    }

    void LevelThreeSetup() {
		helpInstructions.SetActive (false);
        playerPos = new Vector2(3, 0);
        selectorPos = new Vector2(0, 0);
        treasurePos = new List<Vector2>();
        treasurePos.Add(new Vector2(7, 1));
        treasurePos.Add(new Vector2(7, 5));
        wallPos = new Vector2[6] { new Vector2(7, 0), new Vector2(6, 0), new Vector2(7, 4),
            new Vector2(6, 4), new Vector2(7, 6), new Vector2(6, 6) };
        //Z coordinate used to determine which way laser comes out of emitter
        //0 = right, 1 = left, 2 = up, 3 = down
        emitterPos = new Vector3[] { new Vector3(2, 7, 3), new Vector3(7, 2, 1) };
        receiverPos = new Vector3[] { new Vector3(7, 3, 3), new Vector3(2, 0, 1) };
        doorPos = new Vector2[] { new Vector2(6, 1), new Vector2(6, 5) };
        guardPos = new Vector3[] { new Vector3(4, 4, 0) };
        //Add mirrors to game board
        mirrorPos.Add(new Vector2(2, 2)); //Down-right mirror = mirrors[3]
        mirrorPos.Add(new Vector2(2, 3)); //Up-right mirror = mirrors[1]
        CreateGameObject(2, 2, mirrors[3]);
        CreateGameObject(3, 2, mirrors[1]);
        AddToScreen(2, 2, floorTiles[0]);
        AddToScreen(3, 2, floorTiles[0]);
    }

    void LevelFourSetup() {
        playerPos = new Vector2(4, 3);
        selectorPos = new Vector2(5, 3);
        treasurePos = new List<Vector2>();
        treasurePos.Add(new Vector2(7, 7));
        wallPos = new Vector2[] { new Vector2(4, 2) };
        //Z coordinate used to determine which way laser comes out of emitter
        //0 = right, 1 = left, 2 = up, 3 = down
        emitterPos = new Vector3[] { new Vector3 (3, 2, 2), new Vector3 (4, 4, 0),
            new Vector3 (6, 3, 3),  new Vector3 (6, 0, 1),  new Vector3 (1, 0, 2),
			new Vector3 (1, 6, 0)};
        receiverPos = new Vector3[] { new Vector3 (3, 4, 3), new Vector3 (6, 4, 1),
            new Vector3 (6, 1, 2), new Vector3 (2, 0, 0), new Vector3 (1, 5, 3),
			new Vector3 (7, 6, 1), new Vector3 (0, 7, 3)};
        doorPos = new Vector2[] { };
        guardPos = new Vector3[] { new Vector3 (2, 3, 0), new Vector3 (7, 2, 0),
            new Vector3 (5, 5, 0), new Vector3 (4, 7, 0)};
    }

    //etc.

    void InitialiseList() {
        //gridPositions.Clear();
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                //gridPositions.Add(new Vector3(col, row, 0f));
            }
        }
    }

    public void SetupScene() {
        LevelSetup();
        CreateLaserBeams();
        //InitialiseList();
        //timer = 0.0f;
    }

    void InitializeGameBoard() {
        CreateGameObject(playerPos.y, playerPos.x, players[0]);
        //CreateGameObject (selectorPos.y, selectorPos.x, selectors[0]);
        CreateMultipleGameObjects(treasurePos, treasures[0]);
        CreateMultipleGameObjects(wallPos, walls[0]);
        CreateMultipleGameObjects(emitterPos, laserEmitters[0]);
        CreateMultipleGameObjects(receiverPos, laserRecievers[0]);
        CreateMultipleGameObjects(doorPos, doors[0]);
        CreateMultipleGameObjects(guardPos, guards[0]);
        CreateLaserBeams();
        CreateFloorTiles();
        CreateOnGuardTiles();
    }

    //Instantiate a game object at the given row/column
    void CreateGameObject(float row, float col, GameObject obj) {
        GameObject inst = Instantiate(obj, new Vector3(col, row, 0f), Quaternion.identity) as GameObject;
        inst.transform.SetParent(boardHolder);
        gameBoard[(int)row, (int)col] = obj;
        gameBoardRotations[(int)row, (int)col] = 0;
    }

    //Instantiate a game object at the given row/column with rotation
    void CreateGameObject(float row, float col, float rotationZ, GameObject obj) {
        GameObject inst = Instantiate(obj, new Vector3(col, row, 0f), Quaternion.Euler(0f, 0f, rotationZ)) as GameObject;
        inst.transform.SetParent(boardHolder);
        gameBoard[(int)row, (int)col] = obj;
        gameBoardRotations[(int)row, (int)col] = (int)rotationZ;
    }

    //Instantiate a bunch of game objects at once
    void CreateMultipleGameObjects(Vector2[] positions, GameObject obj) {
        for (int i = 0; i < positions.Length; i++) {
            CreateGameObject(positions[i].y, positions[i].x, obj);
        }
    }

    //Instantiate a bunch of game objects at once
    void CreateMultipleGameObjects(Vector3[] positions, GameObject obj) {
        //0 = right, 1 = left, 2 = up, 3 = down
        float rotation = 0f;
        for (int i = 0; i < positions.Length; i++) {
            if (positions[i].z == 1) rotation = 180f;
            else if (positions[i].z == 2) rotation = 90f;
            else if (positions[i].z == 3) rotation = -90f;
            else rotation = 0f;
            CreateGameObject(positions[i].y, positions[i].x, rotation, obj);
        }
    }

    //Instantiate a bunch of game objects at once
    void CreateMultipleGameObjects(List<Vector2> positions, GameObject obj) {
        for (int i = 0; i < positions.Count; i++) {
            CreateGameObject(positions[i].y, positions[i].x, obj);
        }
    }

    void CreateFloorTiles() {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                GameObject inst = Instantiate(floorTiles[0], new Vector3(col, row, 0f), Quaternion.identity) as GameObject;
                inst.transform.SetParent(boardHolder);
                if (gameBoard[row, col] == null) {
                    gameBoard[(int)row, (int)col] = floorTiles[0];
                    gameBoardRotations[(int)row, (int)col] = 0;
                }
            }
        }
    }

    bool SelectorMovementCommands(int rowMovement, int colMovement) {
        int row = (int)selectorPos.y;
        int col = (int)selectorPos.x;
        if (NextPosIsOnBoard(row, col, rowMovement, colMovement) && NextValidSelectorOnly(row, col, rowMovement, colMovement)) {
            selectorPos.y = row + rowMovement;
            selectorPos.x = col + colMovement;
            //CreateGameObject (row + rowMovement, col + colMovement, selectors [0]);
            //CreateGameObject (row, col, floorTiles [0]);
            return true;
        }
        return false;
    }

    bool PlayerMovementCommands(int rowMovement, int colMovement) {
        int row = (int)playerPos.y;
        int col = (int)playerPos.x;
        if (NextPosIsOnBoard(row, col, rowMovement, colMovement) && NextValid(row, col, rowMovement, colMovement)) {
            playerPos.y = row + rowMovement;
            playerPos.x = col + colMovement;
            //Player runs into laser
            if (gameBoard[(int)playerPos.y, (int)playerPos.x] == laserBeams[0] || gameBoard[(int)playerPos.y, (int)playerPos.x] == laserBeams[1]) {
                //CreateGameObject(row + rowMovement, col + colMovement, players[0]);
                //CreateGameObject(row, col, floorTiles[0]);
                Destroy(board);
                SetupScene();
                sounds[2].GetComponent<AudioSource>().Play();
                //CreateGameObject(row, col, floorTiles[0]);
            }
            //player gets treasure
            else if (gameBoard[(int)playerPos.y, (int)playerPos.x] == treasures[0]) {
                treasurePos.Remove(playerPos);
                if (treasurePos.Count == 0) {
                    sounds[0].GetComponent<AudioSource>().Play();
                    level++;
                    CreateGameObject(row + rowMovement, col + colMovement, players[0]);
                    CreateGameObject(row, col, floorTiles[0]);
                    Destroy(board);
                    SetupScene();
                } else {
                    CreateGameObject(row + rowMovement, col + colMovement, players[0]);
                    CreateGameObject(row, col, floorTiles[0]);
                }
            }
            //player gets caught by gaurd
            else if (gameBoard[(int)playerPos.y, (int)playerPos.x] == onGuardTiles[0]) {
                sounds[1].GetComponent<AudioSource>().Play();
                CreateGameObject(row + rowMovement, col + colMovement, players[0]);
                CreateGameObject(row, col, floorTiles[0]);
                Destroy(board);
                SetupScene();
                CreateGameObject(row, col, floorTiles[0]);
            } else {
                CreateGameObject(row + rowMovement, col + colMovement, players[0]);
                CreateGameObject(row, col, floorTiles[0]);
            }
            return true;
        }
        return false;
    }

    bool NextPosIsOnBoard(int row, int col, int rowMovement, int colMovement) {
        return (row + rowMovement) < rows && (row + rowMovement) >= 0 && (col + colMovement) < columns && (col + colMovement) >= 0;
    }

    bool NextValid(int row, int col, int rowMovement, int colMovement) {
        return gameBoard[row + rowMovement, col + colMovement] == laserBeams[0] ||
        gameBoard[row + rowMovement, col + colMovement] == laserBeams[1] ||
        gameBoard[row + rowMovement, col + colMovement] == floorTiles[0] ||
        gameBoard[row + rowMovement, col + colMovement] == treasures[0] ||
        gameBoard[row + rowMovement, col + colMovement] == onGuardTiles[0];
    }

    bool NextValidSelectorOnly(int row, int col, int rowMovement, int colMovement) {
        return NextValid(row, col, rowMovement, colMovement) ||
        gameBoard[row + rowMovement, col + colMovement] == mirrors[0] ||
        gameBoard[row + rowMovement, col + colMovement] == mirrors[1] ||
        gameBoard[row + rowMovement, col + colMovement] == mirrors[2] ||
        gameBoard[row + rowMovement, col + colMovement] == mirrors[3] ||
        gameBoard[row + rowMovement, col + colMovement] == lenses[0] ||
        gameBoard[row + rowMovement, col + colMovement] == lenses[1];
    }

    bool SelectorCloseEnoughToPlayer() {
        return (selectorPos - playerPos).magnitude < 2;
    }

    void PlaceMirror(int dir) {
        CreateGameObject(selectorPos.y, selectorPos.x, mirrors[dir - 1]);
        AddToScreen((int)selectorPos.y, (int)selectorPos.x, floorTiles[0]);
        mirrorPos.Add(new Vector3(selectorPos.y, selectorPos.x, dir));
        //selectorPos.y = 0;
        //selectorPos.x = 0;
        //CreateGameObject (selectorPos.y, selectorPos.x, selectors [0]);
    }

    void PlaceLens(int dir) {
        CreateGameObject(selectorPos.y, selectorPos.x, lenses[dir]);
        AddToScreen((int)selectorPos.y, (int)selectorPos.x, floorTiles[0]);
        lensPos.Add(new Vector3(selectorPos.y, selectorPos.x, dir));
        //selectorPos.y = 0;
        //selectorPos.x = 0;
        //CreateGameObject (selectorPos.y, selectorPos.x, selectors [0]);
    }

    void DeleteExistingLaserBeamsAndResetReceivers() {
        float rotation = 0;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                if (gameBoard[row, col] == laserBeams[0] || gameBoard[row, col] == laserBeams[1]) {
                    rotation = gameBoardRotations[row, col];
                    CreateGameObject(row, col, rotation, floorTiles[0]);
                }
                if (gameBoard[row, col] == laserRecievers[1]) {
                    rotation = gameBoardRotations[row, col];
                    CreateGameObject(row, col, rotation, laserRecievers[0]);
                }
            }
        }
    }

    void CreateLaserBeams() {
        DeleteExistingLaserBeamsAndResetReceivers();
        int oldSecurityLevel = securityLevel;
        securityLevel = receiverPos.Length;
        foreach (Vector3 pos in emitterPos) {
            Vector2 direction;
            if ((int)pos.z == 0) direction = new Vector2(1, 0);
            else if ((int)pos.z == 1) direction = new Vector2(-1, 0);
            else if ((int)pos.z == 2) direction = new Vector2(0, 1);
            else direction = new Vector2(0, -1);
            Vector2 laserPos = new Vector2(pos.x, pos.y);
            FollowLasers(laserPos, direction);
        }
        if (securityLevel > oldSecurityLevel) {
            sounds[4].GetComponent<AudioSource>().Play();
        }
    }

    void FollowLasers(Vector2 laserPos, Vector2 direction) {
        float rotation = 0;
        while (NextPosIsOnBoard((int)laserPos.y, (int)laserPos.x, (int)direction.y, (int)direction.x)) {
            laserPos += direction;
            GameObject obj = gameBoard[(int)laserPos.y, (int)laserPos.x];
            //If obj is floor tile, selector, treasure: keep going
            if (obj == players[0]) {
                //Game Over
                //CreateGameObject(laserPos.y, laserPos.x, laserBeams[Math.Abs((int)direction.y) % 2]);
				Destroy(board);
				SetupScene();
				sounds[2].GetComponent<AudioSource>().Play();
                break;
            }
            if (obj == laserEmitters[0] || obj == walls[0]) {
                rotation = gameBoardRotations[(int)laserPos.y, (int)laserPos.x];
            //    CreateGameObject(laserPos.y, laserPos.x, rotation, laserRecievers[1]);
                break;
            }
            if (obj == laserRecievers[0]) {
                securityLevel--;
                rotation = gameBoardRotations[(int)laserPos.y, (int)laserPos.x];
                CreateGameObject(laserPos.y, laserPos.x, rotation, laserRecievers[1]);
                break;
            }
            if (obj == doors[0]) {
                CreateGameObject(laserPos.y, laserPos.x, laserBeams[Math.Abs((int)direction.y) % 2]);
            }
            if (obj == mirrors[0]) {
                if (direction == new Vector2(1, 0)) direction = new Vector2(0, 1);
                else if (direction == new Vector2(0, -1)) direction = new Vector2(-1, 0);
                else break;
            }
            if (obj == mirrors[1]) {
                if (direction == new Vector2(-1, 0)) direction = new Vector2(0, 1);
                else if (direction == new Vector2(0, -1)) direction = new Vector2(1, 0);
                else break;
            }
            if (obj == mirrors[2]) {
                if (direction == new Vector2(1, 0)) direction = new Vector2(0, -1);
                else if (direction == new Vector2(0, 1)) direction = new Vector2(-1, 0);
                else break;
            }
            if (obj == mirrors[3]) {
                if (direction == new Vector2(-1, 0)) direction = new Vector2(0, -1);
                else if (direction == new Vector2(0, 1)) direction = new Vector2(1, 0);
                else break;
            }
            if (obj == lenses[0]) {
                //takes vertical laser and sends left and right
                if (direction.x == 0) {
                    FollowLasers(laserPos, new Vector2(-1, 0));
                    FollowLasers(laserPos, new Vector2(1, 0));
                }
                break;
            }
            if (obj == lenses[1]) {
                //takes horizontal laser and sends it up and down
                if (direction.y == 0) {
                    FollowLasers(laserPos, new Vector2(0, -1));
                    FollowLasers(laserPos, new Vector2(0, 1));
                }
                break;
            }
            if (obj == guards[0]) {
                StunGuard(laserPos);
                break;
            }
            //else continue, see if we should put laser on board
            if (obj == doors[0] || obj == floorTiles[0] || obj == players[0] || obj == onGuardTiles[0])
                CreateGameObject(laserPos.y, laserPos.x, laserBeams[Math.Abs((int)direction.y) % 2]);
        }
    }

    void StunGuard(Vector2 laserPos) {
        for (int i = 0; i < guardPos.Length; i++) {
            if (guardPos[i].x == laserPos.x && guardPos[i].y == laserPos.y) {
                guardPos[i].z = 1;
            }
        }
    }

    void DeleteExistingOnGuardTiles() {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                if (gameBoard[row, col] == onGuardTiles[0]) {
                    CreateGameObject(row, col, floorTiles[0]);
                }
            }
        }
    }

    void CreateOnGuardTiles() {
        DeleteExistingOnGuardTiles();
        foreach (Vector3 pos in guardPos) {
            if (pos.z == 0) {
                for (int i = -securityLevel; i <= securityLevel; i++) {
                    for (int j = -securityLevel; j <= securityLevel; j++) {
                        if (NextPosIsOnBoard((int)pos.y, (int)pos.x, i, j) && gameBoard[i + (int)pos.y, j + (int)pos.x] == floorTiles[0]) {
                            CreateGameObject(i + pos.y, j + pos.x, onGuardTiles[0]);
                        }
                    }
                }
            }
        }
    }

    bool RemovingMirror() {
        return Input.GetKeyDown(KeyCode.Space) && SelectorCloseEnoughToPlayer() && (
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == mirrors[0] ||
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == mirrors[1] ||
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == mirrors[2] ||
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == mirrors[3] ||
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == lenses[0] ||
            gameBoard[(int)selectorPos.y, (int)selectorPos.x] == lenses[1]);

    }

    // Use this for initialization
    void Start() {
		helpScreen.SetActive (false);
		helpInstructions.SetActive (true);
    }

    // Update is called once per frame
    void Update() {
        //timer += Time.deltaTime;
        //timeGui.text = "" + (int)timer;
        if (Input.GetKeyDown(KeyCode.P)) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    print(gameBoard[i, j].ToString());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && SelectorCloseEnoughToPlayer()) {
            PlaceMirror(1);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && SelectorCloseEnoughToPlayer()) {
            PlaceMirror(2);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && SelectorCloseEnoughToPlayer()) {
            PlaceMirror(3);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (Input.GetKeyDown(KeyCode.Alpha4) && SelectorCloseEnoughToPlayer()) {
            PlaceMirror(4);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (Input.GetKeyDown(KeyCode.Alpha5) && SelectorCloseEnoughToPlayer()) {
            PlaceLens(0);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (Input.GetKeyDown(KeyCode.Alpha6) && SelectorCloseEnoughToPlayer()) {
            PlaceLens(1);
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else if (RemovingMirror()) {
            CreateGameObject(selectorPos.y, selectorPos.x, floorTiles[0]);
            //selectorPos.x = 0;
            //selectorPos.y = 0;
            CreateLaserBeams();
            CreateOnGuardTiles();
        } else {
            if (Input.GetKeyDown(KeyCode.A)) {
                SelectorMovementCommands(0, -1);
                CreateLaserBeams();
                CreateOnGuardTiles();
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                SelectorMovementCommands(0, 1);
                CreateLaserBeams();
                CreateOnGuardTiles();
            }
            if (Input.GetKeyDown(KeyCode.W)) {
                SelectorMovementCommands(1, 0);
                CreateLaserBeams();
                CreateOnGuardTiles();
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                SelectorMovementCommands(-1, 0);
                CreateLaserBeams();
                CreateOnGuardTiles();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                CreateLaserBeams();
                CreateOnGuardTiles();
                PlayerMovementCommands(0, -1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                CreateLaserBeams();
                CreateOnGuardTiles();
                PlayerMovementCommands(0, 1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                CreateLaserBeams();
                CreateOnGuardTiles();
                PlayerMovementCommands(1, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                CreateLaserBeams();
                CreateOnGuardTiles();
                PlayerMovementCommands(-1, 0);
            }
			if (Input.GetKeyDown(KeyCode.H)) {
				if (!helpScreen.activeInHierarchy) {
					helpScreen.SetActive (true);
				} else {
					helpScreen.SetActive (false);
				}
			}
        }
        DeleteCurrentBoard();
        DrawGameBoardToScreen();
    }

    void DeleteCurrentBoard() {
        var children = new List<GameObject>();
        foreach (Transform child in boardHolder) children.Add(child.gameObject);
        foreach (var child in children) Destroy(child);
    }

    void DrawGameBoardToScreen() {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                if (col == selectorPos.x && row == selectorPos.y) {
                    AddToScreen(row, col, selectors[0]);
                } 
                CreateGameObject(row, col, gameBoardRotations[row, col], gameBoard[row, col]);
                AddToScreen(row, col, gameBoardRotations[row, col], floorTiles[0]);
            }
        }
    }

    void AddToScreen(int row, int col, GameObject obj) {
        GameObject inst = Instantiate(obj, new Vector3(col, row, 0f), Quaternion.identity) as GameObject;
        inst.transform.SetParent(boardHolder);
    }

    void AddToScreen(int row, int col, float rotation, GameObject obj) {
        GameObject inst = Instantiate(obj, new Vector3(col, row, 0f), Quaternion.Euler(0,0,rotation)) as GameObject;
        inst.transform.SetParent(boardHolder);
    }
}