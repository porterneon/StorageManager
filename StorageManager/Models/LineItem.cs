using System.Collections.Generic;

namespace StorageManager.Models
{
    public class LineItem
    {
        public string MaterialName { get; set; }
        public string MaterialId { get; set; }
        public List<KeyValuePair<string, int>> StorageQuantity { get; set; }

        public static LineItem FromText(string input)
        {
            var columns = input.Split(';');
            var inputModel = new LineItem
            {
                StorageQuantity = new List<KeyValuePair<string, int>>(),
                MaterialName = columns[0],
                MaterialId = columns[1]
            };

            var storageDetailsColumns = columns[2].Split('|');
            foreach (var storageDetail in storageDetailsColumns)
            {
                var values = storageDetail.Split(',');
                int.TryParse(values[1], out var quantity);
                inputModel.StorageQuantity.Add(new KeyValuePair<string, int>(values[0], quantity));
            }

            return inputModel;
        }
    }
}