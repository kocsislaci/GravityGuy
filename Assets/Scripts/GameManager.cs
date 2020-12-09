using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = -1;
    public GameObject playerPrefab;
    public Button buttonPrefab;
    public GameObject scoreText;
    public GameObject SPFinalScoreText;
    public GameObject SPGameOverCanvas;
    public GameObject MPFinalScoreText;
    public GameObject MPGameOverCanvas;
    public Camera camera;

    private GameObject redPlayer;
    private Button redPlayerButton;
    private GameObject greenPlayer;
    private Button greenPlayerButton;
    private GameObject bluePlayer;
    private Button bluePlayerButton;
    private GameObject yellowPlayer;
    private Button yellowPlayerButton;
    private Vector3 redStartPos = new Vector3(-8.0f, -4.0f, -1.05f);
    private Vector3 greenStartPos = new Vector3(-8.0f, -1.0f, -1.05f);
    private Vector3 blueStartPos = new Vector3(-8.0f, 1.0f, -1.05f);
    private Vector3 yellowStartPos = new Vector3(-8.0f, 4.0f, -1.05f);
    private List<GameObject> losingPlayers = new List<GameObject>();


    void Start()
    {
        GetComponent<AudioSource>().Play();
        RestartGame();
    }
    void Update()
    {
        if (!ApplicationModel.multiplayer && redPlayer != null)
        {
            if (!scoreText.activeSelf && score > 0)
                scoreText.SetActive(true);
            score = (int)redPlayer.transform.position.x;
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        }
        else
        {
            if (scoreText.activeSelf)
                scoreText.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!ApplicationModel.multiplayer)
        {
            if (col.gameObject.tag == "Player")
            {
                GameOver(this.score);
            }
        }
        else
        {
            if (col.gameObject.tag == "Player")
            {
                losingPlayers.Add(col.gameObject);
                string color = col.gameObject.GetComponent<PlayerController>().color;
                Destroy(col.gameObject);
                switch (color)
                {
                    case "Red":
                        Destroy(redPlayerButton.gameObject);
                        break;
                    case "Green":
                        Destroy(greenPlayerButton.gameObject);
                        break;
                    case "Blue":
                        Destroy(bluePlayerButton.gameObject);
                        break;
                    case "Yellow":
                        Destroy(yellowPlayerButton.gameObject);
                        break;
                    default:
                        break;
                }
                if (losingPlayers.Count >= (ApplicationModel.chosenColors.Count - 1))
                {
                    GameOver(this.score);
                }
            }
        }
    }

    private void initSinglePlayer()
    {
        redPlayer = Instantiate(playerPrefab);
        //redPlayer.GetComponent<PlayerController>().color = "Red";
        redPlayer.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f);
        redPlayer.transform.position = redStartPos;
        redPlayer.GetComponent<PlayerController>().flipAtStart = true;
        redPlayer.GetComponent<PlayerController>().Restart("Red");

        redPlayerButton = Instantiate(buttonPrefab);
        redPlayerButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        redPlayerButton.transform.localPosition = new Vector3(694.0f, -321.0f, 0.0f);
        redPlayerButton.GetComponent<Button>().onClick.AddListener(redPlayer.GetComponent<PlayerController>().ToggleDirection);

    }
    private void initMultiplayer()
    {
        foreach (string color in ApplicationModel.chosenColors)
        {
            switch (color)
            {
                case "Red":
                    redPlayer = Instantiate(playerPrefab);
                    //redPlayer.GetComponent<PlayerController>().color = "Red";
                    redPlayer.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f);
                    redPlayer.transform.position = redStartPos;
                    redPlayer.GetComponent<PlayerController>().flipAtStart = true;
                    redPlayer.GetComponent<PlayerController>().Restart("Red");

                    redPlayerButton = Instantiate(buttonPrefab);
                    redPlayerButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
                    redPlayerButton.transform.localPosition = new Vector3(694.0f, -321.0f, 0.0f);
                    redPlayerButton.GetComponent<Image>().color = new Color(255.0f, 0.0f, 0.0f, 10.0f);
                    redPlayerButton.GetComponent<Button>().onClick.AddListener(redPlayer.GetComponent<PlayerController>().ToggleDirection);
                    break;
                case "Green":
                    greenPlayer = Instantiate(playerPrefab);
                    //greenPlayer.GetComponent<PlayerController>().color = "Green";
                    greenPlayer.GetComponent<SpriteRenderer>().color = new Color(0.0f, 255.0f, 0.0f);
                    greenPlayer.transform.position = greenStartPos;
                    greenPlayer.GetComponent<PlayerController>().flipAtStart = false;
                    greenPlayer.GetComponent<PlayerController>().Restart("Green");

                    greenPlayerButton = Instantiate(buttonPrefab);
                    greenPlayerButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
                    greenPlayerButton.transform.localPosition = new Vector3(694.0f, 321.0f, 0.0f);
                    greenPlayerButton.GetComponent<Image>().color = new Color(0.0f, 255.0f, 0.0f, 10.0f);
                    greenPlayerButton.GetComponent<Button>().onClick.AddListener(greenPlayer.GetComponent<PlayerController>().ToggleDirection);
                    break;
                case "Blue":
                    bluePlayer = Instantiate(playerPrefab);
                    //bluePlayer.GetComponent<PlayerController>().color = "Blue";
                    bluePlayer.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 255.0f);
                    bluePlayer.transform.position = blueStartPos;
                    bluePlayer.GetComponent<PlayerController>().flipAtStart = true;
                    bluePlayer.GetComponent<PlayerController>().Restart("Blue");

                    bluePlayerButton = Instantiate(buttonPrefab);
                    bluePlayerButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
                    bluePlayerButton.transform.localPosition = new Vector3(-694.0f, 321.0f, 0.0f);
                    bluePlayerButton.GetComponent<Image>().color = new Color(0.0f, 0.0f, 255.0f, 10.0f);
                    bluePlayerButton.GetComponent<Button>().onClick.AddListener(bluePlayer.GetComponent<PlayerController>().ToggleDirection);
                    break;
                case "Yellow":
                    yellowPlayer = Instantiate(playerPrefab);
                    //yellowPlayer.GetComponent<PlayerController>().color = "Yellow";
                    yellowPlayer.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 0.0f);
                    yellowPlayer.transform.position = yellowStartPos;
                    yellowPlayer.GetComponent<PlayerController>().flipAtStart = false;
                    yellowPlayer.GetComponent<PlayerController>().Restart("Yellow");

                    yellowPlayerButton = Instantiate(buttonPrefab);
                    yellowPlayerButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
                    yellowPlayerButton.transform.localPosition = new Vector3(-694.0f, -321.0f, 0.0f);
                    yellowPlayerButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 10.0f);
                    yellowPlayerButton.GetComponent<Button>().onClick.AddListener(yellowPlayer.GetComponent<PlayerController>().ToggleDirection);
                    break;
                default:
                    break;
            }
            
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
    public void ExitGame()
    {
        ApplicationModel.InitApplicationModel();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    } 

    public void RestartGame()
    {
        scoreText.SetActive(false);
        losingPlayers= new List<GameObject>();
        if (!ApplicationModel.multiplayer)
        {
            GetComponent<LevelGenerator>().RestartGame();
            camera.GetComponent<CameraController>().Restart();
            initSinglePlayer();

            score = -1;
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        }
        else
        {
            GetComponent<LevelGenerator>().RestartGame();
            camera.GetComponent<CameraController>().Restart();
            initMultiplayer();
        }
        Time.timeScale = 1.0f;
        //TODO start the game, countdown
    }
    public void GameOver(int score)
    {
        ApplicationModel.lastGameScore = score;
        Time.timeScale = 0.0f;
        GameObject.Find("PauseButton").SetActive(false);
        if (!ApplicationModel.multiplayer)
        {
            Destroy(redPlayer);
            Destroy(redPlayerButton.gameObject);
            if (redPlayerButton == null)
                Debug.Log("redPlayerButton deleted");
            SPGameOverCanvas.SetActive(true);
            SPFinalScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final score: " + score.ToString();
        }
        else
        {
            foreach (string color in ApplicationModel.chosenColors)
            {
                foreach (GameObject player in losingPlayers)
                {
                    if (player.GetComponent<PlayerController>().color == color)
                    {

                    }
                    else
                    {
                        switch (color)
                        {
                            case "Red":
                                Destroy(redPlayer);
                                Destroy(redPlayerButton.gameObject);
                                break;
                            case "Green":
                                Destroy(greenPlayer);
                                Destroy(greenPlayerButton.gameObject);
                                break;
                            case "Blue":
                                Destroy(bluePlayer);
                                Destroy(bluePlayerButton.gameObject);
                                break;
                            case "Yellow":
                                Destroy(yellowPlayer);
                                Destroy(yellowPlayerButton.gameObject);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            MPGameOverCanvas.SetActive(true);
            //MPFinalScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Winner: " + color;
        }
    } //TODO multi gameover behaviour

    public void setVolume(float volume)
    {
        ApplicationModel.volume = volume;
        if (ApplicationModel.audioMixer != null)
            ApplicationModel.audioMixer.SetFloat("volume", volume);
        else
            Debug.Log("Audiomixer volume didnt set");
    }
    public void toggleDebugMode()
    {
        ApplicationModel.toggleDebugMode();
    }
}
