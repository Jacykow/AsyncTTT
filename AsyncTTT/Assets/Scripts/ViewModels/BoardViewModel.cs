using Assets.Scripts.Abstraction;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.BLL.Operations;
using Assets.Scripts.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModels
{
    public class BoardViewModel : ViewModel
    {
        [SerializeField]
        private GameObject _fieldPrefab, _rowPrefab;
        [SerializeField]
        private RectTransform _fieldContainer;
        [SerializeField]
        private int _boardSize = 20;

        private readonly Subject<Vector2Int> _fieldClickSubject =
            new Subject<Vector2Int>();

        private GameObject[,] _fields;
        private Game _game;

        protected new void Awake()
        {
            base.Awake();

            _fields = new GameObject[_boardSize, _boardSize];
            RectTransform row;
            for (int y = 0; y < _boardSize; y++)
            {
                row = Instantiate(_rowPrefab, _fieldContainer).GetComponent<RectTransform>();
                for (int x = 0; x < _boardSize; x++)
                {
                    _fields[y, x] = Instantiate(_fieldPrefab, row);
                    _fields[y, x].GetComponent<Image>().color =
                        (y + x) % 2 == 0
                        ? Color.white
                        : (Color.white * 0.5f + Color.gray * 1.5f) / 2;
                    _fields[y, x].GetComponent<Button>().interactable = false;
                    var coords = new Vector2Int(x, y);
                    _fields[y, x].GetComponent<Button>().OnClickAsObservable()
                        .Subscribe(_ =>
                        {
                            _fieldClickSubject.OnNext(coords);
                        }).AddTo(this);
                }
            }
        }

        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_fieldContainer);

            _game = ViewManager.Main.ViewParameters["selected_game"] as Game;
            if (_game == null)
            {
                PopupManager.Main.ShowPopup("Nie znaleziono wybranej gry");
                ViewManager.Main.ChangeView("Main");
                return;
            }

            new GetBoard(_game).Execute()
                .Subscribe(game =>
                {
                    for (int y = 0; y < _boardSize; y++)
                    {
                        for (int x = 0; x < _boardSize; x++)
                        {
                            _fields[y, x].transform.GetChild(0).GetComponent<Image>().color =
                                _game.Board[x, y] == 0 ? (Color.gray * 0.5f + Color.green * 1.5f) / 2
                                : _game.Board[x, y] == 1 ? (Color.gray + Color.black) / 2
                                : Color.clear;
                            _fields[y, x].GetComponent<Button>().interactable =
                                _game.Board[x, y] == -1 && (x + y) % 2 == _game.TurnOddity;
                        }
                    }
                }).AddTo(this);

            _fieldClickSubject.SelectMany(clickCoords =>
            {
                return new MakeMove(clickCoords).Execute();
            }).Subscribe(moveResponse =>
            {
                if (moveResponse.GameEnded)
                {
                    PopupManager.Main.ShowPopup("Wygrałeś!");
                    ViewManager.Main.ChangeView("Main");
                    return;
                }

                for (int y = 0; y < _boardSize; y++)
                {
                    for (int x = 0; x < _boardSize; x++)
                    {
                        _fields[y, x].GetComponent<Button>().interactable = false;
                    }
                }
                _fields[moveResponse.MoveCoords.y, moveResponse.MoveCoords.x]
                    .transform.GetChild(0).GetComponent<Image>().color =
                    (Color.gray * 0.5f + Color.green * 1.5f) / 2;
            }).AddTo(this);
        }
    }
}
