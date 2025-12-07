using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Formats.Tar;
using System.Text;
using Jtar.Decompression.Output;
using Jtar.Decompression;

namespace Jtar.Tests
{
    [TestClass]
    public class OutputWorkerTests
    {
        [TestMethod]
        public void OutputWorker_ShouldProduceOneEntry_WhenFedInThirds()
        {
            // Arrange
            var tarBytes = CreateSingleFileTar();

            var thirds = new[]
            {
                tarBytes[..(tarBytes.Length / 3)],
                tarBytes[(tarBytes.Length / 3)..(2 * tarBytes.Length / 3)],
                tarBytes[(2 * tarBytes.Length / 3)..]
            };

            var input = new FakeInput(thirds);
            var output = new FakeOutput<TarEntry>();

            var worker = new OutputWorker(input, output);

            // Act
            worker.Run(); // expected: should NOT throw

            // Assert: desired behavior
            Assert.AreEqual(1, output.Items.Count,
                "Expected exactly one TarEntry after all chunks are fed in.");
        }

        private static byte[] CreateSingleFileTar()
        {
            using var ms = new MemoryStream();
            using var writer = new TarWriter(ms, TarEntryFormat.Pax);

            var data = Encoding.UTF8.GetBytes("Hello world");

            var entry = new PaxTarEntry(TarEntryType.RegularFile, "file.txt")
            {
                DataStream = new MemoryStream(data)
            };

            writer.WriteEntry(entry);
            return ms.ToArray();
        }

        // -----------------------------------------------------
        // Fake IInput + IOutput
        // -----------------------------------------------------

        private class FakeInput : IInput<DecompressionChunk>
        {
            private readonly Queue<DecompressionChunk> _queue;

            public FakeInput(IEnumerable<byte[]> chunks)
            {
                _queue = new Queue<DecompressionChunk>();
                int i = 0;
                foreach (var c in chunks)
                {
                    _queue.Enqueue(new DecompressionChunk(i++, c));
                }
            }

            public bool Get(out DecompressionChunk chunk)
            {
                if (_queue.Count == 0)
                {
                    chunk = null;
                    return false;
                }

                chunk = _queue.Dequeue();
                return true;
            }
        }

        private class FakeOutput<T> : IOutput<T>
        {
            public readonly List<T> Items = new();

            public void Output(T item)
            {
                if (item != null)
                    Items.Add(item);
            }
        }
    }
}
