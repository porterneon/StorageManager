using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorageManager.Interfaces;
using StorageManager.Models;

namespace StorageManager.Services
{
    public class OutputFormatter : IOutputFormatter
    {
        public string FormatOutputText(List<Storage> storageCollection)
        {
            var outputBuilder = new StringBuilder();
            var sortedBySumDesc = storageCollection.OrderByDescending(i => i.Materials.Sum(s => s.Quantity)).ThenByDescending(i => i.Id).ToList();

            foreach (var item in sortedBySumDesc)
            {
                outputBuilder.AppendLine(item.ToString());
            }

            return outputBuilder.ToString();
        }
    }
}