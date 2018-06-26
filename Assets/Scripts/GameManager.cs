using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    #region Settings
    
    public MatchSettings matchSettings;
    #endregion

    public static GameManager instance;

    private void Awake()
    {
        if(instance !=null)
        {
            Debug.LogError("More than one Game Manager in scene");
        } else
        {
            instance = this;
        }
    }

    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player ";
    
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private static string masterNetID;

    public static void RegisterMaster(string _netID)
    {
        masterNetID = _netID;
    }

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    #endregion
    #region Enemy tracking
    private const string ENEMY_ID_PREFIX = "Enemy ";
    private static Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();
    public static Enemy GetEnemy(string _enemyID)
    {
        return enemies[_enemyID];
    }
    public static void RegisterEnemy(string _netID, Enemy _enemy)
    {
        string _enemyID = ENEMY_ID_PREFIX + _netID;
        enemies.Add(_enemyID, _enemy);
        _enemy.transform.name = _enemyID;
    }

    public static void UnRegisterEnemy(string _enemyID)
    {
        enemies.Remove(_enemyID);
    }
    #endregion
}
