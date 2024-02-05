using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text;

namespace RestaurantExtended.Models
{
    public class Image
    {
        public int Id { get; set; }

        //64base encoded string
        public string data { get; set; }

        public System.Drawing.Image ToImage()
        {
            byte[] imageBytes = Convert.FromBase64String(data);

            // Don't need to use the constructor that takes the starting offset and length
            // as we're using the whole byte array.
            MemoryStream ms = new MemoryStream(imageBytes);


            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true, true);

            return image;
        }





    }
}
