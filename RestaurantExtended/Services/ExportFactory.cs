namespace RestaurantExtended.Services
{
    public class ExportFactory
    {
        public Exporter? create(ExportType type)
        {
            if (type == ExportType.Excell)
                return new ExportExcell();

            if (type == ExportType.Csv)
                return new ExportCsv();
            return null;
        }
    }
}
