using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class ScrambleMinigameMgr : MonoBehaviour
{
    //public OpheliaStats ostats;
    static List<string> sixLetterWords = new List<string>
    {
        "animal", "bottle", "candle", "desert", "empire", "flight", "garden", "hammer", "island", "jungle",
        "kitten", "ladder", "mirror", "napkin", "orange", "pencil", "quartz", "rocket", "silver", "travel",
        "united", "victor", "window", "xenial", "yellow", "zenith", "absent", "bounce", "canvas", "damage",
        "effort", "fabric", "gentle", "honest", "injury", "jacket", "keeper", "legend", "magnet", "nature",
        "object", "packet", "quiver", "rescue", "salute", "tablet", "unique", "vacuum", "wander", "xylems",
        "yawner", "zigzag", "anchor", "bubble", "circle", "daring", "editor", "famous", "glider", "humble",
        "impact", "jersey", "karate", "laptop", "market", "nation", "office", "plasma", "quaint", "repeat",
        "shadow", "timber", "update", "vacant", "wealth", "yellow", "zodiac", "artist", "banana", "charge",
        "donkey", "estate", "fiddle", "gather", "hunter", "injure", "jumper", "kitten", "lawyer", "moment",
        "notice", "oxygen", "pardon", "quench", "ripple", "school", "thread", "urgent", "valley", "warden",
        "xenons", "yonder", "zealot", "acorns", "blazer", "cradle", "device", "engine", "flower", "gospel",
        "hermit", "infant", "jungle", "kitbag", "legend", "memory", "noodle", "oracle", "pepper", "quartz",
        "rocket", "saddle", "turtle", "useful", "virtue", "waffle", "xenial", "yeller", "zapper", "accent",
        "barrel", "casual", "defend", "efface", "flight", "gentle", "humour", "injury", "jigsaw", "knight",
        "listen", "modern", "normal", "outlaw", "planet", "quiche", "retail", "stable", "thanks", "unfold",
        "violet", "wander", "xenons", "yellow", "zigzag", "avenue", "breeze", "carpet", "danger", "effort",
        "forest", "growth", "hunter", "ignite", "jerkin", "kitten", "lemony", "mortal", "napkin", "output",
        "plight", "quartz", "refine", "signal", "travel", "uplift", "vendor", "woolen", "xenial", "yonder",
        "zephyr", "acting", "butter", "closet", "decent", "entire", "fabric", "glitch", "herald", "impact",
        "jumper", "kettle", "lawful", "mantle", "narrow", "object", "pastel", "quaint", "reside", "shrink",
        "tunnel", "united", "viewer", "wonder", "xerxes", "yogurt", "zinger", "admire", "beauty", "clover",
        "dollar", "exotic", "friend", "gather", "honest", "indigo", "jester", "keeper", "lovely", "manual",
        "notice", "office", "planet", "quiver", "reward", "system", "trophy", "urgent", "vacant", "waiver",
        "xeroma", "yellow", "zodiac"
    };

    static List<string> fourLetterWords = new List<string>
    {
        "bake", "blue", "brag", "chat", "cool", "crab", "dark", "dawn", "drip", "drop",
        "easy", "even", "fast", "fire", "flip", "frog", "game", "gaze", "glow", "grab",
        "grow", "hate", "heal", "heat", "hope", "joke", "jump", "keep", "kick", "kind",
        "lamp", "lava", "leaf", "lift", "lion", "love", "luck", "mask", "mate", "maze",
        "meat", "mild", "mint", "move", "nest", "nice", "open", "pace", "park", "peel",
        "pink", "play", "pray", "push", "quiz", "rage", "rain", "read", "rest", "ring",
        "roar", "rock", "roll", "roof", "rush", "safe", "sand", "save", "seal", "seed",
        "shut", "sing", "skip", "slap", "slip", "snap", "snow", "soft", "song", "spin",
        "stay", "stem", "stop", "swap", "swim", "tame", "tape", "team", "time", "twin",
        "view", "walk", "warm", "wave", "wild", "wish", "wolf", "wood", "wrap", "zoom",
        "ache", "ally", "arch", "army", "aunt", "bark", "barn", "beam", "belt", "bend",
        "best", "bite", "blow", "bold", "bowl", "buck", "bump", "burn", "calm", "camp",
        "cane", "card", "care", "cash", "cell", "chip", "clay", "clip", "club", "coal",
        "coat", "cold", "comb", "cook", "cope", "cord", "cost", "crew", "crop", "cute",
        "dare", "dash", "deal", "debt", "deep", "deny", "desk", "diet", "dish", "dive",
        "door", "drew", "duck", "dust", "edge", "envy", "exit", "face", "fail", "fair",
        "fake", "fame", "fear", "feed", "feel", "file", "film", "fine", "fish", "fizz",
        "flat", "flow", "fold", "foot", "fort", "gain", "gift", "girl", "give", "glad",
        "gold", "golf", "grip", "hail", "hair", "hall", "hand", "hard", "harm", "hawk",
        "head", "hill", "hint", "hold", "hole", "holy", "hook", "horn", "host", "hunt"
    };

    static List<string> fiveLetterWords = new List<string>
    {
        "apple", "baker", "beach", "blink", "blown", "bored", "brave", "break", "bride", "bring",
        "broom", "build", "cabin", "candy", "catch", "chess", "child", "chill", "clean", "climb",
        "clock", "cloud", "color", "couch", "count", "cover", "craft", "crash", "cream", "creek",
        "dance", "death", "delay", "demon", "dirty", "ditch", "dizzy", "drain", "drink", "drive",
        "eagle", "early", "earth", "eject", "elite", "enter", "equal", "error", "event", "every",
        "faith", "false", "favor", "feast", "fence", "field", "fight", "final", "flame", "flash",
        "fleet", "floor", "focus", "force", "frame", "fresh", "frost", "fruit", "funny", "giant",
        "glide", "globe", "grace", "grade", "grain", "grant", "grasp", "green", "grind", "grove",
        "guard", "guest", "habit", "happy", "harsh", "heart", "heavy", "honey", "honor", "horse",
        "hotel", "house", "human", "hurry", "ideal", "image", "index", "inner", "input", "jelly",
        "joint", "judge", "juice", "kneel", "knife", "label", "laugh", "layer", "learn", "leave",
        "lemon", "level", "light", "limit", "liver", "lobby", "lucky", "lunar", "lunch", "magic",
        "maker", "march", "match", "metal", "might", "miner", "model", "money", "month", "moral",
        "motor", "mouth", "movie", "music", "naked", "nasty", "nerve", "never", "noble", "noise",
        "north", "notch", "novel", "ocean", "offer", "often", "onion", "orbit", "order", "organ",
        "other", "outer", "paint", "panel", "party", "patch", "peace", "petal", "phase", "phone",
        "photo", "piano", "piece", "pilot", "plant", "plate", "point", "power", "press", "price",
        "pride", "prime", "print", "prize", "proof", "proud", "queen", "quick", "quiet", "radio",
        "raise", "range", "reach", "ready", "realm", "rebel", "relax", "reply", "ridge", "river",
        "robot", "rough", "round", "royal", "rugby", "rural", "scale", "scene", "score", "scout"
    };

    public List<string> words;
    string word;
    public TextMeshProUGUI scrambledWordText;
    public TextMeshProUGUI invalidWordMessageText;
    public TextMeshProUGUI guessesText;
    public TextMeshProUGUI scoreText;
    public TMP_InputField targetInputField;
    float timer = 100000;
    public float maxTime;
    public Slider timerSlider;
    public int score = 0;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDifficulty(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < maxTime)
        {
            targetInputField.interactable = true;
            menu.SetActive(false);
            timerSlider.value = timer/maxTime;

            if (!targetInputField.isFocused)
            {
                EventSystem.current.SetSelectedGameObject(targetInputField.gameObject, null);
                targetInputField.OnPointerClick(new PointerEventData(EventSystem.current)); // Open the keyboard cursor
            }

        }
        else
        {
            targetInputField.interactable = false;
            menu.SetActive(true);
        }
    }

    public void UpdateDifficulty(int setting)
    {
        Debug.Log(setting);

        if (setting == 0)
        {
            Debug.Log("in here");
            words = fourLetterWords;
        }
        else if (setting == 1)
            words = fiveLetterWords;
        else if (setting == 2)
            words = sixLetterWords;
    }

    void GetNewWord()
    {
        int index = Random.Range(0, words.Count);
        word = words[index];
        string scrambledWord = Scramble(word);
        Debug.Log(word);
        scrambledWordText.text = scrambledWord.ToUpper();
    }

    public static string Scramble(string word)
    {
        char[] chars = word.ToCharArray();

        // Fisher-Yates shuffle
        for (int i = chars.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // UnityEngine.Random
            (chars[i], chars[j]) = (chars[j], chars[i]); // Swap using tuple
        }

        return new string(chars);
    }

    public void ProcessInput(string input)
    {
        Debug.Log("RAN");
        if (IsValidString(input))
        {
            if (EvaluateGuess(input))
            {
                ResetBoard();
                score++;
                scoreText.text = "Score: " + score;
            }
        }
        else
        {
            StopCoroutine(DisplayAndFade(""));
            StartCoroutine(DisplayAndFade("INVALID GUESS"));
        }
    }

    public bool IsValidString(string input)
    {
        bool validChars = input.All(char.IsLetter);
        bool validLength = input.Length == word.Length;

        targetInputField.text = "";

        return validChars && validLength;
    }

    public bool EvaluateGuess(string input)
    {
        string result = "";

        string inputCaps = input.ToUpper();
        string targetCaps = word.ToUpper();

        for (int i = 0; i < inputCaps.Length; i++)
        {
            char inputChar = inputCaps[i];
            char targetChar = targetCaps[i];

            if (inputChar == targetChar)
            {
                result += $"<color=green>{inputChar}</color>";
            }
            else
            {
                result += $"<color=red>{inputChar}</color>";
            }
        }

        guessesText.text += result + "\n";

        return inputCaps == targetCaps;
    }

    public void StartGame()
    {
        timer = 0;
        score = 0;
        guessesText.text = "";
        scoreText.text = "Score: " + score;
        GetNewWord();
        OpheliaStats.inst.ogDamage += 1;
    }

    public void ResetBoard()
    {
        StopCoroutine(DisplayAndFade(""));
        StartCoroutine(DisplayAndFade("CORRECT"));
        guessesText.text = "";
        GetNewWord();
    }

    private IEnumerator DisplayAndFade(string message)
    {
        // Show text
        invalidWordMessageText.text = message;
        invalidWordMessageText.alpha = 1f;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Fade out
        float elapsed = 0f;
        while (elapsed < 1)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / 1);
            invalidWordMessageText.alpha = newAlpha;
            yield return null;
        }

        invalidWordMessageText.alpha = 0f;
    }
}
