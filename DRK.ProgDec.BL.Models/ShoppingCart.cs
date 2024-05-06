using System.ComponentModel.DataAnnotations;

namespace DRK.ProgDec.BL.Models
{
    public class ShoppingCart
    {
        // Declaration application specific - Declaration Cost
        // not how we do it in DVDCENTRAL -- Movie Cost is the field

        const double ITEM_COST = 120.03;

        public List<Declaration> Items { get; set; } = new List<Declaration>();
        public int NumberOfItems { get { return Items.Count; } }

        // Display Format - Currency
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get { return Items.Count * ITEM_COST; } }

        //Calc Tax
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * 0.055; } }

        // Real Total W/Tax
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }




    }
}
