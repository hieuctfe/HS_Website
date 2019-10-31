using RealEstate.Models.ComplexType;

namespace RealEstate.ViewModels
{
    public class _MenuItem
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }

    public class UpdateMenuItem
    {
        public UpdateMenuItem()
        {
            UIOption = new UIOption();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public UIOption UIOption { get; set; }
    }
}