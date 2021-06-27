namespace CodigoDeBarras
{
	class Regiao
	{
		const string centroOeste = "111";
		const string nordeste = "333";
		const string norte = "555";
		const string sudeste = "888";
		const string sul = "000";

		public string regiao(string regiao)
		{
			switch(regiao)
			{
				case centroOeste:
					regiao = "Centro-oeste";
					break;
				case nordeste:
					regiao = "Nordeste";
					break;
				case norte:
					regiao = "Norte";
					break;
				case sudeste:
					regiao = "Sudeste";
					break;
				case sul:
					regiao = "Sul";
					break;
				default:
					regiao = "ND";
					break;
			}
			return regiao;
		}
	}
}
