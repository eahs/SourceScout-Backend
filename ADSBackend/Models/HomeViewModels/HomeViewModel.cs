using ADSBackend.Models.Identity;

namespace ADSBackend.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public string SearchInput { get; set; }
        public ApplicationUser User { get; set; }
    }
}
