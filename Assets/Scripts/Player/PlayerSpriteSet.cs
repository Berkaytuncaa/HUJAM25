using UnityEngine;

namespace Player
{
    public class PlayerSpriteSet : MonoBehaviour
    {
        [SerializeField] private Sprite blackBg;
        [SerializeField] private Sprite blackB;
        [SerializeField] private Sprite blackG;
        [SerializeField] private Sprite black;
        [SerializeField] private Sprite whiteBg;
        [SerializeField] private Sprite whiteB;
        [SerializeField] private Sprite whiteG;
        [SerializeField] private Sprite white;
        private void Awake()
        {
            //B means cap and g means gloves
            if (Singleton.Instance.isBlack && !Singleton.Instance.isGlove && !Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = black;
            }
            else if (Singleton.Instance.isBlack && Singleton.Instance.isGlove && !Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = blackG;
            }
            else if (Singleton.Instance.isBlack && !Singleton.Instance.isGlove && Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = blackB;
            }
            else if (Singleton.Instance.isBlack && Singleton.Instance.isGlove && Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = blackBg;
            }
            else if (Singleton.Instance.isWhite && !Singleton.Instance.isGlove && !Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = white;
            }
            else if (Singleton.Instance.isWhite && Singleton.Instance.isGlove && !Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = whiteG;
            }
            else if (Singleton.Instance.isWhite && !Singleton.Instance.isGlove && Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = whiteB;
            }
            else if (Singleton.Instance.isWhite && Singleton.Instance.isGlove && Singleton.Instance.isCap)
            {
                GetComponent<SpriteRenderer>().sprite = whiteBg;
            }
        }

    }
}
