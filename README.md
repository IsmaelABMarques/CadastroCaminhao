# CadastroCaminhao

## Informação Geral

Sistema para cadastrar caminhões.

## Inicialização do Banco de Dados
No powershell, executar os seguintes comandos na pasta CadastroCaminhao que contiver o arquivo CadastroCaminhao.csproj:

### dotnet ef database update --context RepositoryBase
### dotnet ef database update --context RepositoryBaseTest

## Execução da API

Apenas iniciar a depuração pelo Visual Studio, tecla F5, que a api estará disponível em https://localhost:44306/, acessível por um navegador como Chrome.
Ao clicar em Caminhões, você será redirecionado para a tela de Index, que permitirá visualizar, inserir editar e excluir os caminhões do sistema.

## Execução dos Testes de Unidade

No Visual Studio, no Gerenciador de Soluções, clique com o Botão direito em CadastroCaminhaoTest e então em Executar Testes. Os testes unitários serão então realizados.

A depuração deve estar desligada. 