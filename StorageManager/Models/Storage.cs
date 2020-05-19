using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageManager.Models
{
    public class Storage
    {
        public string Id { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var quantitySum = Materials.Sum(s => s.Quantity);
            stringBuilder.AppendLine($"{Id} (total {quantitySum})");
            foreach (var item in Materials.OrderBy(i => i.Id))
            {
                stringBuilder.AppendLine($"{item.Id}: {item.Quantity}");
            }

            return stringBuilder.ToString();
        }
    }
}