namespace Welo.Domain.Entities.GDocs
{
    public class GSheetQuery
    {
        public string[] Ranges { get; set; }
        public int[,] FormatCollumns { get; set; }
    }
}