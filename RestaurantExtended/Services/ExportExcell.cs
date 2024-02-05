
using ExcelLibrary;
using ExcelLibrary.SpreadSheet;
using Newtonsoft.Json.Linq;
using RestaurantExtended.Controllers.dtos;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RestaurantExtended.Services
{
    public class ExportExcell : Exporter
    {
        public void export()
        {

          

        }

        public ExportResultDto export(string path, List<object> objects)
        {

            var result = new ExportResultDto();


            string file = path+".xls";

            Workbook workbook = new Workbook();
            Debug.WriteLine("Start");

            Worksheet worksheet = new Worksheet("First Sheet");

            int i = 1;
          


            foreach (var obj in objects)
            {

                var json = JToken.Parse((String)obj);
                var fieldsCollector = new JsonFieldsCollector(json);
                var fields = fieldsCollector.GetAllFields();

         
                int j = 1;
                foreach (var field in fields)
                {

                    Debug.WriteLine("Value:"+field.Value);
                    worksheet.Cells[i, j] = new Cell(field.Key.ToString()+":"+field.Value.ToString());
                    j++;
                }
                i++;

            }





            for (int k = 0; k < 100; k++)
                worksheet.Cells[k, 0] = new Cell("");


            workbook.Worksheets.Add(worksheet);
            
            workbook.Save(file);



            Workbook book = Workbook.Load(file);
            Worksheet sheet = book.Worksheets[0];


            Debug.WriteLine("Done");

            byte[] byteArray = File.ReadAllBytes(file);
            string S = Convert.ToBase64String(byteArray);
            result.content = S;
            result.filename = file;
            return result;

        }
    }
}
