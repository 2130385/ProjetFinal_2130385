using ProjetFinal_2130385.Models;

namespace ProjetFinal_2130385.ViewModels
{
    public class AdressesVM
    {
        public IEnumerable<Adresse> Adresses { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
