# Teste Técnico Loggi

Este Repositório foi criado para entrega do projeto de Geração de Relatório tendo como entrada um código de barras no formato ``### ### ### ### ###`` e cada ternário é responsável por uma checagem de um determinado pacote a ser enviado.  

## O Programa

Esta aplicação do tipo console foi desenvolvida em ``C#`` e está configurada para ler um dado arquivo``.txt`` com ``N`` strings no formato ``Pacote #: ###############`` onde ``Pacote #`` é o título do pacote para organização após a formatação do código de barras e ``:`` é o separador escolhido como padrão para separar a string em duas e para não quebrar a aplicação, o formato ``Pacote #: ###############`` deve ser respeitado.
Após leitura do arquivo, que ao clonar este repositório se encontrará ``C:\...\16-Loggi\CodigoDeBarras\Arquivos\01-Entrada`` com o título ``pacotes`` que já está configurado como padrão e que pode ser adicionada linhas ou removidas sem problemas, desdeque repeitanda a configuração original das strings, mas ao remover o comentário, outro arquivo podem ser lido. A aplicação está configurada para ler arquivos de extenção ``.txt`` e retornará um arquivo``.txt`` no formato, conforme exemplo abaixo.  

```"
Pacote #
Código: ### ### ### ### ##
Região de origem: #####
Região de destino: ########
Código Loggi: ###
Código do vendedor do produto: ###
Tipo do produto: #####
```

## Configurações Necessárias

1. Para o programa funcionar, é necessário indicar o caminho do arquivo. Ao clonar este repositório, ele estará no caminho ``C:\...\16-Loggi\CodigoDeBarras\Arquivos\01-Entrada``.

    - Se desejar configurar um local fixo na aplicação, o formato com duas barras invertidas deve ser respeitado ``C:\\...\\16-Loggi\\CodigoDeBarras\\Arquivos\\01-Entrada``.

    - Não é necessário editar a string se for usar a aplicação. Está configurado para ajustar a string por padrão.

2. A Aplicação está configurada, previamente, com o nome do arquivo, que também é requerido, mas por opção, pois o arquivo ``pacotes.txt`` é padrão, mas o que não impede de que seja alterado o nome do arquivo e requisitar entrada de dados do usuário. Para isso, só é necessário remover o cometário e o valor padrão.  

3. Para gerar os relatórios, é requerido um caminho para que os mesmos sejam armazenados. Ao clonar o este repositório, há um local previamente configurado ``C:\...\16-Loggi\CodigoDeBarras\Arquivos\02-Saida``.

4. Para gerar relatórios, é requisitado pelo sistema que o usuário digite o qual o relatório deseja, se região ``Nordeste`` ou tipo do produto ``Livros``. A princípio a aplicação é case-sensitive e o formato do pdf deve ser respeitado.  

    - O relatório gerado terá o título relatorio-pesquisa escolhida ex: ``relatorio-Nordeste``.

5. Por hora o menu apresentado ao iniciar a aplicação, deve ser respeitado para não quebrar a aplicação. É requisitado uma entrada numérica e deve ser respeitada. Serão de ``0 - 8`` e cada inteiro representa uma opção a ser explorada.

## Bibliografia

|Título|Link Relacionado|
|-|-|
|Formatar String C#|[TecGuia - João da Costa](https://sites.google.com/site/tecguia/formatar-string-c-string-format)|
|1. Como Usar Replace C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/csharp/how-to/modify-string-contents#code-try-3)|
|2. Como Usar Replace C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/api/system.string.replace?view=net-5.0#System_String_Replace_System_Char_System_Char_)|
|3. Como Usar Replace C#|[Stack Overflow](https://stackoverflow.com/questions/4673437/c-sharp-replace-characters)|
|Split C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/standard/base-types/divide-up-strings)|
|Ler Arquivo C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/troubleshoot/dotnet/csharp/read-write-text-file)|
|Escrever Arquivo C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/standard/io/how-to-write-text-to-a-file)|
|Pesquisar Na String C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/csharp/how-to/search-strings)|
|Dividir Strings C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/csharp/how-to/parse-strings-using-split#code-try-4)|
|1. Pesquisar em List<> C#|[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/api/system.collections.generic.list-1.find?view=net-5.0)|
|2. Pesquisar em List<> C#|[Macoratti.net](http://www.macoratti.net/10/02/c_list1.htm)|
|Ordenar List<> C#|[Repositório Jeferon Melo](https://github.com/JefersonMelo/01-URI/blob/master/04-Estruturas_e_Bibliotecas/URI_1258/Program.cs)|
|Remover Duplicados List<> C# |[Docs Microsoft](https://docs.microsoft.com/pt-br/dotnet/framework/data/adonet/sql/linq/eliminate-duplicate-elements-from-a-sequence)|
