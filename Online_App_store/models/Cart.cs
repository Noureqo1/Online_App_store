using Microsoft.AspNetCore.Mvc;

namespace Online_App_store.models
{
    public class Cart
    {
        [BindProperty]
        public string Delivery_time { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public double Delivery_Fee { get; set; }
        [BindProperty]
        public string Voucher { get; set; }
        [BindProperty]
        public double Total_Price { get; set; }
        [BindProperty]
        public string Status_Voucher { get; set; }
        [BindProperty]
        public int P_ID { get; set; }
    }
}
