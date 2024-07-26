namespace frontendLossSounds.Models
{
    public class AlbumModel
    {
    }

    public class AlbumInfo
    {
        public long ID_Album { get; set; }
        public long ID_Artista { get; set; }
        public string Nombre_Album { get; set; }
        public string Nombre_Artista { get; set; }
        public string Genero { get; set; }
        public Nullable<int> Año_Album { get; set; }
        public byte[] Caratula_Album { get; set; }

        public string Caratula_Album_Base64
        {
            get
            {
                return Convert.ToBase64String(Caratula_Album);
            }
            set { }
        }
    }
}
