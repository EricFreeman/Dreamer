using UnityEngine;

public class Dust : MonoBehaviour
{
    public float Size;
    public float FadeSpeed;

    public float MaxSizeDifference;
    public float MaxFadeSpeedDifference;

    private float _currentSizeDifference;
    private float _currentFadeSpeedDifference;

    private float _currentAlpha = 1;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _currentSizeDifference = Random.Range(-MaxSizeDifference, MaxSizeDifference);
        _currentFadeSpeedDifference = Random.Range(-MaxFadeSpeedDifference, MaxFadeSpeedDifference);

        var scale = Size + _currentSizeDifference;
        transform.localScale = new Vector3(scale, scale, scale);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position -= new Vector3(0, .2f, 0);
    }

    void Update()
    {
        var fadeSpeed = FadeSpeed + _currentFadeSpeedDifference;
        _currentAlpha -= fadeSpeed;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _currentAlpha);
    }
}