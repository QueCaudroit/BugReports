FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . ./
RUN dotnet restore

CMD [ "dotnet", "run", "--urls", "https://0.0.0.0:5001" ]
