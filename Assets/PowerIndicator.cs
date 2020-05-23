using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerIndicator : MonoBehaviour
{
    List<Vector3> iconPositions;
    Vector3 smallScale, bigScale;
    public float SwitchTime;

    List<Image> icons;

    [System.Serializable]
    public class PowerIcon
    {
        public AbilityType type;
        public Sprite sprite;
    }

    public List<PowerIcon> IconSprites;
    Dictionary<AbilityType, Sprite> sprites = new Dictionary<AbilityType, Sprite>();
    Player player;
    Transform indicator;

    int currentAbility;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var icon in IconSprites)
        {
            sprites[icon.type] = icon.sprite;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentAbility = player.CurrentAbility;
        indicator = transform.Find("PowerIndicator");
        icons = indicator.GetChildren().Select(c => c.GetComponent<Image>()).ToList();

        iconPositions = icons.Select(icon => icon.transform.localPosition).ToList();

        smallScale = icons[1].transform.localScale;
        bigScale = icons[2].transform.localScale;
    }

    AbilityType circGet(int i)
    {
        while (i < 0)
        {
            i += player.Abilities.Count;
        }
        return player.Abilities[i % player.Abilities.Count].type;
    }

    int circIndex(int i)
    {
        while (i < 0)
        {
            i += 5;
        }
        return i % 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Abilities[0].type != AbilityType.Empty)
        {
            if (Input.GetButton("Power Wheel"))
            {
                indicator.gameObject.SetActive(false);
            }
            else
            {
                indicator.gameObject.SetActive(true);
                if (player.SwitchStatus > 0)
                {
                    currentAbility++;
                    for (int i = 0; i < 5; i++)
                    {
                        int newindex = circIndex(i - 1);
                        StartCoroutine(TweenTo(
                            icons[i],
                            iconPositions[newindex],
                            newindex == 2 ? bigScale : smallScale,
                            newindex == 0 || newindex == 4 ? 0 : 1
                            ));
                    }
                    var first = icons[0];
                    icons.RemoveAt(0);
                    icons.Add(first);
                }
                else if (player.SwitchStatus < 0)
                {
                    currentAbility--;
                    for (int i = 0; i < 5; i++)
                    {
                        int newindex = circIndex(i + 1);
                        StartCoroutine(TweenTo(
                            icons[i],
                            iconPositions[newindex],
                            newindex == 2 ? bigScale : smallScale,
                            newindex == 0 || newindex == 4 ? 0 : 1
                            ));
                    }
                    var last = icons[4];
                    icons.RemoveAt(4);
                    icons.Insert(0, last);
                }
                for (int i = 0; i < 5; i++)
                {
                    icons[i].sprite = sprites[circGet(player.CurrentAbility + i - 2)];
                }
            }
        }
        else
            indicator.gameObject.SetActive(false);
    }

    IEnumerator TweenTo(Image image, Vector3 pos, Vector3 scale, float alpha)
    {
        Transform imgTransform = image.transform;
        Color color = image.color;
        Vector3 prevPos = imgTransform.localPosition;
        Vector3 prevScale = imgTransform.localScale;
        Vector3 move = pos - prevPos;
        Vector3 scaleV = scale - prevScale;
        float prevAlpha = color.a;
        float alphaChange = alpha - prevAlpha;
        float t = 0;
        while (t < SwitchTime)
        {
            float val = EasingFunction.EaseOutCirc(0, 1, t / SwitchTime);
            imgTransform.localPosition = prevPos + val * move;
            imgTransform.localScale = prevScale + scaleV * val;
            color.a = prevAlpha + val * alphaChange;
            image.color = color;
            yield return 0;
            t += Time.deltaTime;
        }
        imgTransform.localPosition = pos;
        imgTransform.localScale = scale;
        color.a = alpha;
        image.color = color;
        player.Sounds.PlayOnce("PowerIndicator");
    }
}
