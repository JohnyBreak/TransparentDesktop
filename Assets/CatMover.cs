using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CatMover : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _xSpeed = 100;
    [SerializeField] private float _ySpeed = 6;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private RectTransform _cat;
    [SerializeField] private SpriteChanger _spriteCharacter;

    private float _xMin = 0;
    private float _xMax;
    private float _yMin = 0;
    private float _halfCatWidth;

    private bool _isDraging;

    private void Start()
    {
        _halfCatWidth = _cat.rect.width / 2;

        _xMax = _canvas.rect.width;

        Rotate();
    }

    void Update()
    {

        if (_isDraging)
        {
            return;
        }

        if (_xSpeed < 0 && transform.position.x < _xMin + _halfCatWidth)
        {
            SnapToBorder();
            Rotate();
        }

        if (_xSpeed > 0 && transform.position.x + _halfCatWidth > _xMax)
        {
            SnapToBorder();
            Rotate();
        }

        float gravity = _ySpeed * 9.8f * Time.deltaTime;

        if (IsGrounded())
        {
            gravity = 0;
        }

        float xSpeed = (IsGrounded() ? _xSpeed : 0);

        transform.position += new Vector3(
            xSpeed * Time.deltaTime,
            gravity,
            0);

        StableOnGround();
    }

    private void Rotate()
    {
        float value = 0;
        if (transform.position.x >= _xMax / 2)
        {
            value = -1;
        }
        else
        {

            value = 1;
        }

        Vector3 scale = _cat.localScale;
        scale.x = value;
        _cat.localScale = scale;

        _xSpeed = Mathf.Abs(_xSpeed) * value;
    }

    private bool IsGrounded()
    {
        return transform.position.y <= _yMin;
    }

    private void StableOnGround()
    {
        if (transform.position.y < _yMin)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    private void SnapToBorder()
    {
        float value = 0;
        if (transform.position.x >= _xMax / 2)
        {
            value = _xMax - _halfCatWidth;
        }
        else
        {

            value = _xMin + _halfCatWidth + 2;
        }

        transform.position = new Vector3(value, transform.position.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDraging == false) 
        {
            return;
        }

        _isDraging = false;

        _spriteCharacter.SetMoveState();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse1) == false) 
        { 
            return; 
        }

        _isDraging = true;

        _spriteCharacter.SetDragState();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse1) && eventData.pointerDrag == this.gameObject)
        {
            _cat.anchoredPosition += eventData.delta;
        }
    }
}
