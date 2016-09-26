namespace Welo.Domain.Entities
{
    public class GSheetQuery
    {
        public string[] Ranges { get; set; }
        public int[,] FormatCollumns { get; set; }
    }
}