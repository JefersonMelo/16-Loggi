namespace CodigoDeBarras
{
    class Regiao
    {
        private readonly string centroOeste = "111";
        private readonly string nordeste = "333";
        private readonly string norte = "555";
        private readonly string sudeste = "888";
        private readonly string sul = "000";

        public string regiao(string regiao)
        {
            if(regiao == centroOeste)
            {
                regiao = "Centro-oeste";
            }
            if(regiao == nordeste)
            {
                regiao = "Nordeste";
            }
            if(regiao == norte)
            {
                regiao = "Norte";
            }
            if(regiao == sudeste)
            {
                regiao = "Sudeste";
            }
            if(regiao == sul)
            {
                regiao = "Sul";
            }
            return regiao;
        }
    }
}
