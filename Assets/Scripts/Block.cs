using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private int _hitsRemaining = 100;
    private TextMeshPro _pointText;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _pointText = GetComponentInChildren<TextMeshPro>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisualState();
    }

    private void Update()
    {
        if (transform.position.y < -3f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateVisualState()
    {
        _pointText.SetText(_hitsRemaining.ToString());
        SetBlockSprite();
        // _spriteRenderer.color = Color.Lerp (Color.White, Color.Red, _hitsRemaining / 10f);
    }

    private void SetBlockSprite()
    {
        if (_hitsRemaining >= 100)
            _spriteRenderer.sprite = _sprites[4];
        else if (_hitsRemaining >= 75 && _hitsRemaining < 100)
            _spriteRenderer.sprite = _sprites[3];
        else if (_hitsRemaining >= 50 && _hitsRemaining < 75)
            _spriteRenderer.sprite = _sprites[2];
        else if (_hitsRemaining >= 25 && _hitsRemaining < 50)
            _spriteRenderer.sprite = _sprites[1];
        else
            _spriteRenderer.sprite = _sprites[0];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hitsRemaining -= 1;
        if (_hitsRemaining > 0)
            UpdateVisualState();
        else
            Destroy(gameObject);
    }
    
    public void SetHits(int hits)
    {
        _hitsRemaining = hits;
        UpdateVisualState();
    }
}
