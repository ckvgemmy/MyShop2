using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    // 1.http request go to controller first
    // 2.controller will call the model ,see what the user wants-are they asking for data or submitting data?
    // 3.controller will control the model to change the data accordingly 
    // 4.view will then take the new arrangement of data and display accordingly

    public class Product

        // 1.when you create a model you need to specify the ciri ciri of the model.
        // 2.the properties of the product need to be define here, name, description, price, category, image, id

    {
        public string Id { get; set; }              // Product informations

        [StringLength(20)]
        [DisplayName("Product Name")]               // Display "Product Name"
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 5000)]
        public string Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public Product()                            // Create a constructor - make an instance and generate a new id each time a new product is created
        {
            this.Id = Guid.NewGuid().ToString();        // Guid = Generated ID
        }
    }
}
