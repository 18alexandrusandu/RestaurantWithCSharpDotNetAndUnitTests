using RestaurantExtended.Controllers.dtos;
using RestaurantExtended.Models;
using RestaurantExtended.Repositories;
using ServiceStack;
using System.Composition;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RestaurantExtended.Services.implementations
{
    public class ComenziService:IComenziService
    {
       public IUnitOfWork _unitOfWork;
        public ExportFactory exportFactory;

        public ComenziService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }


    public async Task<List<Comanda>> ListAll()
        {
            var all=await _unitOfWork.ComenziRepository.GetAll();


            return all.ToList();
        }



       public async Task<bool> CreazaComanda(Comanda comanda)
        {
            comanda.Date = DateTime.Now;
            comanda.Status = "New";
            double price = 0;
            Boolean ok = true;
             foreach(var produs in comanda.produseComanda)
            {

               
                Produs prod =await _unitOfWork.ProdusRepository.Get(produs.ProductId);

                if(prod.stoc<=0)
                    ok = false;
                else
                if (prod.stoc<produs.quantity)
                {
                    ok=false;
                }else
                {
                    price += produs.quantity * prod.pret;
                }
              


            }
             comanda.pretTotal = price;
            if (ok==true)
            {
               await _unitOfWork.ComenziRepository.Add(comanda);
                await _unitOfWork.Save();
                Debug.WriteLine("it is ok");
                return true;
            }
            Debug.WriteLine("it is  not ok");
            return false;
        }
         public List<Comanda> GetComenziByDate(DateTime start,DateTime end)
        {
           return (List<Comanda>)_unitOfWork.ComenziRepository.GetComenziByInterval(start, end);

        }
        public async Task<List<RankedProduct>> GetProduseComandateByDate(DateTime start, DateTime end)
        {


            List<Comanda> comenzi = (List<Comanda>)_unitOfWork.ComenziRepository.GetComenziByInterval(start, end);
            List<ComandaProdus> legaturi = new List<ComandaProdus>();
            Dictionary<int, int> mapaProdusCantitate = new Dictionary<int, int>();
            Debug.WriteLine("comenzi:", comenzi.ToJson().ToString());
            foreach (var com in comenzi)
            {

                legaturi.AddRange(_unitOfWork.ComenziRepository.GetComenziProduseById(com.Id).ToList());

            }

            foreach (var com in legaturi)
            {
                if (!mapaProdusCantitate.Keys.Contains(com.ProductId))
                    mapaProdusCantitate.Add(com.ProductId, com.quantity);
                else
                    mapaProdusCantitate[com.ProductId] += com.quantity;

            }
            var sortedDict = from entry in mapaProdusCantitate orderby entry.Value descending select entry;
            List<RankedProduct> first10 = new List<RankedProduct>();
            int i = 0;
            while (i < 10 && i < sortedDict.Count())
            {
                var product = await _unitOfWork.ProdusRepository.Get(sortedDict.ElementAt(i).Key);

                if (product != null)
                {
                    RankedProduct ranked = new RankedProduct();
                    ranked.produs = product;
                    ranked.rank = i;
                    ranked.numberOfOrders = sortedDict.ElementAt(i).Value;
                    first10.Add(ranked);

                }

                i++;
            }

            return first10;

        }
        public ExportResultDto export(int type,String path,List<object> objects)
        {

            ExportFactory factory = new ExportFactory();
            Exporter exporter = null;
            if (type == 1)
            {
               exporter=factory.create(ExportType.Csv);
               
            }
            else
            {
               exporter = factory.create(ExportType.Excell);
              

            }
            return  exporter.export(path, objects);

        }

        public async Task<Comanda> GetComanda(int id)
        {
            return await _unitOfWork.ComenziRepository.Get(id);
        }

        public async Task<int> DeleteComanda(int id)
        {
            Comanda comanda=await _unitOfWork.ComenziRepository.Get(id); 
            _unitOfWork.ComenziRepository.Delete(comanda);

            return await _unitOfWork.Save();

        }

        public async Task<int> UpdateComanda(Comanda comanda)
        {
            _unitOfWork.ComenziRepository.Update(comanda);
            return await _unitOfWork.Save();
        }
    }
}
