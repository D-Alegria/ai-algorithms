using System;
using System.Collections;
using System.Collections.Generic;
using Genetic_Algorithm;
using UnityEngine;

namespace Core
{
    public class Navigator : MonoBehaviour
    {
        public GameObject floorBoard;
        private int[,] _grid;
        private GeneticAlgorithm _geneticAlgorithm;
        public LineRenderer pathRenderer;
        public float renderRate;
        private Queue<Vector3[]> _fittestPaths;
        private int gen = 1;
        
        void Awake()
        {
            _grid = new[,]
            {
                {1, 0, 1, 1, 1, 1, 1},
                {1, 0, 1, 1, 1, 1, 1},
                {1, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 1, 1},
                {1, 1, 1, 0, 1, 0, 0},
                {1, 1, 1, 0, 1, 1, 1},
                {1, 1, 1, 0, 1, 1, 9},
            };

            for (int col = 0; col < _grid.GetLength(0); col++)
            {
                for (int row = 0; row < _grid.GetLength(1); row++)
                {
                    GameObject board = Instantiate(floorBoard, new Vector3(row, 0, col), Quaternion.identity,
                        transform);
                    board.GetComponent<FloorBoard>().value = _grid[row, col];
                }
            }
            
            _geneticAlgorithm = new GeneticAlgorithm(_grid, 16, 1000);
            _fittestPaths = new Queue<Vector3[]>();
        }

        void Start()
        {
            InvokeRepeating(nameof(RenderFittestPaths), 0, renderRate);
            _geneticAlgorithm.RunEvolution();
        }

        private void OnEnable()
        {
            _geneticAlgorithm!.ONEvaluatedFitness += EnqueueFittestPath;
        }

        private void OnDisable()
        {
            _geneticAlgorithm!.ONEvaluatedFitness -= EnqueueFittestPath;
        }

        private void EnqueueFittestPath((Chromosome chromosome, float fitness)[] paths)
        {
            var fittestIndividual = paths[paths.Length - 1].chromosome;
            // var fittestIndividual = paths[0].chromosome;
            var fittestPath = fittestIndividual.Genes;

            // Debug.Log(fittestIndividual.ToString());
            // Debug.Log(paths[paths.Length - 1].fitness);
            // Debug.Log(paths[0].fitness);

            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0, 0));

            for (int i = 0; i < fittestPath.Length - 1; i++)
            {
                // getting current gene and next gene for comparison
                var geneA = fittestPath[i];
                var geneB = fittestPath[i + 1];

                // checking whether geneA is column or row wise and setting environment location pointer accordingly
                (int row, int col) locationA =
                    (!geneA.IsColumnWise ^
                     (i > fittestIndividual.PathSwitch[0] && i <= fittestIndividual.PathSwitch[1]))
                        ? (geneA.PathLocation.locus, geneA.PathLocation.allelle)
                        : (geneA.PathLocation.allelle, geneA.PathLocation.locus);

                // checking whether geneB is column or row wise and setting environment location pointer accordingly
                (int row, int col) locationB =
                    (!geneB.IsColumnWise ^ (i + 1 > fittestIndividual.PathSwitch[0] &&
                                                        i + 1 <= fittestIndividual.PathSwitch[1]))
                        ? (geneB.PathLocation.locus, geneB.PathLocation.allelle)
                        : (geneB.PathLocation.allelle, geneB.PathLocation.locus);

                if (geneA.PathDirection == PathDirection.Horizontal)
                {
                    linePoints.Add(new Vector3(locationB.col, locationA.row));
                    linePoints.Add(new Vector3(locationB.col, locationB.row));
                }
                else
                {
                    linePoints.Add(new Vector3(locationA.col, locationB.row));
                    linePoints.Add(new Vector3(locationB.col, locationB.row));
                }
            }

            _fittestPaths.Enqueue(linePoints.ToArray());
        }
        
        private void RenderFittestPaths()
        {
            if (_fittestPaths.Count <= 0) return;
            // Debug.Log(gen);
            var linePoints = _fittestPaths.Dequeue();
            pathRenderer.positionCount = linePoints.Length;
            pathRenderer.SetPositions(linePoints);
            gen++;
        }
    }
}