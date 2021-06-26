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
			else if(regiao == nordeste)
			{
				regiao = "Nordeste";
			}
			else if(regiao == norte)
			{
				regiao = "Norte";
			}
			else if(regiao == sudeste)
			{
				regiao = "Sudeste";
			}
			else if(regiao == sul)
			{
				regiao = "Sul";
			}
			else
			{
				regiao = "ND";
			}
			return regiao;
		}
	}
}
