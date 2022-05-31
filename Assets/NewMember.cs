using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewMember : MonoBehaviour
{

public GameObject panel;
public GameObject SceneText;

public Transform enemyPos;
attribute enemyAttribute;

public GameObject enemyPrefab;

    void Start()
    {
        GameObject enemySpawn = Instantiate(enemyPrefab, enemyPos);
        enemyAttribute = enemySpawn.GetComponent<attribute>();

        Text sentence = SceneText.GetComponent<Text>();
        sentence.text = "You have recruited a new Member!";

        StartCoroutine(transition());
    }

    IEnumerator transition() {
        yield return new WaitForSeconds(3f);
        enemyAttribute.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        enemyAttribute.GetComponentInChildren<Animator>().enabled = true;

        Text sentence = SceneText.GetComponent<Text>();
        sentence.text = enemyAttribute.Name + " has joined your team!";

        panel.GetComponent<Image>().color = new Color(211f, 211f, 211f, 1f);
        SceneText.GetComponent<Text>().color = new Color(0f, 0f, 0f, 1f);
    }
}
