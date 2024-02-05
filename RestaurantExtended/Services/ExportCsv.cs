using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Text;
using System.Formats.Asn1;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Text;
using File = System.IO.File;
using RestaurantExtended.Controllers.dtos;

namespace RestaurantExtended.Services
{
    public class ExportCsv : Exporter
    {
    

        public ExportResultDto export(string path, List<object> objects)
        {
            int i = 1;

            ExportResultDto exportResultDto = new ExportResultDto();

            List<object> objects2 = new List<object>();



             foreach(object obj in objects)
            {
                var json=JToken.Parse((String)obj);
               
                var fieldsCollector = new JsonFieldsCollector(json);
                var fields = fieldsCollector.GetAllFields();

                Dictionary<string, string> values = new Dictionary<string, string>();



                foreach (var field in fields)
                    values.Add(field.Key, field.Value.ToString());

                objects2.Add(values);




            }


          
     

            var csv = CsvSerializer.SerializeToCsv(objects2);
             String src=csv.ToString();




            FileStream fs = new FileStream(path+".csv",
            FileMode.Create,
             FileAccess.ReadWrite);
            fs.Write(src.ToUtf8Bytes());
            fs.Flush(true);
            fs.Close();

            byte[] byteArray = File.ReadAllBytes(path + ".csv");
            string S = Convert.ToBase64String(byteArray);
            exportResultDto.content = S;
            exportResultDto.filename = path + ".csv";
            return exportResultDto;



        }
    }
}
