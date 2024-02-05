using RestaurantExtended.Controllers.dtos;

namespace RestaurantExtended.Services
{
    public interface Exporter
    {
        public ExportResultDto export(string path,List<Object> objects);

    }
}
