using System;
using UnityEngine;
using Zenject;

namespace MazeTest.Core.Startup
{
    public class StartupBehaviour : MonoBehaviour
    {

        private IStartupService _startupService;
        
        [Inject]
        public void Construct(IStartupService startupService)
        {
            _startupService = startupService;
        }
        
        private async void Awake()
        {
            await _startupService.Startup();
        }
    }
}