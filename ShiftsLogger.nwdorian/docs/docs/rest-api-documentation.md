# REST API documentation

- REST API Endpoints documentation is automatically generated based on the OpenAPI spec
- Static site is created with DocFx and deployed to Github Pages through Github Actions

## How to integrate DocFx with Github Pages

- Requirements:
  - [DocFx](https://dotnet.github.io/docfx/index.html) for creating a static site
  - [DocFxOpenApi](https://www.nuget.org/packages/DocFxOpenApi) for converting OpenAPI v3 files into DocFx supported format (OpenAPI v2 JSON files)
  - [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/) and [Microsoft.Extensions.ApiDescription.Server](https://www.nuget.org/packages/Microsoft.Extensions.ApiDescription.Server/) NuGet packages for generating OpenAPI documents at build-time
  - Update .gitignore by adding:

```md
# DocFx generated files
docs/**/toc.yml
docs/reference/
_site
_pdf
```

### 1. Install Tools and Packages

- `dotnet tool update -g docfx`
- `dotnet tool install --global DocFxOpenApi --version 1.32.0`
- `dotnet add package Microsoft.Extensions.ApiDescription.Server --version 9.0.2`

### 2. Setup .csproj

```c#
<OpenApiGenerateDocuments>true</OpenApiGenerateDocuments>
<OpenApiDocumentsDirectory>.</OpenApiDocumentsDirectory>
<OpenApiGenerateDocumentsOptions>--file-name my-openapi</OpenApiGenerateDocumentsOptions>
```

- `OpenApiGenerateDocuments` - generate OpenApi document during build
- `OpenApiDocumentsDirectory` -  `.` value will emit the OpenAPI document in the same directory as the project file
- `<OpenApiGenerateDocumentsOptions>--file-name` - custom output file name

### 3. Initialise DocFx

- Open `cmd` in the `root/docs` folder and run `docfx init`
- Select yes for every option

### 4. Add REST API section to DocFx

1. Create `restapi` folder in `root/docs`
2. Add `toc.md` file to `restapi` folder and add

   ```md
   # [Example API](my-openapi.swagger.json)
   ```

3. Edit `toc.yml` in `root/docs` and add

    ```md
    - name: REST API
      href: restapi/
    ```

### 5. Enable Github pages

1. Go to Github Repo Settings - Pages
2. Set Source to Github Actions

### 6. Add workflow file

- check and edit main/master branch name
- check file paths for DocFxOpenApi step
- example workflow file:
  
```yml
name: Publish Documentation

on:
  push:
    branches:
      - main

permissions:
  contents: read
  pages: write
  id-token: write

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout Repository
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: |
            8.0.x
            9.0.x

      - name: Restore Dependencies
        run: dotnet restore

      - name: Build Solution (Generates OpenAPI JSON)
        run: dotnet build

      - name: Install DocFxOpenApi Tool
        run: dotnet tool install --global DocFxOpenApi --version 1.32.0

      - name: Convert OpenAPI to Swagger
        run: DocFxOpenApi -s WebApi/ShiftsLogger.WebApi/my-openapi.json -o docs/restapi

      - name: Install DocFX
        run: dotnet tool install -g docfx

      - name: Build Documentation
        run: docfx docs/docfx.json

      - name: Setup GitHub Pages
        uses: actions/configure-pages@v4

      - name: Upload Documentation Artifact
        uses: actions/upload-pages-artifact@v3
        with:
          path: docs/_site # DocFX outputs files inside 'docs/_site'

      - name: Deploy to GitHub Pages
        uses: actions/deploy-pages@v4

```
