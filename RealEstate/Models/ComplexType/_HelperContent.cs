namespace RealEstate.Models.ComplexType
{
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class _HelperContent
    {
        [NotMapped]
        public string Prefix { get; set; }
    }
}