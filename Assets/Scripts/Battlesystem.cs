using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class Battlesystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject firePrefab;

    public Transform playerPos;
    [SerializeField] GameObject playerSprite;
    public Transform enemyPos;

    attribute playerAttribute;
    attribute enemyAttribute;

    public Text battleText;

    public PlayerHUD playerHUD;
    public EnemyHUD enemyHUD;

    [SerializeField] GameObject enemyInfo;
    [SerializeField] GameObject acts;

    public GameObject box;

    Vector3 originalPos;
    private bool toldJoke;

    // Always updates HP and HUD of the Player and Enemy
    void Update()
    {
        playerHUD.setHP(playerAttribute.currentHP);
        playerHUD.setHUD(playerAttribute);

        enemyHUD.setHP(enemyAttribute.currentHP);
        enemyHUD.setHUD(enemyAttribute);
    }

    // Gets called when starting Scene
    void Start()
    {
        playerPrefab.GetComponent<Movement>().speed = 0;
        state = BattleState.START;
        StartCoroutine(setBattle());
        originalPos = new Vector3(playerAttribute.transform.position.x, playerAttribute.transform.position.y);
    }

    IEnumerator setBattle() {
        enemyInfo.SetActive(false);
        acts.SetActive(false);

        // Makes Player "invisible" for the start of battle
        playerPos = playerSprite.transform;
        playerSprite.SetActive(false);

        // Spawns Player and gets Information from script attribute
        GameObject playerSpawn = Instantiate(playerPrefab, playerPos);
        playerAttribute = playerSpawn.GetComponent<attribute>();

        // Spawns Enemy and gets Information from script attribute
        GameObject enemySpawn = Instantiate(enemyPrefab, enemyPos);
        enemyAttribute = enemySpawn.GetComponent<attribute>();

        battleText.text = "My- ";
        yield return new WaitForSeconds(1f);
        battleText.text = "No ";
        yield return new WaitForSeconds(1f);
        battleText.text = "OUR NAME IS " + enemyAttribute.Name;
        yield return new WaitForSeconds(2f);

        // Show Interfaces of Player
        playerHUD.setHUD(playerAttribute);

        // Change State
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // Gets called when Enemy Name is clicked after Attack Button
    IEnumerator PlayerAttack() {

        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage);

        enemyInfo.SetActive(false);
        battleText.enabled = true;
        battleText.text = enemyAttribute.Name + " has been hit";

        yield return new WaitForSeconds(2f);

        // Checks if Enemy is alive
        if(isDead) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerActJoke() {

        acts.SetActive(false);

        string[] joke = {"* It found the joke hilarious", "* You have already told that joke"};
        string[] reactions = {"\"HAHAHAHAAHAHA\" \n\"I hate to admit it but that was funny!\"", "\"Dude you have already told me the joke\""};

        battleText.text = "* You told the enemy a joke";
        battleText.enabled = true;
        yield return new WaitForSeconds(2f);

        if (toldJoke) {
            battleText.text = joke[1];
            yield return new WaitForSeconds(2f);
            battleText.text = reactions[1];

        } else {

            battleText.text = joke[0];
            yield return new WaitForSeconds(2f);
            battleText.text = reactions[0];
            yield return new WaitForSeconds(3f);
            enemyAttribute.friendliness += 25;

            if (playerAttribute.takenDamage > 1) {

                playerAttribute.takenDamage = 1;
                battleText.text = "Damage has been reduced";
                yield return new WaitForSeconds(2f);
            }
            toldJoke = true;
        }

        if (enemyAttribute.friendliness == 100) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerActThreat() {

        acts.SetActive(false);

        battleText.text = "* You threatened the enemy";
        battleText.enabled = true;
        enemyAttribute.friendliness += 10;

        yield return new WaitForSeconds(2f);

        battleText.text = "\"Tch, go home kid\"";
        yield return new WaitForSeconds(2f);

        playerAttribute.takenDamage += 1;
        battleText.text = "* The enemys Damage has increased";
        yield return new WaitForSeconds(2f);

        if (enemyAttribute.friendliness == 100) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerFire() {

        // Creates Fire "Animation"
        GameObject fire = Instantiate(firePrefab, enemyPos);
        Destroy(fire, 2);

        // Makes Fire Damage
        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage * playerAttribute.fire_multiplier);

        enemyInfo.SetActive(false);

        battleText.enabled = true;
        battleText.text = "It was super effective";

        yield return new WaitForSeconds(2f);

        // Checks if Enemy is alive
        if(isDead) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn() {

        battleText.text = enemyAttribute.Name + " is going to attack!";
        yield return new WaitForSeconds(2f);

        playerPos = playerSprite.transform;
        playerSprite.SetActive(true);

        // Disables EnemyHUD during Enemys turn
        enemyInfo.SetActive(false);

        // Sets the scale of the GameObject "box"
        box.transform.localScale = new Vector3(8,8,0);

        playerAttribute.transform.position = originalPos;

        // Activates the movement script to doge Enemy hits
        playerAttribute.GetComponent<Movement>().speed = 5;

        battleText.enabled = false;

        yield return new WaitForSeconds(2f);

        enemyAttribute.GetComponent<Attack>().allAttack();

        yield return new WaitForSeconds(6f);

        // Checks if Player is alive
        if(playerAttribute.currentHP <= 0) {

            state = BattleState.LOST;
            EndBattle();

        } else {

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle() {

        if (state == BattleState.WON) {

            SceneManager.LoadScene(sceneName:"WinScreen");

        } else if (state == BattleState.LOST) {

            SceneManager.LoadScene(sceneName:"Deathscreen");
        }
    }

    void PlayerTurn() {
        // Change size of box
        box.transform.localScale = new Vector3(32,8,0);

        // Makes Player "invisible" for this turn
        playerPos = playerSprite.transform;
        playerSprite.SetActive(false);

        battleText.enabled = true;
        battleText.text = "Choose your move";

        // Disable Movement for PlayerTurn
        playerAttribute.GetComponent<Movement>().speed = 0;
    }

    public void OnAttackButton() {

        acts.SetActive(false);

        if (state != BattleState.PLAYERTURN) {
            return;
        }
        else {
            battleText.enabled = false;
            enemyInfo.SetActive(true);
        }
    }

    public void OnAttackEnemyButton() {

        if (state != BattleState.PLAYERTURN) {
            return;

        } else {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnFireButton() {

        if (state != BattleState.PLAYERTURN) {
            return;

        } else {
            StartCoroutine(PlayerFire());
        }
    }

    public void OnActButton() {

        if (state != BattleState.PLAYERTURN) {
            return;

        } else {
            battleText.enabled = false;
            enemyInfo.SetActive(false);

            acts.SetActive(true);
        }
    }

    public void OnActButtonJoke() {

        if (state != BattleState.PLAYERTURN) {
            return;

        } else {
            StartCoroutine(PlayerActJoke());
        }
    }

    public void OnActButtonThreat() {

        if (state != BattleState.PLAYERTURN) {
            return;

        } else {
            StartCoroutine(PlayerActThreat());
        }
    }
}
