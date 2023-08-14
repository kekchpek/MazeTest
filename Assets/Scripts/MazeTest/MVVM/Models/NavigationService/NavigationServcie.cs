using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MazeTest.MVVM.Models.NavigationService
{
    public class NavigationService : INavigationService
    {
        public void BakeNavigationData(Bounds area,
            Bounds[] obstacles)
        {
            NavMesh.RemoveAllNavMeshData();  
            var settings = NavMesh.CreateSettings();
            settings.agentTypeID = 0;
            var buildSources = new List<NavMeshBuildSource>();

            var floorCenter = area.center;
            floorCenter.y = -1f;
            var floor = new NavMeshBuildSource
            {
                transform = Matrix4x4.TRS(floorCenter, Quaternion.identity, Vector3.one),
                shape = NavMeshBuildSourceShape.Box,
                size = new Vector3(area.size.x, 2f, area.size.z)
            };
            buildSources.Add(floor);
    
            // Create obstacle 
            foreach (var obstacleBounds in obstacles)
            {
                var obstacle = new NavMeshBuildSource
                {
                    transform = Matrix4x4.TRS(obstacleBounds.center, Quaternion.identity, obstacleBounds.size),
                    shape = NavMeshBuildSourceShape.Box,
                    size = new Vector3(1, 1, 1),
                    area = 1
                }; 
                buildSources.Add(obstacle);
            }
    
            // build navmesh
            NavMeshData built = NavMeshBuilder.BuildNavMeshData(
                settings, buildSources, area, 
                new Vector3(0,0,0), Quaternion.identity);
            NavMesh.AddNavMeshData(built);
        }
    }
}