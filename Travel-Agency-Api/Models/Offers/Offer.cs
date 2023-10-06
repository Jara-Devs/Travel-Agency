using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel_Agency_Api.Models.Offers;

public abstract class Offer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Description { get; set; }

    [Required] public int Price { get; set; }

    public Offer(string description, int price)
    {
        this.Price = price;
        this.Description = description;
    }
}