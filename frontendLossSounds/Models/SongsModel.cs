namespace frontendLossSounds.Models
{
    public class SongsModel
    {
    }

    public class CancionData
    {
        public long ID_Cancion { get; set; }
        public string Nombre_Cancion { get; set; }
        public decimal? Numero_Cancion { get; set; }
        public int Duracion_Cancion { get; set; }
        public long ID_Artista { get; set; }
        public string Nombre_Artista { get; set; }
        public long ID_Album { get; set; }
        public string Nombre_Album { get; set; }
        public string Ruta_Audio { get; set; }
        public byte[] Caratula_Cancion { get; set; }
        public byte[] File_Content { get; set; } // Propiedad para almacenar el contenido del archivo

        public string Caratula_Cancion_Base64
    {
            get
            {
                return Convert.ToBase64String(Caratula_Cancion);
            }
            set { }
    }
    }
}
