using Genetic_Algorithm;
using UnityEngine;

namespace Core
{
    public class Navigator : MonoBehaviour
    {
        public GameObject floorBoard;
        private int[,] _grid;

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
                    GameObject board = Instantiate(floorBoard, new Vector3(row, 0, col), Quaternion.identity, transform);
                    board.GetComponent<FloorBoard>().value = _grid[row, col];
                }
            }
        }

        void Start()
        {
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(_grid, 10, 1);
            geneticAlgorithm.RunEvolution();
        }
    }
}