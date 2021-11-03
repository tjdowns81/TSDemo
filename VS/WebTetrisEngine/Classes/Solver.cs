using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTetrisEngine.Objects;

namespace WebTetrisEngine.Classes
{
    public class Solver
    {
        public async Task<List<string>> SolveTestFile(StreamReader reader)
        {
            try
            {
                var lines = await ReadInputFile(reader);
                var results = new List<string>();

                foreach (var line in lines)
                {
                    var testRows = new List<int[]>();
                    foreach (string item in line.Split(","))
                    {
                        Cell shape = BlockOptions.blocks[item[0].ToString()];
                        int startNdx = Convert.ToInt32(item[1].ToString());

                        // Add rows to guarantee room
                        AddRowsNeeded(shape.Height, ref testRows);

                        // Find row to add shape to
                        bool bFound = false;
                        int rowToFill = -1;
                        while (!bFound)
                        {
                            rowToFill++;
                            bFound = CheckForSpace(rowToFill, startNdx, shape, testRows);
                        }

                        AddShapeToRows(rowToFill, startNdx, shape, ref testRows);
                    }

                    // Add result to line for output
                    string result = line + "=" + GetAnswer(testRows);
                    results.Add(result);
                }

                return results;
            }
            catch (Exception ex)
            {
                return new List<string>() { ex.ToString() };
            }
        }
        private void AddRowsNeeded(int n, ref List<int[]> rows)
        {
            while (n > 0)
            {
                rows.Add(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                n--;
            }
        }

        private bool CheckForSpace(int startRow, int startNdx, Cell shape, List<int[]> rows)
        {
            bool bSafe = true;
            int currentRow = startRow;
            int y = shape.Height - 1;

            while (y >= 0)
            {
                int x = shape.Width - 1;
                while (x >= 0)
                {
                    if (shape.Cells[y, x] == 1)
                    {
                        if (rows[currentRow][startNdx + x] == 1 ||
                            (CheckPath(rows, (currentRow + 1), (startNdx + x))))
                        {
                            x = -1;
                            y = -1;
                            bSafe = false;
                        }
                    }

                    x--;
                }

                currentRow++;
                y--;
            }

            return bSafe;
        }

        private bool CheckPath(List<int[]> rows, int currentRow, int startNdx)
        {
            bool bBlocked = false;

            while (currentRow < rows.Count && !bBlocked)
            {
                if (rows[currentRow][startNdx] == 1)
                {
                    bBlocked = true;
                    currentRow = rows.Count;
                }

                currentRow++;
            }

            return bBlocked;

        }

        private void AddShapeToRows(int startRow, int startNdx, Cell shape, ref List<int[]> rows)
        {
            int currentRow = startRow;
            int y = shape.Height - 1;

            while (y >= 0)
            {
                int x = shape.Width - 1;
                while (x >= 0)
                {
                    if (shape.Cells[y, x] == 1)
                    {
                        rows[currentRow][startNdx + x] = shape.Cells[y, x];
                    }

                    x--;
                }

                currentRow++;
                y--;
            }
        }

        private int GetAnswer(List<int[]> rows)
        {
            return rows.Count(t => t.Sum() != 0 && t.Sum() != 10);
        }

        private async Task<List<string>> ReadInputFile(StreamReader reader)
        {
            var results = new List<string>();
            while (!reader.EndOfStream)
            {
                var newVal = await reader.ReadLineAsync();
                results.Add(newVal);
            }

            return results;
        }
    }
}
