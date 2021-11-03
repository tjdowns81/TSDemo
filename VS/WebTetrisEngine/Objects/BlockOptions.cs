using System;
using System.Collections.Generic;

namespace WebTetrisEngine.Objects
{
    public static class BlockOptions
    {
        public static Dictionary<string, Cell> blocks;

        static BlockOptions()
        {
            blocks = new Dictionary<string, Cell>()
            {
                {
                    "I",
                    new Cell()
                    {
                        Height = 1,
                        Width = 4,
                        Cells = new int[,]
                        {
                            {1, 1, 1, 1}
                        }
                    }
                },
                {
                    "J",
                    new Cell()
                    {
                        Height = 3,
                        Width = 2,
                        Cells = new int[,]
                        {
                            {0, 1},
                            {0, 1},
                            {1, 1}
                        }
                    }
                },
                {
                    "L",
                    new Cell()
                    {
                        Height = 3,
                        Width = 2,
                        Cells = new int[,]
                        {
                            {1, 0},
                            {1, 0},
                            {1, 1}
                        }
                    }
                },
                {
                    "Q",
                    new Cell()
                    {
                        Height = 2,
                        Width = 2,
                        Cells = new int[,]
                        {
                            {1, 1},
                            {1, 1}
                        }
                    }
                },
                {
                    "S",
                    new Cell()
                    {
                        Height = 2,
                        Width = 3,
                        Cells = new int[,]
                        {
                            {0, 1, 1},
                            {1, 1, 0}
                        }
                    }
                },
                {
                    "T",
                    new Cell()
                    {
                        Height = 2,
                        Width = 3,
                        Cells = new int[,]
                        {
                            {1, 1, 1},
                            {0, 1, 0}
                        }
                    }
                },
                {
                    "Z",
                    new Cell()
                    {
                        Height = 2,
                        Width = 3,
                        Cells = new int[,]
                        {
                            {1, 1, 0},
                            {0, 1, 1}
                        }
                    }
                }
            };
        }
    }
}
