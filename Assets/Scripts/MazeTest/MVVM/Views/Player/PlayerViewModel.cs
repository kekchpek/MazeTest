using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using MazeTest.MVVM.Models.Game;
using MazeTest.MVVM.Models.Input;
using MazeTest.MVVM.Views.Player.Payload;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace MazeTest.MVVM.Views.Player
{
    public class PlayerViewModel : ViewModel, IPlayerViewModel, IInitializable
    {

        private const float PlayerSpeed = 15f;

        private readonly IMutable<Vector3> _position = new Mutable<Vector3>();

        private readonly IPlayerPayload _payload;
        private readonly IInputController _inputController;
        private readonly IGameModel _gameModel;

        private IBindable<Vector3> _inputDirection;
        public IBindable<Vector3> MoveDirection => _inputDirection.ConvertTo(x =>
        {
            if (!_gameModel.IsPlaying.Value)
                return new Vector3();
            return x * PlayerSpeed;
        });

        public IBindable<Vector3> Position => _position;

        public Vector3 SpawnPosition => _payload.SpawnPosition;

        public PlayerViewModel(
            IPlayerPayload payload,
            IInputController inputController,
            IGameModel gameModel)
        {
            _payload = payload;
            _inputController = inputController;
            _gameModel = gameModel;
        }
        
        public void SetPosition(Vector3 position)
        {
            _position.Set(position);
        }

        public void Initialize()
        {
            _inputDirection = _inputController.GetVectorInput(InputUnit.Direction);
        }
    }
}