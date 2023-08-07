# NL2EF

#### Plugin which translates a question into SQL, fetches relevant data from the database, and formulates a response based on the retrieved data

#### ⭐ Winner of Microsoft's first Semantic Kernel hackathon in the "Most Useful for the Enterprise" category.

## Semantic Kernel Plugin Hackathon Entry

This project is my entry to the Semantic Kernel Plugin Hackathon. It's designed to be a drop-in plugin service that can expose an existing database to be queried via natural language. It accomplishes this by leveraging the power of Entity Framework Core and OpenAI's embedding/GPT models to generate and construct SQL queries that retrieve relevant information for RAG based responses.

The demo connects to a modified version of the SQLite movies database available [here](https://www.kaggle.com/datasets/luizpaulodeoliveira/imdb-project-sql).

Via Chat Copilot:

![Screenshot 2023-07-25 at 1 15 38 AM](/screenshots/chat.png)

Via Swagger:

![Screenshot 2023-07-25 at 8 24 43 AM](/screenshots/swagger.png)

## Built With

- [Semantic Kernel](https://github.com/microsoft/semantic-kernel)
- .NET 7 Minimal Web APIs
- Visual Studio Code (C# and C# Dev Kit Extensions)

## How It Works

1. **Database Creation Script Generation**: Use Entity Framework Core to generate the database creation script.
2. **Embedding Creation**: Create embeddings for each part of the database creation script.
3. **User Input Processing**: Take a user's input and get the most relevant parts of the database creation script that will help build a SQL query.
4. **SQL Query Construction**: Build the SQL query using a GPT model.
5. **Query Execution**: Run the query and attempt to retry and have the model fix its query if it fails.
6. **Response Formatting**: Format the response data as a CSV which the model can easily parse.
7. **Answer Generation**: Answer the user's question using the retrieved data for grounding (RAG).

## Future Enhancements

- Code cleanup
- Modifying the prompts to produce better results
- Moving hardcoded options to be environment configurable

## Pitfalls Before Moving to Production

- Ensure the user connecting to the database has the appropriate permissions (or lack thereof) to prevent SQL injection or users viewing data they shouldn't.
- Seed the kernel database schema memories as part of a preprocessing pipeline instead of every run.
- Be aware of responses overloading the model token window.

## Getting Started

To get a local copy up and running follow the below steps.

### Prerequisites

- .NET 7
- Visual Studio Code
- C# and C# Dev Kit Extensions

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/anthonypuppo/sk-nl2ef-plugin.git
   ```
2. Install .NET packages
   ```sh
   dotnet restore
   ```
3. Open `appsettings.json`
   - Update the `SemanticKernel:AIService` configuration section:
     - Update `Type` to the AI service you will be using (i.e., `AzureOpenAI` or `OpenAI`).
     - If your are using Azure OpenAI, add/update `Endpoint` to your Azure OpenAI resource Endpoint address.
       > If you are using OpenAI, this property will be ignored.
     - Set your Azure OpenAI or OpenAI key by opening a terminal in the project directory and using `dotnet user-secrets`
       ```bash
       dotnet user-secrets set "SemanticKernel:AIService:Key" "MY_AZUREOPENAI_OR_OPENAI_KEY"
       ```
4. Run the project
   ```sh
   dotnet run
   ```

## Usage

Configure the DB context to expose the relevant parts of the database to the model. The service will automatically seed the database creation script embeddings at startup.

## Plugin Manifest

When running locally the plugin will be exposed at https://localhost:7012/.well-known/ai-plugin.json. CORS defaults to allowing ChatGPT as well as https://localhost:7012.
