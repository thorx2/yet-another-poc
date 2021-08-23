using UnityEngine;
using UnityEngine.EventSystems;

public class MapTile : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    //Naming is weird in this class for private variables....
    private Color _mouseOverColor = Color.red;
    private Color _originalColor;
    private MeshRenderer _renderer;
    private ITopHudPanel _topHudPanel;
    private IPCControler _pCControler;
    private int _posX, _posY;
    private bool _isPassable;

    private void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        _renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        _originalColor = _renderer.material.color;
    }

    public void InitTileData(ITopHudPanel topHudPanel, IPCControler pCControler, int posX, int posY, bool hadObstacle)
    {
        _topHudPanel = topHudPanel;
        _pCControler = pCControler;
        _posX = posX;
        _posY = posY;
        _isPassable = hadObstacle;
        if (!hadObstacle)
        {
            GameObject sphereIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereIndicator.transform.SetParent(transform);
            sphereIndicator.transform.localScale = new Vector3(0.25f, 0.5f, 0.25f);
            sphereIndicator.transform.localPosition = new Vector3(0, 1.0f, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material.color = _mouseOverColor;
        _topHudPanel.DisplayCellDetails($"{_posX}, {_posY} : Is passable:{_isPassable}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material.color = _originalColor;
        _topHudPanel.DisplayCellDetails("");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(_pCControler.MovePlayerToCell(_posX, _posY));
    }
}
