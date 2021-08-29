using System;
using System.Collections;
using System.Collections.Generic;
using Genetic_Algorithm;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Navigator : MonoBehaviour
    {
        [Header("Genetic Algorithm Settings")] 
        public int generationLimit = 4000;
        public int population = 200;
        public int fitnessTolerance = 90;
            
        [Header("Unity Stuff")]
        public GameObject floorBoard;
        public LineRenderer pathRenderer;
        public TMP_Text stats;
        public float renderRate;
        
        private int[,] _grid;
        private GeneticAlgorithm _geneticAlgorithm;
        private Queue<(Vector3[] path, float fitness)> _fittestPaths;
        private int _generation;
        private bool _isRunning;

        void Awake()
        {
            _grid = new[,]
            {
                {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 0, 1, 1, 0, 0, 0, 1, 1},
                {1, 1, 0, 1, 1, 0, 0, 0, 1, 1},
                {1, 1, 0, 1, 1, 0, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 0, 1, 0, 0, 0},
                {1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 0, 1, 1, 1, 9},
            };

            for (int col = 0; col < _grid.GetLength(0); col++)
            {
                for (int row = 0; row < _grid.GetLength(1); row++)
                {
                    FloorBoard board = Instantiate(floorBoard, new Vector3(row, 0, col), Quaternion.identity,
                        transform).GetComponent<FloorBoard>();
                    board.value = _grid[row, col];
                    int boundRow = row;
                    int boundCol = col;
                    board.ONStateChanged += (value) => _grid[boundRow, boundCol] = value;
                }
            }

            _geneticAlgorithm = new GeneticAlgorithm(_grid, population, generationLimit, fitnessTolerance);
        }

        public void Run()
        {
            if (!_isRunning)
            {
                Session.Instance.StartSession();
                _isRunning = true;
                _fittestPaths = new Queue<(Vector3[], float)>();
                StartCoroutine(VisualizePathFinding());
            }
            else
            {
                Session.Instance.EndSession();
                _isRunning = false;
                _fittestPaths.Clear();
                _generation = 0;
                StopAllCoroutines();
                stats.text = $"GENERATION\n{_generation}/{generationLimit}\nBEST FITNESS\n{0.0f}";
                pathRenderer.positionCount = 0;
            }
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
            var bestFitness = paths[paths.Length - 1].fitness;
            // var fittestIndividual = paths[0].chromosome;
            var fittestPath = fittestIndividual.Genes;

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

            _fittestPaths.Enqueue((linePoints.ToArray(), bestFitness));
        }

        private IEnumerator VisualizePathFinding()
        {
            IEnumerable generations = _geneticAlgorithm.NextGeneration();
            foreach(var generation in generations)
            {
                yield return new WaitForSeconds(renderRate);
                RenderFittestPaths();
            }
        }

        private void RenderFittestPaths()
        {
            if (_fittestPaths.Count <= 0) return;
            var (path, fitness) = _fittestPaths.Dequeue();
            _generation++;
            stats.text = $"GENERATION\n{_generation}/{generationLimit}\nBEST FITNESS\n{fitness}";
            pathRenderer.positionCount = path.Length;
            pathRenderer.SetPositions(path);
        }
    }
}