using AoC.Common.Puzzle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    public class AoC2020_5Puzzle : PuzzleBase<IEnumerable<(string RowData, string ColumData)>, long>
    {
        private const string Pattern = @"^([BF]{7})([LR]{3})$";
        private const byte MultiplyConstant = 8;

        private static readonly (byte Rows, byte Columns) Plane = (127, 7);

        protected override int Day => 5;

        protected override async ValueTask<IEnumerable<(string RowData, string ColumData)>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            var urkidi = await loadedData.Split(Environment.NewLine)
                .ToAsyncEnumerable()
                .Where(d => Regex.IsMatch(d, Pattern))
                .Select(d => Regex.Split(d, Pattern).Where(split => !string.IsNullOrEmpty(split)))
                .Where(d => d.Count() == 2)
                .Select(d => (RowData: d.FirstOrDefault()?.Replace('B', '1').Replace('F', '0'),
                            ColumData: d.LastOrDefault().Replace('L', '0').Replace('R', '1')))
                .ToArrayAsync(cancellationToken);

            return urkidi;
        }

        protected override async ValueTask<long> FirstPart(IEnumerable<(string RowData, string ColumData)> input, CancellationToken cancellationToken = default)
        {
            var max = long.MinValue;
            await foreach (var (rowData, columnData) in input.ToAsyncEnumerable().WithCancellation(cancellationToken))
            {
                var (row, column) = GetSeatPosition(rowData, columnData);
                var seatId = GetSeatId(column, row);

                if (seatId > max)
                    max = seatId;
            }

            return max;
        }

        protected override async ValueTask<long> SecondPart(IEnumerable<(string RowData, string ColumData)> input, CancellationToken cancellationToken = default)
        {
            var orderedSeats = await input.ToAsyncEnumerable()
                .Select(data =>
                {
                    var (row, column) = GetSeatPosition(data.RowData, data.ColumData);
                    return GetSeatId(column, row);
                })
                .OrderBy(d => d)
                .ToArrayAsync(cancellationToken);

            for (int i = 0; i < orderedSeats.Length; i++)
            {
                var current = orderedSeats[i];
                var next = orderedSeats[(i + 1) % orderedSeats.Length];

                if (next - current >= 2)
                    return current + 1;
            }

            return default;
        }

        private static (byte Row, byte Column) GetSeatPosition(string rowData, string columnData)
        {
            var row = Convert.ToByte(rowData, 2);
            var column = Convert.ToByte(columnData, 2);

            return (row, column);
        }

        private static long GetSeatId(byte column, byte row)
        {
            return row * MultiplyConstant + column;
        }
    }
}