using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public abstract class Global
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public Collection<RealState> States { get; set; }
    }
}
